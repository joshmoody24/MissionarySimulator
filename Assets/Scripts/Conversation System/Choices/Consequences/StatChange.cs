using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class StatChange
{
    public string statName;
    [Range(-0.5f, 0.5f)]
    public float changeAmount;
}