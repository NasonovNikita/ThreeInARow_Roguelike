public abstract class Item
{
    public abstract ItemType Type { get; set; }

    public abstract void Use();
}