using UnityEngine;

[CreateAssetMenu(fileName = "Good", menuName = "Good")]
public class Good : ScriptableObject
{
    public Object good;
    public GoodType type;
    public int price;
}

public enum GoodType
{
    Item,
    Spell
}