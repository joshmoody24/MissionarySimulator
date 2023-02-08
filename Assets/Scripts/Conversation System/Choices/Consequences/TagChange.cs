using System;

[Serializable]
public class TagChange
{
    public string tag;
    public TagChangeType type;
}

public enum TagChangeType
{
    Add,
    Remove,
}