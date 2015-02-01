#Orleans Event-sourcing Libary

----------
###What is Orleans
  Project [Orleans](https://github.com/dotnet/orleans) provides a straightforward approach to building distributed high-scale computing applications.
  
###USE CASE
  you can see the detail from simple project

####Server config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>  
    <section name="eventStoreProvider" type="Orleans.EventSourcing.EventStoreSection,Orleans.EventSourcing"/>
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
 