using System;

[Serializable]
public abstract class ActiveAction
{
    public abstract void Use();

    public ActiveAction() {}
}