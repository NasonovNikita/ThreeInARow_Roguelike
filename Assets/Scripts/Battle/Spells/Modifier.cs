using UnityEngine;

public class Modifier
{
    private int _moves;
    public int Moves => _moves;
    
    private ModifierType _type;
    public ModifierType Type => _type;
    
    private float _value;
    public float Value => _value;

    public Modifier(int moves, ModifierType type, float value)
    {
        _moves = moves;
        _type = type;
        _value = value;
    }
}