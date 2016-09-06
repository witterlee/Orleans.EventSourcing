#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998
[assembly: global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0")]
[assembly: global::Orleans.CodeGeneration.OrleansCodeGenerationTargetAttribute("Orleans.EventSourcing.SimpleInterface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
namespace Orleans.EventSourcing
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.SuccessMessage)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_SuccessMessageSerializer
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
            global::Orleans.EventSourcing.SuccessMessage input = (global::Orleans.EventSourcing.SuccessMessage)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Message, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Success, stream, typeof (global::System.Boolean));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.SuccessMessage result = (global::Orleans.EventSourcing.SuccessMessage)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.SuccessMessage));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField1(result, (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream));
            setField0(result, (global::System.Boolean)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Boolean), stream));
            return (global::Orleans.EventSourcing.SuccessMessage)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.SuccessMessage), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_SuccessMessageSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.EventSourcing.ErrorMessage)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_EventSourcing_ErrorMessageSerializer
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
            global::Orleans.EventSourcing.ErrorMessage input = (global::Orleans.EventSourcing.ErrorMessage)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Message, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@Success, stream, typeof (global::System.Boolean));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.EventSourcing.ErrorMessage result = (global::Orleans.EventSourcing.ErrorMessage)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Orleans.EventSourcing.ErrorMessage));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField1(result, (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream));
            setField0(result, (global::System.Boolean)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Boolean), stream));
            return (global::Orleans.EventSourcing.ErrorMessage)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.EventSourcing.ErrorMessage), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_EventSourcing_ErrorMessageSerializer()
        {
            Register();
        }
    }
}

namespace Orleans.EventSourcing.SimpleInterface
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction))]
    internal class OrleansCodeGenTransferTransactionReference : global::Orleans.Runtime.GrainReference, global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction
    {
        protected @OrleansCodeGenTransferTransactionReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenTransferTransactionReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1895808213;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1895808213;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1895808213:
                    switch (@methodId)
                    {
                        case 518927232:
                            return "Initialize";
                        case 901321164:
                            return "ConfirmAccountValidatePassed";
                        case 1613436135:
                            return "ConfirmTransferOutPreparation";
                        case 108736162:
                            return "ConfirmTransferInPreparation";
                        case -1176500040:
                            return "ConfirmTransferOut";
                        case 1990501567:
                            return "ConfirmTransferIn";
                        case 751141822:
                            return "Cancel";
                        case -1251605435:
                            return "GetStatus";
                        case 1419660080:
                            return "GetTransferTransactionInfo";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1895808213 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Initialize(global::System.Guid @fromAccountId, global::System.Guid @toAccountId, global::System.Decimal @amount)
        {
            return base.@InvokeMethodAsync<global::System.Object>(518927232, new global::System.Object[]{@fromAccountId, @toAccountId, @amount});
        }

        public global::System.Threading.Tasks.Task @ConfirmAccountValidatePassed()
        {
            return base.@InvokeMethodAsync<global::System.Object>(901321164, null);
        }

        public global::System.Threading.Tasks.Task @ConfirmTransferOutPreparation()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1613436135, null);
        }

        public global::System.Threading.Tasks.Task @ConfirmTransferInPreparation()
        {
            return base.@InvokeMethodAsync<global::System.Object>(108736162, null);
        }

        public global::System.Threading.Tasks.Task @ConfirmTransferOut()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1176500040, null);
        }

        public global::System.Threading.Tasks.Task @ConfirmTransferIn()
        {
            return base.@InvokeMethodAsync<global::System.Object>(1990501567, null);
        }

        public global::System.Threading.Tasks.Task @Cancel(global::Orleans.EventSourcing.SimpleInterface.TransactionFaileReason @reason)
        {
            return base.@InvokeMethodAsync<global::System.Object>(751141822, new global::System.Object[]{@reason});
        }

        public global::System.Threading.Tasks.Task<global::Orleans.EventSourcing.SimpleInterface.TransactionStatus> @GetStatus()
        {
            return base.@InvokeMethodAsync<global::Orleans.EventSourcing.SimpleInterface.TransactionStatus>(-1251605435, null);
        }

        public global::System.Threading.Tasks.Task<global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo> @GetTransferTransactionInfo()
        {
            return base.@InvokeMethodAsync<global::Orleans.EventSourcing.SimpleInterface.TransferTransactionInfo>(1419660080, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction", 1895808213, typeof (global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenTransferTransactionMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case 1895808213:
                        switch (methodId)
                        {
                            case 518927232:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@Initialize((global::System.Guid)arguments[0], (global::System.Guid)arguments[1], (global::System.Decimal)arguments[2]).@Box();
                            case 901321164:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@ConfirmAccountValidatePassed().@Box();
                            case 1613436135:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@ConfirmTransferOutPreparation().@Box();
                            case 108736162:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@ConfirmTransferInPreparation().@Box();
                            case -1176500040:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@ConfirmTransferOut().@Box();
                            case 1990501567:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@ConfirmTransferIn().@Box();
                            case 751141822:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@Cancel((global::Orleans.EventSourcing.SimpleInterface.TransactionFaileReason)arguments[0]).@Box();
                            case -1251605435:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@GetStatus().@Box();
                            case 1419660080:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransaction)@grain).@GetTransferTransactionInfo().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 1895808213 + ",methodId=" + methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1895808213;
            }
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

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager))]
    internal class OrleansCodeGenTransferTransactionProcessManagerReference : global::Orleans.Runtime.GrainReference, global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager
    {
        protected @OrleansCodeGenTransferTransactionProcessManagerReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenTransferTransactionProcessManagerReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 2060187244;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 2060187244;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 2060187244:
                    switch (@methodId)
                    {
                        case 1500745786:
                            return "ProcessTransferTransaction";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 2060187244 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @ProcessTransferTransaction(global::System.Guid @fromAccountId, global::System.Guid @toAccountId, global::System.Decimal @amount)
        {
            return base.@InvokeMethodAsync<global::System.Object>(1500745786, new global::System.Object[]{@fromAccountId, @toAccountId, @amount});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager", 2060187244, typeof (global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenTransferTransactionProcessManagerMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case 2060187244:
                        switch (methodId)
                        {
                            case 1500745786:
                                return ((global::Orleans.EventSourcing.SimpleInterface.ITransferTransactionProcessManager)@grain).@ProcessTransferTransaction((global::System.Guid)arguments[0], (global::System.Guid)arguments[1], (global::System.Decimal)arguments[2]).@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + 2060187244 + ",methodId=" + methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 2060187244;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Orleans.EventSourcing.SimpleInterface.IBankAccount))]
    internal class OrleansCodeGenBankAccountReference : global::Orleans.Runtime.GrainReference, global::Orleans.EventSourcing.SimpleInterface.IBankAccount
    {
        protected @OrleansCodeGenBankAccountReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenBankAccountReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -912346303;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Orleans.EventSourcing.SimpleInterface.IBankAccount";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -912346303;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -912346303:
                    switch (@methodId)
                    {
                        case -1993006295:
                            return "Initialize";
                        case 1980500241:
                            return "Validate";
                        case 1417003246:
                            return "AddTransactionPreparation";
                        case -576022763:
                            return "CommitTransactionPreparation";
                        case -1730429136:
                            return "CancelTransactionPreparation";
                        case 698066810:
                            return "GetBalance";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -912346303 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Initialize(global::System.Guid @ownerId)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1993006295, new global::System.Object[]{@ownerId});
        }

        public global::System.Threading.Tasks.Task<global::System.Boolean> @Validate()
        {
            return base.@InvokeMethodAsync<global::System.Boolean>(1980500241, null);
        }

        public global::System.Threading.Tasks.Task<global::Orleans.EventSourcing.TaskMessage> @AddTransactionPreparation(global::System.Guid @transactionId, global::Orleans.EventSourcing.SimpleInterface.TransactionType @transactionType, global::Orleans.EventSourcing.SimpleInterface.PreparationType @preparationType, global::System.Decimal @amount)
        {
            return base.@InvokeMethodAsync<global::Orleans.EventSourcing.TaskMessage>(1417003246, new global::System.Object[]{@transactionId, @transactionType, @preparationType, @amount});
        }

        public global::System.Threading.Tasks.Task @CommitTransactionPreparation(global::System.Guid @transactionId)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-576022763, new global::System.Object[]{@transactionId});
        }

        public global::System.Threading.Tasks.Task @CancelTransactionPreparation(global::System.Guid @transactionId)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1730429136, new global::System.Object[]{@transactionId});
        }

        public global::System.Threading.Tasks.Task<global::System.Decimal> @GetBalance()
        {
            return base.@InvokeMethodAsync<global::System.Decimal>(698066810, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.0.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Orleans.EventSourcing.SimpleInterface.IBankAccount", -912346303, typeof (global::Orleans.EventSourcing.SimpleInterface.IBankAccount)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenBankAccountMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            try
            {
                if (@grain == null)
                    throw new global::System.ArgumentNullException("grain");
                switch (interfaceId)
                {
                    case -912346303:
                        switch (methodId)
                        {
                            case -1993006295:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@Initialize((global::System.Guid)arguments[0]).@Box();
                            case 1980500241:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@Validate().@Box();
                            case 1417003246:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@AddTransactionPreparation((global::System.Guid)arguments[0], (global::Orleans.EventSourcing.SimpleInterface.TransactionType)arguments[1], (global::Orleans.EventSourcing.SimpleInterface.PreparationType)arguments[2], (global::System.Decimal)arguments[3]).@Box();
                            case -576022763:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@CommitTransactionPreparation((global::System.Guid)arguments[0]).@Box();
                            case -1730429136:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@CancelTransactionPreparation((global::System.Guid)arguments[0]).@Box();
                            case 698066810:
                                return ((global::Orleans.EventSourcing.SimpleInterface.IBankAccount)@grain).@GetBalance().@Box();
                            default:
                                throw new global::System.NotImplementedException("interfaceId=" + -912346303 + ",methodId=" + methodId);
                        }

                    default:
                        throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
                }
            }
            catch (global::System.Exception exception)
            {
                return global::Orleans.Async.TaskUtility.@Faulted(exception);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -912346303;
            }
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
