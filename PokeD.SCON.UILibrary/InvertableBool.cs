namespace PokeD.SCON.UILibrary
{
    public class InvertableBool
    {
        private readonly bool _value;

        public bool Value => _value;
        public bool Invert => !_value;

        public InvertableBool(bool b) { _value = b; }

        public static implicit operator InvertableBool(bool b) => new InvertableBool(b);

        public static implicit operator bool (InvertableBool b) => b._value;
    }
}