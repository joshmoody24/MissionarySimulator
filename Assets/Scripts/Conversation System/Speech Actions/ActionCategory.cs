using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Category", menuName = "Action Category")]
public class ActionCategory : ScriptableObject
{
    public new string name;
    public string description;
    public int order;
}