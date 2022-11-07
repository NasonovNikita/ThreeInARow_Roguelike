using System.Collections.Generic;
using System.Linq;

public class Player : Unit
{
    public int Damage(Dictionary<GemType, int> destroyed)
    {
        return destroyed.Sum(type => type.Value) * BaseDamage;
    }
}
