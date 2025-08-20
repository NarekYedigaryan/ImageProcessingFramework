using System;

namespace ImageProcessingFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectParameterAttribute : Attribute
    {
        public string Name { get; }
        public Type ParameterType { get; }
        public object DefaultValue { get; }
        public string Description { get; }

        public EffectParameterAttribute(string name, Type parameterType, object defaultValue = null, string description = "")
        {
            Name = name;
            ParameterType = parameterType;
            DefaultValue = defaultValue;
            Description = description;
        }
    }
}