using System;
using Orleans;
using Orleans.EventSourcing.SimpleInterface;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Simple
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // The Orleans silo environment is initialized in its own app domain in order to more
            // closely emulate the distributed situation, when the client and the server cannot
            // pass data via shared memory.
            AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                AppDomainInitializerArguments = args,
            });

            Orleans.GrainClient.Initialize("DevTestClientConfiguration.xml");
            var sw3 = Stopwatch.StartNew();
            TestConcurent();

            //TestPerformance();

            Console.WriteLine("<------总共用时-------" + sw3.Elapsed.TotalSeconds + "------------>");

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();
            hostDomain.DoCallBack(ShutdownSilo);
        }

        static void InitSilo(string[] args)
        {
            hostWrapper = new OrleansHostWrapper(args);

            if (!hostWrapper.Run())
            {
                Console.Error.WriteLine("Failed to initialize Orleans silo");
            }
        }

        static void ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                hostWrapper.Dispose();
                GC.SuppressFinalize(hostWrapper);
            }
        }

        static void TestConcurent()
        {
            var userAId = Guid.NewGuid();
            var userBId = Guid.NewGuid();
            var accountAId = Guid.Parse("33ca5cd9-e39b-44d1-98cc-82e68baafca2");
            var accountBId = Guid.Parse("0dc32514-51c0-458e-bed7-5d84d5b1de31");

            var accountA = GrainFactory.GetGrain<IBankAccount>(accountAId);
            var accountB = GrainFactory.GetGrain<IBankAccount>(accountBId);

            Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
            Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());

            Task.WhenAll(accountA.Initialize(userAId), accountB.Initialize(userBId)).Wait();

            var transferManager = GrainFactory.GetGrain<ITransferTransactionProcessManager>(1);

            var loopTimes = 100;
            while (loopTimes-- > 0)
            {
                var tasks = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    decimal amount = new Random().Next(100);
                    tasks.Add(transferManager.ProcessTransferTransaction(accountAId, accountBId, amount));
                }

                Task.WaitAll(tasks.ToArray());
                //Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
                //Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
            }

            Task.Delay(300 * 1000).Wait();

            Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
            Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
        }

        static void TestPerformance()
        {
            var accountPairs = new ConcurrentDictionary<Guid, Guid>();
            var accountCreateTasks = new ConcurrentBag<Task>();
            var transferTasks = new ConcurrentBag<Task>();


            Parallel.For(0, 100, (i) =>
            {
                var accountAId = Guid.NewGuid();
                var accountBId = Guid.NewGuid();
                accountPairs.TryAdd(accountAId, accountBId);
            });

            var sw = Stopwatch.StartNew();
            accountPairs.AsParallel().ForAll((ap) =>
            {
                var accountA = GrainFactory.GetGrain<IBankAccount>(ap.Key);
                var accountB = GrainFactory.GetGrain<IBankAccount>(ap.Value);

                accountCreateTasks.Add(accountA.Initialize(ap.Value));
                accountCreateTasks.Add(accountB.Initialize(ap.Key));
            });
            Task.WhenAll(accountCreateTasks).Wait();

            Thread.Sleep(3000);

            var sw1 = Stopwatch.StartNew();

            foreach (var kv in accountPairs)
            {
                for (int i = 0; i < 100; i++)
                { 
                    var managerId = i % 10;
                    var transferManager = GrainFactory.GetGrain<ITransferTransactionProcessManager>(managerId);
                    transferTasks.Add(transferManager.ProcessTransferTransaction(kv.Key, kv.Value, 100M));
                } 
            } 

            Task.WhenAll(transferTasks).ConfigureAwait(false).GetAwaiter().GetResult();

            sw1.Stop();

            Console.WriteLine("<-------------" + sw.Elapsed.TotalSeconds + "------------>");
            Console.WriteLine("<-------------" + sw1.Elapsed.TotalSeconds + "------------>");
        }

        private static OrleansHostWrapper hostWrapper;
    }
}
