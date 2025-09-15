using System;

namespace TypeReferences
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InterfaceOnlyAttribute : ClassTypeConstraintAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassImplementsAttribute" /> class.
        /// </summary>
        public InterfaceOnlyAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassImplementsAttribute" /> class.
        /// </summary>
        /// <param name="interfaceType">Type of interface that selectable classes must implement.</param>
        public InterfaceOnlyAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
            AllowAbstract = true;
        }


        /// <summary>
        ///     Gets the type of interface that selectable classes must implement.
        /// </summary>
        public Type InterfaceType { get; }


        /// <inheritdoc />
        public override bool IsConstraintSatisfied(Type type)
        {
            if (base.IsConstraintSatisfied(type))
                foreach (Type interfaceType in type.GetInterfaces())
                    if (interfaceType == InterfaceType)
                        return true;
            return false;
        }
    }
}