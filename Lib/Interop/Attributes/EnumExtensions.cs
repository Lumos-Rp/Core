using System.Reflection;

namespace HogWarp.Lib.Interop.Attributes
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString())!;
            EnumAttribute attribute = (EnumAttribute)Attribute.GetCustomAttribute(field, typeof(EnumAttribute))!;
            return attribute?.Value ?? value.ToString();
        }
    }
}
