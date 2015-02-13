using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Concurrency;
using System.Collections.Concurrent;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Orleans.EventSourcing
{
    public class GrainInternalEventHandlerProvider
    {
        private static readonly IDictionary<Type, IDictionary<Type, Action<IEventSourcingGrain, IEvent>>> mappings = new Dictionary<Type, IDictionary<Type, Action<IEventSourcingGrain, IEvent>>>();
        private static readonly IDictionary<string, Type> eventNameMappings = new Dictionary<string, Type>();

        private static readonly BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        private static readonly Type[] parameterTypes = new Type[] { typeof(IEventSourcingGrain), typeof(IEvent) };
        private static readonly List<string> registerAssembly = new List<string>();
        private static object _lock = new object();

        #region event internal handler

        public static Action<IEventSourcingGrain, IEvent> GetInternalEventHandler(Type grainType, Type eventType)
        {
            Action<IEventSourcingGrain, IEvent> eventHandler;
            IDictionary<Type, Action<IEventSourcingGrain, IEvent>> eventHandlerDic;


            if (!mappings.TryGetValue(grainType, out eventHandlerDic))
            {
                var assemblyName = grainType.Assembly.FullName;
                lock (_lock)
                {
                    if (!registerAssembly.Contains(assemblyName))
                    {
                        RegisterInternalEventHandler(grainType.Assembly);
                        registerAssembly.Add(assemblyName);
                    }
                }
                mappings.TryGetValue(grainType, out eventHandlerDic);
            };
            return eventHandlerDic.TryGetValue(eventType, out eventHandler) ? eventHandler : null;
        }

        //private static void registerGrainType(Type grainType)
        //{
        //    foreach (var method in grainType.GetMethods(bindingFlags))
        //    {
        //        var parameters = method.GetParameters();
        //        if (method.Name == "Handle" &&
        //            parameters.Length == 1 &&
        //           typeof(IEvent).IsAssignableFrom(parameters.Single().ParameterType))
        //        {
        //            RegisterInternalEventHandler(grainType, parameters.Single().ParameterType, method);
        //        }
        //    }

        //}

        public static void RegisterInternalEventHandler(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                lock (_lock)
                {
                    RegisterInternalEventHandler(assembly);
                }
            }

        }

        public static void RegisterInternalEventHandler(Assembly assembly)
        {
            if (!registerAssembly.Contains(assembly.FullName))
            {
                lock (_lock)
                {
                    foreach (var grainType in assembly.GetTypes().Where(IsEventSourcingGrain))
                    {
                        var entries = from method in grainType.GetMethods(bindingFlags)
                                      let parameters = method.GetParameters()
                                      where method.Name == "Handle"
                                          && parameters.Length == 1
                                          && typeof(IEvent).IsAssignableFrom(parameters.Single().ParameterType)
                                      select new { Method = method, EventType = parameters.Single().ParameterType };
                        foreach (var entry in entries)
                        {
                            RegisterInternalEventHandlerAndEventType(grainType, entry.EventType, entry.Method);
                        }
                    }
                }
            }
        }

        private static void RegisterInternalEventHandlerAndEventType(Type grainType, Type eventType, MethodInfo method)
        {
            IDictionary<Type, Action<IEventSourcingGrain, IEvent>> eventHandlerDic;

            if (!mappings.TryGetValue(grainType, out eventHandlerDic))
            {
                eventHandlerDic = new Dictionary<Type, Action<IEventSourcingGrain, IEvent>>();
                mappings.Add(grainType, eventHandlerDic);
            }

            if (eventHandlerDic.ContainsKey(eventType))
            {
                throw  new Exception(string.Format("duplicated event handler on event-sourcing grain, grain type:{0}, event type:{1}", grainType.FullName, eventType.FullName));
            }
            else
            {
                var methodDelegate = DynamicReflection.CreateDelegate<Action<IEventSourcingGrain, IEvent>>(method, parameterTypes);

                eventHandlerDic.Add(eventType, methodDelegate);
                eventNameMappings.Add(eventType.FullName, eventType);
            }
        }
        #endregion

        private static bool IsEventSourcingGrain(Type grainType)
        {
            return grainType.IsClass && !grainType.IsAbstract && typeof(IEventSourcingGrain).IsAssignableFrom(grainType);
        }
    }

}
