public class PoweredKick : Spell
{
    protected override int ManaCost => 20;
    protected override int Moves => 1;

    private float Value = 0.3f;

    public override void Cast()
    {
        if (manager.State != BattleState.PlayerTurn) return;
        
        player.ChangeMana(-ManaCost);
        player.DamageModifiers.Add(new Modifier(Moves, ModifierType.DamageMul, Value));
    }
}
