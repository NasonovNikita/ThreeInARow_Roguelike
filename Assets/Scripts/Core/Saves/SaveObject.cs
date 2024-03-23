namespace Core.Saves
{
    [System.Serializable]
    public abstract class SaveObject
    {
        public abstract void Apply();
    }
}