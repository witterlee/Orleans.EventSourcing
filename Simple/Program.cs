using System;
using Orleans;
using Orleans.EventSourcing.SimpleInterface;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            var userAId = Guid.NewGuid();
            var userBId = Guid.NewGuid();
            var accountAId = Guid.Parse("33ca5cd9-e39b-44d1-98cc-82e68baafca1");
            var accountBId = Guid.Parse("0dc32514-51c0-458e-bed7-5d84d5b1de3f");

            var accountA = GrainFactory.GetGrain<IBankAccount>(accountAId);
            var accountB = GrainFactory.GetGrain<IBankAccount>(accountBId);

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
                Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
                Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
            }

            Task.Delay(300 * 1000).Wait();

            Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
            Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());

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

        private static OrleansHostWrapper hostWrapper;
    }
}
