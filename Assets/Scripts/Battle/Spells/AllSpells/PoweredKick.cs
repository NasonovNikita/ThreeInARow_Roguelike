public class PoweredKick : Spell
{
    protected override int ManaCost => 40;
    
    protected override int Moves => 1;

    private const float Value = 1.5f;

    public override void Cast()
    {
        if (manager.State != BattleState.PlayerTurn) return;
        
        player.ChangeMana(-ManaCost);
        player.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun));
        player.DamageModifiers.Add(new Modifier(Moves + 1, ModifierType.DamageMul, Value));
        manager.EndTurn();
    }
}