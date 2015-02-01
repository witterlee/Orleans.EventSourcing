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
    public class EventNameTypeMapping
    {
        private static readonly IDictionary<string, Type> eventNameMappings = new Dictionary<string, Type>();
        private static readonly List<string> registerAssembly = new List<string>();

        private static object _lock = new object();

        public static bool TryGetEventType(string typeName, out Type eventType)
        {
            return eventNameMappings.TryGetValue(typeName, out eventType);
        }
        public static void RegisterEventType(Assembly[] assemblies)
        {
            if (assemblies != null && assemblies.Count() > 0)
            {
                lock (_lock)
                {
                    foreach (var assembly in assemblies)
                        RegisterEventType(assembly);
                }
            }
        }

        public static void RegisterEventType(Assembly assembly)
        {
            if (!registerAssembly.Contains(assembly.FullName))
            {
                lock (_lock)
                {
                    if (!registerAssembly.Contains(assembly.FullName))
                    {
                        foreach (var type in assembly.GetTypes().Where(IsEventType))
                        {
                            RegisterEventType(type);
                        }
                        registerAssembly.Add(assembly.FullName);
                    }
                }
            }
        }

        private static void RegisterEventType(Type type)
        {
            if (!eventNameMappings.ContainsKey(type.FullName))
                eventNameMappings.Add(type.FullName, type);
        }


        private static bool IsEventType(Type grainType)
        {
            return grainType.IsClass && !grainType.IsAbstract && typeof(IEvent).IsAssignableFrom(grainType);
        }
    }

}
