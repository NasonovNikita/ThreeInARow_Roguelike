public class Kick : Spell
{
    protected override int ManaCost => 50;
    protected override int Moves { get; set; } = 1;

    public override void Cast()
    {
        //TODO make modifier list to damage enemy 100 HP
    }
}
