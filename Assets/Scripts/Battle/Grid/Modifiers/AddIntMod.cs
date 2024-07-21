using Battle.Modifiers;

namespace Battle.Grid.Modifiers
{
    public class AddIntMod : Modifier, IIntModifier
    {
        private int value;

        public override bool EndedWork => value == 0;

        public AddIntMod(int value)
        {
            this.value = value;
        }

        int IIntModifier.Modify(int val)
        {
            return val + value;
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is AddIntMod;
        }

        public override void Concat(Modifier other)
        {
            value += ((AddIntMod)other).value;
        }
    }
}