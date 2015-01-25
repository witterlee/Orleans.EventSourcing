using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Providers;
using Orleans.Concurrency;
using System.Collections.Concurrent;
using RabbitMQ.Client;
using System.Diagnostics;
using RabbitMQ.Client.Content;
using Newtonsoft.Json;
using System.Threading;
using RabbitMQ.Client.Events;

namespace Orleans.EventSourcing
{
    //[StatelessWorker]
    //when stateless, if the second dispather startup ,it will block on ProcessAllUncommitEventMessageBeforeEventStoreDispatcherStartUp
    //becauce the first dispather awalys send new message to queue, I need to think about this condition
    //so remove [StatelessWorker] before I have a idea to process this condition.                                          
    public class EventStoreDispatcherGrain : Orleans.Grain, Orleans.EventSourcing.IEventStoreDispatcher
    {
        private static readonly IEventStoreProvider eventStoreProvider = EventStoreProviderManager.GetProvider();
        private readonly IEventStore eventStore = eventStoreProvider.Create();
        private static EventStoreDispatcherSetting dispatcherSetting;
        private readonly IDictionary<string, IModel> producterChannels = new Dictionary<string, IModel>();
        private readonly IDictionary<string, Tuple<IModel, EventMessageConsumer>> consumerChannelDic = new Dictionary<string, Tuple<IModel, EventMessageConsumer>>();
        private readonly IDictionary<string, Tuple<IModel, EventMessageConsumer>> tmpconsumerChannelDic = new Dictionary<string, Tuple<IModel, EventMessageConsumer>>();
        private readonly ConcurrentQueue<byte> unCommitEventsMessageCounter = new ConcurrentQueue<byte>();
        private const string MQ_EXCHANAGE_NAME = "___EVENT_STORE_EXCHANGE";
        private const string MQ_QUEUE_NAME = "___EVENT_STORE_QUEUE";
        private const int ERROR_CODE_EVENT_PROCESS = 60000;
        static SemaphoreSlim slim;

        public EventStoreDispatcherGrain()
        {
            slim = new SemaphoreSlim(dispatcherSetting.QueuePoolSize, dispatcherSetting.QueuePoolSize);
            InitEventMessageProducter();
            StartEventMessageConsumer();
        }

        Task IEventStoreDispatcher.Append(Guid grainId, ulong eventVersion, IEvent @event)
        {
            var eventId = grainId + eventVersion.ToString();
            var eventJsonString = JsonConvert.SerializeObject(@event);
            PublishEventMessage(eventId, eventJsonString);

            return TaskDone.Done;
        }

        async Task<IOrderedEnumerable<IEvent>> IEventStoreDispatcher.ReadFrom(Guid grainId, ulong eventVersion)
        {
            IOrderedEnumerable<IEvent> events = null;

            var eventJson = await eventStore.ReadFrom(grainId, eventVersion);

            if (eventJson.Count() > 0)
            {
                events = eventJson.Select(ConvertJsonToEvent).OrderBy(et => et.Version);
            }

            return events;
        }
        public override Task OnActivateAsync()
        {
            ProcessAllUncommitEventMessageBeforeEventStoreDispatcherStartUp();
            return base.OnActivateAsync();
        }

        public static void Initialize(EventStoreDispatcherSetting setting)
        {
            dispatcherSetting = setting;
        }
        private void InitEventMessageProducter()
        {
            //uri will give by  a config item
            var _factory = new ConnectionFactory();
            _factory.Uri = dispatcherSetting.RabbitMqConnectString;
            _factory.AutomaticRecoveryEnabled = true;
            var connection = _factory.CreateConnection();
            var channelCounter = dispatcherSetting.QueuePoolSize;

            while (channelCounter > 0)
            {
                var itemNo = channelCounter - 1;
                var channel = connection.CreateModel();
                var queueName = MQ_QUEUE_NAME + itemNo;

                channel.ExchangeDeclare(MQ_EXCHANAGE_NAME, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(MQ_EXCHANAGE_NAME, queueName, string.Empty);

                producterChannels.Add(queueName, channel);
            }
        }

        private void StartEventMessageConsumer()
        {
            //uri will give by  a config item
            var _factory = new ConnectionFactory();
            _factory.Uri =dispatcherSetting.RabbitMqConnectString;
            _factory.AutomaticRecoveryEnabled = true;
            var connection = _factory.CreateConnection();
            var channelCounter = dispatcherSetting.QueuePoolSize;

            while (channelCounter > 0)
            {
                var itemNo = channelCounter - 1;
                var channel = connection.CreateModel();
                var queueName = MQ_QUEUE_NAME + itemNo;

                channel.ExchangeDeclare(MQ_EXCHANAGE_NAME, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(MQ_EXCHANAGE_NAME, queueName, string.Empty);
                var consumer = new EventMessageConsumer(channel, ProcessEventMessage, ProcessEventMessageFailed);
                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queueName, false, consumer);

                consumerChannelDic.Add(queueName, new Tuple<IModel, EventMessageConsumer>(channel, consumer));
            }


        }
        private void ProcessAllUncommitEventMessageBeforeEventStoreDispatcherStartUp()
        { 
            var _factory = new ConnectionFactory();
            _factory.Uri =dispatcherSetting.RabbitMqConnectString;
            _factory.AutomaticRecoveryEnabled = true;
            var connection = _factory.CreateConnection();
            var channelCounter = dispatcherSetting.QueuePoolSize;

            while (channelCounter > 0)
            {
                var itemNo = channelCounter - 1;
                var channel = connection.CreateModel();
                var queueName = MQ_QUEUE_NAME + itemNo;

                channel.ExchangeDeclare(MQ_EXCHANAGE_NAME, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(MQ_EXCHANAGE_NAME, queueName, string.Empty);
                var consumer = new EventMessageConsumer(channel, ProcessEventMessageOnStartUp, ProcessEventMessageFailed);
                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queueName, false, consumer);

                tmpconsumerChannelDic.Add(queueName, new Tuple<IModel, EventMessageConsumer>(channel, consumer));
            }

            Task.Factory.StartNew(() =>
            {
                this.GetLogger("EventStoreDispatcherStartUp").Info("waiting proccess uncommit event message at {0}", DateTime.Now);
                while (slim.CurrentCount < dispatcherSetting.QueuePoolSize)
                {
                    Task.Delay(1000).Wait();
                }
                this.GetLogger("EventStoreDispatcherStartUp").Info("proccess uncommit event message end at {0}", DateTime.Now);
            }).Wait();
            tmpconsumerChannelDic.AsParallel().ForAll(cc => cc.Value.Item1.Close());
            tmpconsumerChannelDic.Clear();
        }
        private IModel GetChannelByEventId(string eventId)
        {
            var routeKey = GetRouteKeyByEventId(eventId);

            return producterChannels[routeKey];
        }

        private string GetRouteKeyByEventId(string eventId)
        {
            return (eventId.GetHashCode() % dispatcherSetting.QueuePoolSize).ToString();
        }

        private void PublishEventMessage(string eventId, string eventJsonString)
        {
            var useChannel = this.GetChannelByEventId(eventId);
            var routeKey = (eventId.GetHashCode() % dispatcherSetting.QueuePoolSize).ToString();
            var eventMsg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new EventMessage { EventId = eventId, Message = eventJsonString }));
            var build = new BytesMessageBuilder(useChannel);
            build.WriteBytes(eventMsg);
            ((IBasicProperties)build.GetContentHeader()).DeliveryMode = 2;

            useChannel.BasicPublish(MQ_EXCHANAGE_NAME, routeKey, ((IBasicProperties)build.GetContentHeader()), build.GetContentBody());
        }
        private void ProcessEventMessage(EventMessage eventMessage)
        {
            eventStore.Append(eventMessage.EventId, eventMessage.Message);
        }
        private void ProcessEventMessageOnStartUp(EventMessage eventMessage)
        {
            var data = new Byte();
            unCommitEventsMessageCounter.Enqueue(data);
            eventStore.Append(eventMessage.EventId, eventMessage.Message);
            unCommitEventsMessageCounter.TryDequeue(out data);
        }
        private void ProcessEventMessageFailed(Exception ex)
        {
            this.GetLogger("eventMessageProcessor").Warn(ERROR_CODE_EVENT_PROCESS, "process event message failed", ex);
        }

        private IEvent ConvertJsonToEvent(string eventJson)
        {
            dynamic @event = JsonConvert.DeserializeObject(eventJson);
            string eventTypeName = @event.Type;
            Type eventType;

            if (!EventNameTypeMapping.TryGetEventType(eventTypeName, out eventType))
            {
                throw new Exception("unknow event type");
            }

            var convertEvent = JsonConvert.DeserializeObject(eventJson, eventType) as IEvent;

            return convertEvent;
        }
    }

    public class EventMessage
    {
        public string EventId { get; set; }
        public string Message { get; set; }
    }


    public class EventMessageConsumer : DefaultBasicConsumer
    {
        private Action<EventMessage> action;
        private Action<Exception> errorCallback;
        public EventMessageConsumer(IModel model, Action<EventMessage> action, Action<Exception> errorCallback)
            : base(model)
        {
            this.action = action;
            this.errorCallback = errorCallback;
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var messageBody = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<EventMessage>(messageBody);
            try
            {
                action(message);
                Model.BasicAck(deliveryTag, false);
            }
            catch (Exception ex)
            {
                errorCallback(ex);
                Model.BasicNack(deliveryTag, false, true);
            }
        }
    }

    public class EventStoreDispatcherSetting
    {
        public EventStoreDispatcherSetting(string rabbitMqConnectString, int queuePoolSize = 10)
        {
            if (queuePoolSize < 1)
                throw new ArgumentOutOfRangeException("queue pool size can not less than 1");
            if (string.IsNullOrWhiteSpace(rabbitMqConnectString))
                throw new ArgumentNullException("rabbitmq connect string can be null or empty");

            this.RabbitMqConnectString = rabbitMqConnectString;
            this.QueuePoolSize = queuePoolSize;
        }
        /// <summary>
        /// give user name and password in connect string
        /// </summary>
        public string RabbitMqConnectString { get; set; }
        /// <summary>
        /// how many queue to process event message
        /// </summary>
        public int QueuePoolSize { get; set; }
    }
}
