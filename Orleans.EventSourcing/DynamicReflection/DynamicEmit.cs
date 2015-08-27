using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Orleans.EventSourcing
{
    internal class DynamicEmit
    {
        private static readonly Dictionary<Type, OpCode> s_Converts;

        static DynamicEmit()
        {
            s_Converts = new Dictionary<Type, OpCode>();
            s_Converts.Add(typeof(sbyte), OpCodes.Conv_I1);
            s_Converts.Add(typeof(short), OpCodes.Conv_I2);
            s_Converts.Add(typeof(int), OpCodes.Conv_I4);
            s_Converts.Add(typeof(long), OpCodes.Conv_I8);

            s_Converts.Add(typeof(byte), OpCodes.Conv_U1);
            s_Converts.Add(typeof(ushort), OpCodes.Conv_U2);
            s_Converts.Add(typeof(uint), OpCodes.Conv_U4);
            s_Converts.Add(typeof(ulong), OpCodes.Conv_U8);

            s_Converts.Add(typeof(float), OpCodes.Conv_R4);
            s_Converts.Add(typeof(double), OpCodes.Conv_R8);

            s_Converts.Add(typeof(bool), OpCodes.Conv_I1);
            s_Converts.Add(typeof(char), OpCodes.Conv_U2);
        }
        private ILGenerator _ilGen;

        public DynamicEmit(DynamicMethod dm)
        {
            _ilGen = dm.GetILGenerator();
        }

        public DynamicEmit(ILGenerator ilGen)
        {
            _ilGen = ilGen;
        }

        public LocalBuilder DeclareLocal(Type type)
        {
            return _ilGen.DeclareLocal(type);
        }

        public Label DefineLabel()
        {
            return _ilGen.DefineLabel();
        }

        public void MarkLabel(Label loc)
        {
            _ilGen.MarkLabel(loc);
        }

        public void LoadElementReference()
        {
            _ilGen.Emit(OpCodes.Ldelem_Ref);
        }

        public void LoadLiteral(int value)
        {
            switch (value)
            {
                case -1:
                    _ilGen.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    _ilGen.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    _ilGen.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    _ilGen.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    _ilGen.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    _ilGen.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    _ilGen.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    _ilGen.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    _ilGen.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    _ilGen.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                _ilGen.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                _ilGen.Emit(OpCodes.Ldc_I4, value);
            }
        }

        public void LoadLiteral(long value)
        {
            _ilGen.Emit(OpCodes.Ldc_I8, value);
        }

        public void LoadLiteral(float value)
        {
            _ilGen.Emit(OpCodes.Ldc_R4, value);
        }

        public void LoadLiteral(double value)
        {
            _ilGen.Emit(OpCodes.Ldc_R8, value);
        }

        public void LoadLiteral(string value)
        {
            _ilGen.Emit(OpCodes.Ldstr, value);
        }

        public void LoadArrayLength()
        {
            _ilGen.Emit(OpCodes.Ldlen);
        }

        public void LoadToken(Type type)
        {
            _ilGen.Emit(OpCodes.Ldtoken, type);
        }

        public void LoadToken<T>()
        {
            LoadToken(typeof(T));
        }

        public void TypeFromHandle()
        {
            MethodInfo translator = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(RuntimeTypeHandle) }, null);
            Call(translator);
        }

        public void LoadType(Type type)
        {
            LoadToken(type);
            TypeFromHandle();
        }

        public void LoadType<T>()
        {
            LoadType(typeof(T));
        }

        public void StringFormat()
        {
            MethodInfo formatter = typeof(String).GetMethod("Format", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string), typeof(object []) }, null);
            Call(formatter);
        }

        public void NewArray<T>(int length)
        {
            LoadLiteral(length);
            _ilGen.Emit(OpCodes.Newarr, typeof(T));
        }

        public void StoreElement()
        {
            _ilGen.Emit(OpCodes.Stelem);
        }

        public void StoreElementReference()
        {
            _ilGen.Emit(OpCodes.Stelem_Ref);
        }

        public void NewObject(ConstructorInfo constructor)
        {
            _ilGen.Emit(OpCodes.Newobj, constructor);
        }

        public void Throw()
        {
            _ilGen.Emit(OpCodes.Throw);
        }

        public void Convert(Type toType)
        {
            _ilGen.Emit(s_Converts[toType]);
        }

        public void Convert<T>()
        {
            Convert(typeof(T));
        }

        public void CastTo(Type toType)
        {
            if (toType.IsValueType)
            {
                _ilGen.Emit(OpCodes.Unbox_Any, toType);
            }
            else
            {
                _ilGen.Emit(OpCodes.Castclass, toType);
            }
        }

        public void CastTo(Type fromType, Type toType)
        {
            if (fromType == toType)
                return;

            if (toType == typeof(void))
            {
                if (fromType != typeof(void))
                    this.Pop();
            }
            else
            {
                if (fromType.IsValueType)
                {
                    if (toType.IsValueType)
                    {
                        Convert(toType);
                        return;
                    }

                    _ilGen.Emit(OpCodes.Box, fromType);
                }

                CastTo(toType);
            }
        }

        public void LoadArgumentAddress(int argumentIndex)
        {
            if (argumentIndex < 256)
                _ilGen.Emit(OpCodes.Ldarga_S, (byte)argumentIndex);
            else
                _ilGen.Emit(OpCodes.Ldarga, argumentIndex);
        }

        public void LoadArgument(int argumentIndex)
        {
            switch (argumentIndex)
            {
                case 0:
                    _ilGen.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    _ilGen.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    _ilGen.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    _ilGen.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    if (argumentIndex < 256)
                        _ilGen.Emit(OpCodes.Ldarg_S, (byte)argumentIndex);
                    else
                        _ilGen.Emit(OpCodes.Ldarg, argumentIndex);
                    break;
            }
        }

        public void LoadLocalAddress(int localIndex)
        {
            if (localIndex < 256)
                _ilGen.Emit(OpCodes.Ldloca_S, (byte)localIndex);
            else
                _ilGen.Emit(OpCodes.Ldloca, localIndex);
        }

        public void LoadLocal(int localIndex)
        {
            switch (localIndex)
            {
                case 0:
                    _ilGen.Emit(OpCodes.Ldloc_0);
                    break;
                case 1:
                    _ilGen.Emit(OpCodes.Ldloc_1);
                    break;
                case 2:
                    _ilGen.Emit(OpCodes.Ldloc_2);
                    break;
                case 3:
                    _ilGen.Emit(OpCodes.Ldloc_3);
                    break;
                default:
                    if (localIndex < 256)
                        _ilGen.Emit(OpCodes.Ldloc_S, (byte)localIndex);
                    else
                        _ilGen.Emit(OpCodes.Ldloc, localIndex);
                    break;
            }
        }

        public void StoreField(FieldInfo field)
        {
            if (field.IsStatic)
            {
                _ilGen.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                _ilGen.Emit(OpCodes.Stfld, field);
            }
        }

        public void StoreLocal(int localIndex)
        {
            switch (localIndex)
            {
                case 0:
                    _ilGen.Emit(OpCodes.Stloc_0);
                    break;
                case 1:
                    _ilGen.Emit(OpCodes.Stloc_1);
                    break;
                case 2:
                    _ilGen.Emit(OpCodes.Stloc_2);
                    break;
                case 3:
                    _ilGen.Emit(OpCodes.Stloc_3);
                    break;
                default:
                    if (localIndex < 256)
                        _ilGen.Emit(OpCodes.Stloc_S, (byte)localIndex);
                    else
                        _ilGen.Emit(OpCodes.Stloc, localIndex);
                    break;
            }
        }

        public void LoadNull()
        {
            _ilGen.Emit(OpCodes.Ldnull);
        }

        public void Return()
        {
            _ilGen.Emit(OpCodes.Ret);
        }

        public void Call(ConstructorInfo constructor)
        {
            _ilGen.Emit(OpCodes.Newobj, constructor);
        }

        public void Call(MethodInfo method)
        {
            if ((method.CallingConvention & CallingConventions.VarArgs) != 0)
            {
                if (method.IsFinal || !method.IsVirtual)
                {
                    _ilGen.EmitCall(OpCodes.Call, method, null);
                }
                else
                {
                    _ilGen.EmitCall(OpCodes.Callvirt, method, null);
                }
            }
            else
            {
                if (method.IsFinal || !method.IsVirtual)
                {
                    _ilGen.Emit(OpCodes.Call, method);
                }
                else
                {
                    _ilGen.Emit(OpCodes.Callvirt, method);
                }
            }
        }

        public void LoadArgument(bool targetIsValueType, int argumentIndex)
        {
            if (targetIsValueType)
            {
                LoadArgumentAddress(argumentIndex);
            }
            else
            {
                LoadArgument(argumentIndex);
            }
        }

        public void LoadField(FieldInfo field)
        {
            if (field.IsStatic)
            {
                _ilGen.Emit(OpCodes.Ldsfld, field);
            }
            else
            {
                _ilGen.Emit(OpCodes.Ldfld, field);
            }
        }

        public void BoxIfNeeded(Type type)
        {
            if (type.IsValueType || type.IsEnum)
            {
                _ilGen.Emit(OpCodes.Box, type);
            }
        }

        public void BoxIfNeeded<T>()
        {
            BoxIfNeeded(typeof(T));
        }

        public void Duplicate()
        {
            _ilGen.Emit(OpCodes.Dup);
        }

        public void Pop()
        {
            _ilGen.Emit(OpCodes.Pop);
        }

        public void Branch(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Br_S, label);
            else
                _ilGen.Emit(OpCodes.Br, label);
        }

        public void BranchEqual(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Beq_S, label);
            else
                _ilGen.Emit(OpCodes.Beq, label);
        }

        public void BranchEqual(Label label, bool isShort, int value)
        {
            LoadLiteral(value);

            if (isShort)
                _ilGen.Emit(OpCodes.Beq_S, label);
            else
                _ilGen.Emit(OpCodes.Beq, label);
        }

        public void BranchEqual(Label label, bool isShort, long value)
        {
            LoadLiteral(value);

            if (isShort)
                _ilGen.Emit(OpCodes.Beq_S, label);
            else
                _ilGen.Emit(OpCodes.Beq, label);
        }

        public void BranchEqual(Label label, bool isShort, float value)
        {
            LoadLiteral(value);

            if (isShort)
                _ilGen.Emit(OpCodes.Beq_S, label);
            else
                _ilGen.Emit(OpCodes.Beq, label);
        }

        public void BranchEqual(Label label, bool isShort, double value)
        {
            LoadLiteral(value);

            if (isShort)
                _ilGen.Emit(OpCodes.Beq_S, label);
            else
                _ilGen.Emit(OpCodes.Beq, label);
        }

        public void BranchLess(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Blt_S, label);
            else
                _ilGen.Emit(OpCodes.Blt, label);
        }

        public void BranchLessEqual(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Ble_S, label);
            else
                _ilGen.Emit(OpCodes.Ble, label);
        }

        public void BranchGreater(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Bgt_S, label);
            else
                _ilGen.Emit(OpCodes.Bgt, label);
        }

        public void BranchGreaterEqual(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Bge_S, label);
            else
                _ilGen.Emit(OpCodes.Bge, label);
        }

        public void BranchIfTrue(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Brtrue_S, label);
            else
                _ilGen.Emit(OpCodes.Brtrue, label);
        }

        // synonym for clearer code
        public void BranchIfNotNull(Label label, bool isShort)
        {
            BranchIfTrue(label, isShort);
        }

        // synonym for clearer code
        public void BranchIfNonZero(Label label, bool isShort)
        {
            BranchIfTrue(label, isShort);
        }

        public void BranchIfFalse(Label label, bool isShort)
        {
            if (isShort)
                _ilGen.Emit(OpCodes.Brfalse_S, label);
            else
                _ilGen.Emit(OpCodes.Brfalse, label);
        }

        // synonym for clearer code
        public void BranchIfNull(Label label, bool isShort)
        {
            BranchIfFalse(label, isShort);
        }

        // synonym for clearer code
        public void BranchIfZero(Label label, bool isShort)
        {
            BranchIfFalse(label, isShort);
        }

        public void Negate()
        {
            _ilGen.Emit(OpCodes.Neg);
        }

        public void Get(bool targetIsValueType, int argumentIndex, SortProperty property)
        {
            LoadArgument(targetIsValueType, argumentIndex);

            if (property.Get != null)
                Call(property.Get); // Get property value.
            else
                _ilGen.Emit(OpCodes.Ldfld, property.Field);  // Get field value
        }
    }
}
