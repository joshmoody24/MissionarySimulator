using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MinMax
{
    public float Min, Max;

    public MinMax(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public float Clamp(float value)
    {
        return Mathf.Clamp(value, Min, Max);
    }

    public float RandomValue
    {
        get { return UnityEngine.Random.Range(Min, Max); }
    }
}

public class MinMaxAttribute : PropertyAttribute
{
    public float Min, Max;

    public MinMaxAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

