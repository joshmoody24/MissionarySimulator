using System;
using UnityEngine;

public struct Trait
{
    public string name;
    [Range(-1f, 1f)]
    public float value;
}