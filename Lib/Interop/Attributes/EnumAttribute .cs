namespace HogWarp.Lib.Interop.Attributes
{

    public class EnumAttribute : Attribute
    {
        public string Value { get; }

        public EnumAttribute(string value)
        {
            Value = value;
        }
    }
}
