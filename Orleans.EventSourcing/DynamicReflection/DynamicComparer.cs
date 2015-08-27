using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Orleans.EventSourcing
{
    public sealed class DynamicIComparable<T> : IComparer<T>
    {
        #region Private Fields
        private DynamicMethod method;
        private Comparison<T> Comparer;
        #endregion

        #region Properties
        public Comparison<T> IComparer
        {
			get
			{
                return Comparer;
			}
        }
        #endregion

        #region Constructors
        public DynamicIComparable(string orderBy)
        {
            Initialize(orderBy);
        }

        public DynamicIComparable(SortProperty[] sortProperties)
        {
            Initialize(sortProperties);
        }
        #endregion

        #region Public Methods
        public void Initialize(string orderBy)
        {
            Initialize(SortProperty.ParseOrderBy(orderBy));
        }

        public void Initialize(SortProperty[] sortProperties)
        {
            SortProperty.BindSortProperties(sortProperties, typeof(T));
            method = CreateDynamicCompareMethod(sortProperties);
            Comparer = (Comparison<T>)method.CreateDelegate(typeof(Comparison<T>));
        }

        #region IIComparable<T> Members
        public int Compare(T x, T y)
        {
            return Comparer.Invoke(x, y);
        }
        #endregion
        #endregion

        #region Private Methods
        private DynamicMethod CreateDynamicCompareMethod(SortProperty[] sortProperties)
        {
            // at this time, the inner loop is (worst case) 39 IL bytes per property with short branches
            const int BytesPerProperty = 39;
            const int PropertiesPerShortBranch = 128 / BytesPerProperty;

            DynamicMethod dm = new DynamicMethod("DynamicCompare"
                , MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard,
                typeof(int), new[] { typeof(T), typeof(T) }, typeof(T), false);
            dm.InitLocals = false;
            DynamicEmit de = new DynamicEmit(dm);

            #region Generate IL for dynamic method
            Dictionary<Type, LocalBuilder> localVariables = new Dictionary<Type, LocalBuilder>();
            bool isValueType = typeof(T).IsValueType;

            if (sortProperties.Length > 0)
            {
                Label breakLabel = de.DefineLabel();

                // For each of the properties we want to check inject the following.
                int numberLeft = sortProperties.Length;
                foreach (SortProperty property in sortProperties)
                {
                    Label continueLabel = de.DefineLabel();
                    Type propertyType = property.ValueType;

                    // Load argument at position 0.
                    de.Get(isValueType, 0, property);

                    // If the type is an Enum, then we need to box it...
                    if (propertyType.IsEnum)
                    {
                        de.BoxIfNeeded(propertyType);
                    }
                    else if (propertyType.IsValueType)
                    {
                        if (!property.IsNullable)
                        {
                            // If the type is a ValueType then we need to inject code to store
                            // it in a local variable, this insures it doesn't get boxed.
                            // Do we have a local variable for this type?
                            LocalBuilder localBuilder;

                            if (!localVariables.TryGetValue(propertyType, out localBuilder))
                            {
                                // Adds a local variable of type x and remember it
                                localBuilder = de.DeclareLocal(propertyType);
                                localVariables.Add(propertyType, localBuilder);
                            }

                            // This local variable is for handling value types of type x.
                            int localIndex = localBuilder.LocalIndex;

                            de.StoreLocal(localIndex);       // Store the value in the local var at position x.
                            de.LoadLocalAddress(localIndex); // Load the address of the local
                        }
                    }
                    else
                    {
                        // value is an reference type
                        Label leftNotNull = de.DefineLabel();
                        Label rightNotNull = de.DefineLabel();
                        de.Duplicate(); // left is now on stack twice.
                        de.BranchIfNotNull(leftNotNull, true);

                        // Left is null
                        de.Pop(); // discard second copy of left

                        // Get right...
                        de.Get(isValueType, 1, property);

                        // and check if right is not null
                        de.BranchIfNotNull(rightNotNull, true);

                        // We know that right is null too, thus they are equal
                        de.LoadLiteral(0);
                        de.Branch(continueLabel, numberLeft <= PropertiesPerShortBranch);

                        // Okay, right is NOT null, left is less
                        de.MarkLabel(rightNotNull);
                        de.LoadLiteral(-1);
                        de.Branch(continueLabel, numberLeft <= PropertiesPerShortBranch);
                        
                        de.MarkLabel(leftNotNull);
                    }

                    // Load argument at position 1.
                    de.Get(isValueType, 1, property);

                    // If the type is an Enum, then we need to box it...
                    if (propertyType.IsEnum)
                    {
                        de.BoxIfNeeded(propertyType);
                    }

                    // Compare the top 2 items in the evaluation stack and push the return value onto the stack.
                    if (property.IsNullable)
                    {
                        // use Nullable's Compare method
                        MethodInfo elementCompare = typeof(Nullable).GetMethod("Compare");
                        elementCompare = elementCompare.MakeGenericMethod(propertyType.GetGenericArguments()[0]);
                        de.Call(elementCompare);
                    }
                    else
                    {
                        //IComparable<T>.Default;
                        // use propertyType's CompareTo method
                        MethodInfo elementCompare = propertyType.GetMethod("CompareTo", new[] { propertyType });
                        de.Call(elementCompare);
                    }

                    de.MarkLabel(continueLabel);

                    // If the sort should be descending we need to flip the result of the comparison.
                    if (property.Descending)
                        de.Negate();

                    if (--numberLeft > 0)
                    {
                        de.Duplicate();     // save a copy of the return value
                        // Is the result is not zero, we're done so break out of the loop.
                        de.BranchIfNonZero(breakLabel, numberLeft <= PropertiesPerShortBranch); 
                        de.Pop();           // discard the (known 0) copy of the return value
                    }
                }

                de.MarkLabel(breakLabel); // This is the spot where the label we created earlier should be added.
            }
            else
            {
                // if there are no properties, call object IComparable directly...
                de.LoadArgument(isValueType, 0);    // Load argument at position 0.
                de.LoadArgument(1);                 // Load argument at position 1.
                MethodInfo instanceCompare = typeof(T).GetMethod("CompareTo", new[] { typeof(T) });
                de.Call(instanceCompare);
            }

            de.Return(); // Return the value.
            #endregion

            return dm;
        }
        #endregion
    }

    /// <summary>
    /// public struct to carry the sorting properties.
    /// </summary>
    public struct SortProperty
    {
        #region Properties
        private string name;
        public string Name
        {
			get
			{
				return name;
			}
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("A property cannot have an empty name.", "value");

                name = value.Trim();
            }
        }

        private bool descending;
        public bool Descending
        {
			get
			{
				return descending;
			}
			set
			{
				descending = value;
			}
        }

        public static bool IsComparable(Type valueType)
        {
            bool isNullable;
            return IsComparable(valueType, out isNullable);
        }
        
        public static bool IsComparable(Type valueType, out bool isNullable)
        {
            isNullable = valueType.IsGenericType
                    && ! valueType.IsGenericTypeDefinition
                    && valueType.IsAssignableFrom(typeof(Nullable<>).MakeGenericType(valueType.GetGenericArguments()[0]));

            return (typeof(IComparable).IsAssignableFrom(valueType)
                    || typeof(IComparable<>).MakeGenericType(valueType).IsAssignableFrom(valueType)
                    || isNullable);
        }

        #region publics
        private Type valueType;
        public Type ValueType
        {
			get
			{
				return valueType;
			}
            private set
            {
                valueType = value;

                if (!IsComparable(value, out isNullable))
                {
                    throw new NotSupportedException("The type \""
                        + value.FullName
                        + "\" of the property \""
                        + this.Name
                        + "\" does not implement IComparable, IComparible<T> or is Nullable<T>.");
                }
            }
        }

        private MethodInfo get;
        public MethodInfo Get
        {
			get
			{
				return get;
			}
			private set
			{
				get = value;
			}
		}

        private FieldInfo field;
        public FieldInfo Field
        {
			get
			{
				return field;
			}
			private set
			{
				field = value;
			}
		}

        private bool isNullable;
        public bool IsNullable
        {
			get
			{
				return isNullable;
			}
		}
        #endregion
        #endregion

        #region Static methods
        public static SortProperty[] ParseOrderBy(string orderBy)
        {
            if (orderBy == null)
                throw new ArgumentException("The orderBy clause may not be null.", "orderBy");

            string[] properties = orderBy.Split(new[] { ',' } , StringSplitOptions.RemoveEmptyEntries);
            SortProperty[] sortProperties = new SortProperty[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                bool descending = false;
                string property = properties[i].Trim();
                string[] propertyElements = property.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (propertyElements.Length > 1)
                {
                    if (propertyElements[1].Equals("DESC", StringComparison.OrdinalIgnoreCase))
                    {
                        descending = true;
                    }
                    else if (propertyElements[1].Equals("ASC", StringComparison.OrdinalIgnoreCase))
                    {
                        // already set to descending = false;
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected sort order type \"" + propertyElements[1] + "\" for \"" + propertyElements[0] + "\"", "orderBy");
                    }
                }

                sortProperties[i] = new SortProperty(propertyElements[0], descending);
            }

            return sortProperties;
        }
        #endregion

        #region Constructors
        public SortProperty(string propertyName, bool sortDescending)
        {
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("A property cannot have an empty name.", "propertyName");

            name = propertyName;
            descending = sortDescending;

            // we set these when accessor validated
            valueType = null;
            get = null;
            field = null;
            isNullable = false;
        }
        #endregion

        #region publics
        public static void BindSortProperties(SortProperty[] sortProperties, Type instanceType)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            if (sortProperties == null)
                sortProperties = new SortProperty[0];

            if (sortProperties.Length > 0)
            {
                for (int index = 0; index < sortProperties.Length; index++)
                {
                    string propertyName = sortProperties[index].Name;
                    PropertyInfo propertyInfo = instanceType.GetProperty(propertyName, BindingFlags.GetProperty | flags);

                    if (propertyInfo != null)
                    {
                        sortProperties[index].ValueType = propertyInfo.PropertyType;
                        sortProperties[index].Get = propertyInfo.GetGetMethod(true);
                    }
                    else
                    {
                        FieldInfo fieldInfo = instanceType.GetField(propertyName, BindingFlags.GetField | flags);

                        if (fieldInfo != null)
                        {
                            sortProperties[index].ValueType = fieldInfo.FieldType;
                            sortProperties[index].Field = fieldInfo;
                        }
                        else
                        {
                            throw new ArgumentException("No public property or field named \""
                                + propertyName
                                + "\" was found in type \""
                                + instanceType.FullName
                                + "\".");
                        }
                    }
                }
            }
            else
            {
                if (!IsComparable(instanceType))
                    throw new NotSupportedException("The type \""
                        + instanceType.FullName
                        + "\" does not implement IComparable, IComparable<T> nor is a Nullable<T>.");
            }
        }
        #endregion
    }
}