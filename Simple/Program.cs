using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Orleans.EventSourcing.SimpleInterface;

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
                AppDomainInitializerArguments = args
            });

            GrainClient.Initialize("DevTestClientConfiguration.xml");
            var sw3 = Stopwatch.StartNew();
            //TestConcurent();

            TestPerformance();

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

            var accountA = GrainClient.GrainFactory.GetGrain<IBankAccount>(accountAId);
            var accountB = GrainClient.GrainFactory.GetGrain<IBankAccount>(accountBId);

            Console.WriteLine("account A balance=" + accountA.GetBalance().Result);
            Console.WriteLine("account B balance=" + accountB.GetBalance().Result);

            Task.WhenAll(accountA.Initialize(userAId), accountB.Initialize(userBId)).Wait();

            var transferManager = GrainClient.GrainFactory.GetGrain<ITransferTransactionProcessManager>(1);

            var loopTimes = 100;
            while (loopTimes-- > 0)
            {
                var tasks = new List<Task>();
                for (int i = 0; i < 200; i++)
                {
                    decimal amount = new Random().Next(100);
                    tasks.Add(transferManager.ProcessTransferTransaction(accountAId, accountBId, amount));
                }

                Task.WaitAll(tasks.ToArray());
                //Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
                //Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
            }

            Task.Delay(300 * 1000).Wait();

            Console.WriteLine("account A balance=" + accountA.GetBalance().Result);
            Console.WriteLine("account B balance=" + accountB.GetBalance().Result);
        }

        static void TestPerformance()
        {
            var accountPairs = new ConcurrentDictionary<Guid, Guid>();

            var sw = Stopwatch.StartNew();
          
                var accountCreateTasks = new ConcurrentBag<Task>();

                Parallel.For(0, 100, j =>
                {
                    var accountAId = Guid.NewGuid();
                    var accountBId = Guid.NewGuid();
                    accountPairs.TryAdd(accountAId, accountBId);
                });

                accountPairs.AsParallel().ForAll(ap =>
                {
                    var accountA = GrainClient.GrainFactory.GetGrain<IBankAccount>(ap.Key);
                    var accountB = GrainClient.GrainFactory.GetGrain<IBankAccount>(ap.Value);

                    accountCreateTasks.Add(accountA.Initialize(ap.Value));
                    accountCreateTasks.Add(accountB.Initialize(ap.Key));
                });
                Task.WhenAll(accountCreateTasks).Wait();

           
            var sw1 = Stopwatch.StartNew();

            foreach (var kv in accountPairs)
            {
                var transferTasks = new ConcurrentBag<Task>();
                for (int i = 0; i < 200; i++)
                {
                    var managerId = i % 10;
                    var transferManager = GrainClient.GrainFactory.GetGrain<ITransferTransactionProcessManager>(managerId);
                    transferTasks.Add(transferManager.ProcessTransferTransaction(kv.Key, kv.Value, 100M));
                }

                Task.WhenAll(transferTasks).Wait();
            }


            sw1.Stop();

            Console.WriteLine("<-------------" + sw.Elapsed.TotalSeconds + "------------>");
            Console.WriteLine("<-------------" + sw1.Elapsed.TotalSeconds + "------------>");
        }

        private static OrleansHostWrapper hostWrapper;
    }
}
