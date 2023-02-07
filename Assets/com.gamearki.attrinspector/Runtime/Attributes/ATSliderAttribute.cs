using System;
using System.Reflection;

namespace GameArki.AttrInspector {

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ATSliderAttribute : Attribute {

        public FieldInfo belongField;

        public string MinName { get; }
        public string MaxName { get; }

        public ATSliderAttribute(string minName, string maxName) {
            this.MinName = minName;
            this.MaxName = maxName;
        }

    }

}
