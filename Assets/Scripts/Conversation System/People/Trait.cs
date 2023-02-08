using System;
using UnityEngine;

[Serializable]
public struct Trait
{
    public string name;
    [Range(-1f, 1f)]
    public float value;
}