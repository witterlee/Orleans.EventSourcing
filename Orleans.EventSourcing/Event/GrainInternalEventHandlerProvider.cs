//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace Orleans.EventSourcing
//{
//    public class GrainInternalEventHandlerProvider
//    {
//        private static readonly IDictionary<Type, Proc<object,IEventSourcingState>> EventApplyMethodMappings = new Dictionary<Type, Proc<object,IEventSourcingState>>();
//        private static readonly BindingFlags bindingFlags =BindingFlags.Public| BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

//        private static object _lock = new object();

//        #region event internal handler

//        public static Proc<object,IEventSourcingState> GetInternalEventApplyMethod(Type eventType)
//        {
//            Proc<object,IEventSourcingState> eventApplyMethod;

//            return EventApplyMethodMappings.TryGetValue(eventType, out eventApplyMethod) ? eventApplyMethod : null;
//        }

//        //private static void registerGrainType(Type grainType)
//        //{
//        //    foreach (var method in grainType.GetMethods(bindingFlags))
//        //    {
//        //        var parameters = method.GetParameters();
//        //        if (method.Name == "Handle" &&
//        //            parameters.Length == 1 &&
//        //           typeof(IEvent).IsAssignableFrom(parameters.Single().ParameterType))
//        //        {
//        //            RegisterInternalEventHandler(grainType, parameters.Single().ParameterType, method);
//        //        }
//        //    }

//        //}

//        public static void RegisterInternalEventHandler(Assembly[] assemblies)
//        {
//            foreach (var assembly in assemblies)
//            {
//                lock (_lock)
//                {
//                    RegisterInternalEventHandler(assembly);
//                }
//            }

//        }

//        public static void RegisterInternalEventHandler(Assembly assembly)
//        {
//            lock (_lock)
//            {
//                foreach (var eventType in assembly.GetTypes().Where(IsEventType))
//                {
//                    var entries = from method in eventType.GetMethods(bindingFlags)
//                                  let parameters = method.GetParameters()
//                                  where method.Name == "Apply"
//                                      && parameters.Length == 1
//                                      && typeof(EventSourcingState).IsAssignableFrom(parameters.Single().ParameterType)
//                                  select new { Method = method, StateType = parameters.Single().ParameterType };

//                    foreach (var entry in entries)
//                    {
//                        RegisterEventApplyMethod(eventType, entry.StateType, entry.Method);
//                    }
//                }
//            }
//        }

//        private static void RegisterEventApplyMethod(Type eventType, Type stateType, MethodInfo method)
//        {  
//            if (!EventApplyMethodMappings.ContainsKey(eventType))
//            {
//                var applyMethod = Dynamic<object>.Instance.Procedure.Explicit<EventSourcingState>.CreateDelegate(method);

//                EventApplyMethodMappings.Add(eventType, applyMethod);
//            }

//        }
//        #endregion

//        private static bool IsEventType(Type grainType)
//        {
//            return grainType.IsClass && !grainType.IsAbstract && typeof(GrainEvent).IsAssignableFrom(grainType);
//        }
//    }

//}
