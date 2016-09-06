#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998
[assembly: global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0")]
[assembly: global::Orleans.CodeGeneration.OrleansCodeGenerationTargetAttribute("Orleans.EventSourcing.SimpleGrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
namespace Orleans.EventSourcing.SimpleGrain
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_BankAccountInitializedSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Orleans.EventSourcing.TaskMessage).@GetTypeInfo().@GetField("<Message>k__BackingField", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.String> setField1 = (global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.String>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Orleans.EventSourcing.TaskMessage).@GetTypeInfo().@GetField("<Success>k__BackingField", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.Boolean> setField0 = (global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.Boolean>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            return original;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized input = (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Message, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Success, stream, typeof (global::System.Boolean));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized result = (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField1(result, (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream));
            setField0(result, (global::System.Boolean)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Boolean), stream));
            return (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.BankAccountInitialized), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_BankAccountInitializedSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_BalanceNotEnoughSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Orleans.EventSourcing.TaskMessage).@GetTypeInfo().@GetField("<Message>k__BackingField", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.String> setField1 = (global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.String>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Orleans.EventSourcing.TaskMessage).@GetTypeInfo().@GetField("<Success>k__BackingField", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.Boolean> setField0 = (global::System.Action<global::Orleans.EventSourcing.TaskMessage, global::System.Boolean>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            return original;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough input = (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Message, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Success, stream, typeof (global::System.Boolean));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough result = (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField1(result, (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream));
            setField0(result, (global::System.Boolean)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Boolean), stream));
            return (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.BalanceNotEnough), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_BalanceNotEnoughSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_BankAccountInitializeEventSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent).@GetTypeInfo().@GetField("<OwnerId>k__BackingField", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Action<global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent, global::System.Guid> setField0 = (global::System.Action<global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent, global::System.Guid>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            setField0(result, (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@OwnerId));
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@OwnerId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            setField0(result, (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream));
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.BankAccountInitializeEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_BankAccountInitializeEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationAddedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionPreparation);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionPreparation, stream, typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationAddedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationAddedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_TransactionPreparationSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation input = ((global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)original);
            global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation result = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation));
            result.@Amount = input.@Amount;
            result.@PreparationType = input.@PreparationType;
            result.@TransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransactionId);
            result.@TransactionType = input.@TransactionType;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation input = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Amount, stream, typeof (global::System.Decimal));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@PreparationType, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.PreparationType));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransactionType, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransactionType));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation result = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@Amount = (global::System.Decimal)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Decimal), stream);
            result.@PreparationType = (global::Orleans.EventSourcing.SimpleInterface.PreparationType)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.PreparationType), stream);
            result.@TransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransactionType = (global::Orleans.EventSourcing.SimpleInterface.TransactionType)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransactionType), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_TransactionPreparationSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationCommittedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent));
            result.@CommandId = input.@CommandId;
            result.@CurrentBalance = input.@CurrentBalance;
            result.@GrainId = input.@GrainId;
            result.@TransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransactionPreparation);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CurrentBalance, stream, typeof (global::System.Decimal));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransactionPreparation, stream, typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@CurrentBalance = (global::System.Decimal)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Decimal), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCommittedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationCommittedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationCanceledEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransactionPreparation);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransactionPreparation, stream, typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransactionPreparation = (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleGrain.TransactionPreparation), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransactionPreparationCanceledEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransactionPreparationCanceledEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferTransactionStartedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferTransactionStartedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferTransactionStartedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleInterface_TransferTransactionInfoSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo input = ((global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)original);
            global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo result = new global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo();
            result.@Amount = input.@Amount;
            result.@FromAccountId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@FromAccountId);
            result.@ToAccountId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@ToAccountId);
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo input = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Amount, stream, typeof (global::System.Decimal));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@FromAccountId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@ToAccountId, stream, typeof (global::System.Guid));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo result = new global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@Amount = (global::System.Decimal)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Decimal), stream);
            result.@FromAccountId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@ToAccountId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            return (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleInterface_TransferTransactionInfoSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_AccountValidatePassedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.AccountValidatePassedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_AccountValidatePassedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferOutPreparationConfirmedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutPreparationConfirmedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferOutPreparationConfirmedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferInPreparationConfirmedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInPreparationConfirmedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferInPreparationConfirmedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferOutConfirmedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferOutConfirmedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferOutConfirmedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferInConfirmedEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionInfo);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionInfo, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TransferTransactionInfo = (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferInConfirmedEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferInConfirmedEventSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferCanceledEventSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent input = ((global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)original);
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent));
            result.@CommandId = input.@CommandId;
            result.@GrainId = input.@GrainId;
            result.@TransactionFaileReason = input.@TransactionFaileReason;
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@TransferTransactionId);
            result.@TypeCode = input.@TypeCode;
            result.@UtcTimestamp = input.@UtcTimestamp;
            result.@Version = input.@Version;
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent input = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@CommandId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GrainId, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransactionFaileReason, stream, typeof (global::Orleans.EventSourcing.SimpleInterface.TransactionFaileReason));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TransferTransactionId, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@TypeCode, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@UtcTimestamp, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Version, stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent result = (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@CommandId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@GrainId = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@TransactionFaileReason = (global::Orleans.EventSourcing.SimpleInterface.TransactionFaileReason)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Orleans.EventSourcing.SimpleInterface.TransactionFaileReason), stream);
            result.@TransferTransactionId = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@TypeCode = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@UtcTimestamp = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@Version = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            return (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SimpleGrain.Events.TransferCanceledEvent), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SimpleGrain_Events_TransferCanceledEventSerializer()
        {
            Register();
        }
    }
}
#pragma warning restore 162
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 649
#pragma warning restore 693
#pragma warning restore 1591
#pragma warning restore 1998
#endif
