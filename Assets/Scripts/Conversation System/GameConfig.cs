using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Config", menuName="Game Config")]
public class GameConfig : ScriptableObject
{
    [Range(0f,1f)]
    public float conversationBurnRate;

}
