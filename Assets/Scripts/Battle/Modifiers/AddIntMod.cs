namespace Battle.Modifiers
{
    /// This class is created because of DRY rule.
    /// Just adds its value to a modified one. Also see IIntModifier.
    public class AddIntMod : Modifier, IIntModifier
    {
        private int _value;

        public AddIntMod(int value) => _value = value;

        public override bool EndedWork => _value == 0;

        int IIntModifier.Modify(int val) => val + _value;

        protected override bool HiddenCanConcat(Modifier other) => other is AddIntMod;

        protected override void HiddenConcat(Modifier other)
        {
            _value += ((AddIntMod)other)._value;
        }
    }
}