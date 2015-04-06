#Orleans Event-sourcing Library

----------
###What is Orleans
  Project [Orleans](https://github.com/dotnet/orleans) provides a straightforward approach to building distributed high-scale computing applications.
  
###USE CASE
  you can see the detail from simple project

###Install From Nuget
    Install-Package Orleans.EventSourcing

####Server Config
    Couchbase event store 
    Install-Package Orleans.EventSourcing.Couchbase
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <sectionGroup name="couchbaseClients">
      <section name="couchbaseDataStore" type="Couchbase.Configuration.Client.Providers.CouchbaseClientSection, Couchbase.NetClient" />
      <section name="couchbaseEventStore" type="Couchbase.Configuration.Client.Providers.CouchbaseClientSection, Couchbase.NetClient" />
    </sectionGroup>
    <section name="eventStoreProvider" type="Orleans.EventSourcing.EventStoreSection,Orleans.EventSourcing" />
  </configSections> 
  <eventStoreProvider>
    <provider Name="CouchBaseEventStore" Type="Orleans.EventSourcing.Couchbase.EventStoreProvider,Orleans.EventSourcing.Couchbase" Default="true" ConfigSection="couchbaseClients/couchbaseEventStore" />
  </eventStoreProvider>
  <couchbaseClients>    
    <couchbaseEventStore>
      <servers>
        <add uri="http://192.168.0.100:8091"/> 
      </servers>
      <buckets>
        <add name="eventstore" password="eventstore" useSsl="false" />
      </buckets>
    </couchbaseEventStore>
  </couchbaseClients>
  <runtime>
    <gcServer enabled="true"/>
  </runtime>
</configuration>
``` 

    MongoDB event store 
    Install-PackageOrleans.EventSourcing.MongoDB
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="eventStoreProvider" type="Orleans.EventSourcing.EventStoreSection,Orleans.EventSourcing" />
  </configSections> 
  <eventStoreProvider>
    <provider Name="MongoDBEventStore" Type="Orleans.EventSourcing.MongoDB.EventStoreProvider,Orleans.EventSourcing.MongoDB" Default="true" DatabaseName="eventstore" ConnectionString="mongodb://192.168.2.10:27017" />
  </eventStoreProvider> 
</configuration>
```
#### OrleansHostWrapper.cs

```csharp
public bool Run()
{
    bool ok = false;

    try
    {
        siloHost.InitializeOrleansSilo();
        var eventStoreSection = (EventStoreSection)ConfigurationManager.GetSection("eventStoreProvider");

        var assembly = Assembly.LoadFrom(".\\Applications\\Orleans.EventSourcing.SimpleGrain\\Orleans.EventSourcing.SimpleGrain.dll");

        siloHost.UseEventStore(eventStoreSection).RegisterGrain(GetEventTypeNameAndCodeMapping(), assembly);
        ok = siloHost.StartOrleansSilo();

        if (ok)
        {
            Console.WriteLine(string.Format("Successfully started Orleans silo '{0}' as a {1} node.", siloHost.Name, siloHost.Type));
        }
        else
        {
            throw new SystemException(string.Format("Failed to start Orleans silo '{0}' as a {1} node.", siloHost.Name, siloHost.Type));
        }
    }
    catch (Exception exc)
    {
        siloHost.ReportStartupError(exc);
        var msg = string.Format("{0}:\n{1}\n{2}", exc.GetType().FullName, exc.Message, exc.StackTrace);
        Console.WriteLine(msg);
    }

    return ok;
}

private Dictionary<string, uint> GetEventTypeNameAndCodeMapping()
{
    //Generate from EventTypeCodeRegisterTool
    var typeCodeDic = new Dictionary<string, uint>();
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent", 1001);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent", 1002);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent", 1003);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent", 1004);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent", 1005);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent", 1006);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent", 1007);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent", 1008);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent", 1009);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent", 1010);
    typeCodeDic.Add("Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent", 1011);

    return typeCodeDic;
}
```
####How to generate Event Code
    Note:  please keep the event code  same as before in your products, Event code generator tool just for easy to use
![](https://github.com/weitaolee/Orleans.EventSourcing/blob/develop/generatecode.jpg)
####Grain code

```csharp
 [EventStoreProvider(ProviderName = "CouchBaseEventStore")]
    [StorageProvider(ProviderName = "CouchbaseStore")]    
    public class BankAccount : EventSourcingGrain<BankAccount, IBankAcountState>, IBankAccount
   {
        ...
   }
``` 

please note,if you config provider **Default="true"**,when grain do not has a [EventStoreProvider] attribute, this grain will use the default event provider.   

you can config many eventstore provider like Orleans' StorageProvider, and use by a similar way. 
```xml
<provider Name="CouchBaseEventStore" Type="Orleans.EventSourcing.Couchbase.EventStoreProvider,Orleans.EventSourcing.Couchbase" Default="true" ConfigSection="couchbaseClients/couchbaseEventStore" />
```
 