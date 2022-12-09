using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Knowledge Prototype", menuName = "Knowledge Prototype")]
public class Knowledge : ScriptableObject
{
    public new string name;
    // 1f = 100 IQ (average)
    public float iq;
    [Range(0f,1f)]
    public float interest;
    public TopicKnowledge[] topicKnowledge;
    public Dictionary<Topic, float> ToDict()
    {
        Dictionary<Topic, float> dict = new Dictionary<Topic, float>();
        foreach(TopicKnowledge tk in topicKnowledge)
        {
            dict.Add(tk.topic, tk.knowledge);
        }
        return dict;
    }

    public float? GetTopicKnowledge(Topic topic, bool force = false)
    {
        var tk = topicKnowledge.FirstOrDefault(tk => tk.topic == topic);
        if (tk != null && tk.revealed) return tk.knowledge;
        else if (tk.revealed == false && force == true) return tk.knowledge;
        else return null;
    }

    public float GetTopicKnowledgeForced(Topic topic)
    {
        var tk = topicKnowledge.FirstOrDefault(tk => tk.topic == topic);
        if (tk != null) return tk.knowledge;
        else return 0f;
    }

    public bool SetTopicKnowledge(Topic topic, float newKnowlege)
    {
        try
        {
            topicKnowledge.First(tk => tk.topic == topic).knowledge = newKnowlege;
            return true;
        }
        catch
        {
            return false;
        }
    }
}

[System.Serializable]
public class TopicKnowledge {
    public Topic topic;
    public bool revealed = false;
    [Range(0f, 1f)]
    public float knowledge;
}