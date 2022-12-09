using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missionary Action", menuName = "Speech Action/Nonmember Action")]
public class NonmemberAction : AbstractAction
{
    //[Range(0f,1f)]
    //public float requiredEngagement;
    [Range(0f, 1f)]
    public float minAttention = 0f;
    [Range(0f, 1f)]
    public float maxAttention = 1f;
}
