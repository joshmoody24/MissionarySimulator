using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Knowledge Prototype", menuName = "Knowledge Prototype")]
public class Knowledge : ScriptableObject
{
    private Dictionary<Topic, Knol> knowls;
    {
        Dictionary<Topic, float> dict = new Dictionary<Topic, float>();
        foreach(Knol tk in topicKnowledge)
        {
            dict.Add(tk.topic, tk.knowledge);
        }
        return dict;
    }

    public Knowl? GetKnowledgeOf(Topic topic, bool force = false)
    {
        var tk = topicKnowledge.FirstOrDefault(tk => tk.topic == topic);
        if (tk != null && tk.revealed) return tk.knowledge;
        else if (tk.revealed == false && force == true) return tk.knowledge;
        else return null;
    }

    public bool SetKnowledgeOf(Topic topic, float newKnowlege)
    {
        try
        {
            knowls.First(tk => tk.topic == topic).knowledge = newKnowlege;
            return true;
        }
        catch
        {
            return false;
        }
    }
}

[System.Serializable]
public struct Knol {
    public bool revealed;
    [Range(0f, 1f)]
    public float knowledge;
}