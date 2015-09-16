using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orleans.EventSourcing.QuerySide
{
    public class QuerySideProcessorContainer
    {
        private readonly object WriteLock = new object();
        private readonly Dictionary<Type, Tuple<Func<object,WriteResult, GrainEvent>, IQuerySideHandler>> DelegateForEvent = new Dictionary<Type, Tuple<Func<object, WriteResult, GrainEvent>, IQuerySideHandler>>();

        public Tuple<Func<object, WriteResult, GrainEvent>, IQuerySideHandler> FindHandler(Type commandType)
        {
            var executor = default(Tuple<Func<object, WriteResult, GrainEvent>, IQuerySideHandler>);

            this.DelegateForEvent.TryGetValue(commandType, out executor);

            return executor;
        }

        public void RegisterHandler(Type executorType)
        {
            if (executorType == null)
                throw new ArgumentNullException("executorType", "executorType is null");

            if (!executorType.IsClass || executorType.IsAbstract || executorType.IsGenericType || executorType.IsInterface) return;

            var executorTypes = TypeUtil.GetGenericArgumentTypes(executorType, typeof(IQuerySideHandler<>));

            if (executorTypes != null && executorTypes.Any())
                lock (this.WriteLock)
                {
                    var executorInstance = (IQuerySideHandler)Activator.CreateInstance(executorType);

                    foreach (var method in executorInstance.GetType().GetMethods())
                    {
                        if (method.Name == "Handle")
                        {
                            var @params = method.GetParameters();

                            if (@params.Length == 1)
                            {
                                var paramsType = @params.First().ParameterType;
                                foreach (var execType in executorTypes)
                                {
                                    if (execType == paramsType)
                                    {
                                        var del = Dynamic<object>.Instance.Function<WriteResult>.Explicit<GrainEvent>.CreateDelegate(method);

                                        this.DelegateForEvent[execType] =
                                            new Tuple<Func<object, WriteResult, GrainEvent>, IQuerySideHandler>(del,executorInstance);
                                    }
                                    //this._executorForCommand[execType] = executorInstance;
                                }
                            }
                        }
                    }

                }
        }

        public void RegisterHandlers(Assembly assemblyToScan)
        {
            if (assemblyToScan == null)
                throw new ArgumentNullException("assemblyToScan", "assemblyToScan is null");

            foreach (Type type in assemblyToScan.GetTypes())
            {
                this.RegisterHandler(type);
            }
        }

        public void RegisterHandlers(IEnumerable<Assembly> assembliesToScan)
        {
            if (assembliesToScan == null || assembliesToScan.Count() < 0)
                throw new ArgumentNullException("assembliesToScan", "assembliesToScan is null");

            foreach (var assemblyToScan in assembliesToScan)
            {
                foreach (Type type in assemblyToScan.GetTypes())
                {
                    this.RegisterHandler(type);
                }
            }
        }
    }

    internal static class TypeUtil
    {
        public static IEnumerable<Type> GetGenericArgumentTypes(Type concreteType, Type genericType)
        {
            foreach (var @interface in concreteType.GetInterfaces())
            {
                if (!@interface.IsGenericType) continue;

                if (@interface.GetGenericTypeDefinition() == genericType)
                {
                    foreach (var argType in @interface.GetGenericArguments())
                    {
                        yield return argType;
                    }
                }
            }
        }
        public static bool IsAttributeDefinedInMethodOrDeclaringClass(MethodInfo method, Type attributeType)
        {
            return method.IsDefined(attributeType, false) || method.DeclaringType.IsDefined(attributeType, false);
        }
    }
}