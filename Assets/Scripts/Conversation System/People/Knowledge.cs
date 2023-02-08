using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

[CreateAssetMenu(fileName = "New Knowledge Prototype", menuName = "Knowledge Prototype")]
public class Knowledge : ScriptableObject
{
    private Dictionary<Topic, float> knols;

    public float GetKnowledgeOf(Topic topic)
    {
        float k = 0;
        knols.TryGetValue(topic, out k);
        return k;
    }

    public void SetKnowledgeOf(Topic topic, float newKnowlege)
    {
        knols[topic] = newKnowlege;
    }
}