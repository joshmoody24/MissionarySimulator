using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Knowledge Prototype", menuName = "Knowledge Prototype")]
public class Knowledge : ScriptableObject
{
    public new string name;
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

    public float GetTopicKnowledge(Topic topic)
    {
        return topicKnowledge.FirstOrDefault(tk => tk.topic == topic)?.knowledge ?? 0f;
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
    [Range(0f, 1f)]
    public float knowledge;
}