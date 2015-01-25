using System;
using Orleans;
using Orleans.EventSourcing.SimpleInterface;
using System.Threading.Tasks;

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

            GrainClient.Initialize("DevTestClientConfiguration.xml");

            var a = false;

            while (true)
            {
                Console.WriteLine("Orleans Silo is running. give some word..");
                var adf = Console.ReadLine();
                var userAId = Guid.NewGuid();
                var userBId = Guid.NewGuid();
                var accountAId = Guid.Parse("33ca5cd9-e39b-44d1-98cc-82e68baafca2");
                var accountBId = Guid.Parse("0dc32514-51c0-458e-bed7-5d84d5b1de3b");

                var accountA = GrainFactory.GetGrain<IBankAccount>(accountAId);
                var accountB = GrainFactory.GetGrain<IBankAccount>(accountBId);
                if (!a)
                {
                    Task.WhenAll(accountA.Initialize(userAId), accountB.Initialize(userBId)).Wait();
                    a = true;
                }
                else
                {
                    Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
                    Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
                }

                var transferManager = GrainFactory.GetGrain<ITransferTransactionProcessManager>(1);
                transferManager.ProcessTransferTransaction(accountAId, accountBId, 100M).Wait();


                Console.WriteLine("account A balance=" + accountA.GetBalance().Result.ToString());
                Console.WriteLine("account B balance=" + accountB.GetBalance().Result.ToString());
            }
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
