using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string firstName;
    public string lastName;
    public Gender gender;
    public IPersonDriver driver;

    public bool playerControlled;

    public List<string> tags;

    public GenericDictionary<string, float> stats;
    public GenericDictionary<string, float> traits;

    [SerializeField]
    public Knowledge startingKnowledge;
    [HideInInspector]
    public Knowledge knowledge;

    void OnEnable()
    {
        if (playerControlled)
        {
            driver = new PlayerDriver(this);
        }
        else driver = new NpcDriver(this);
    }

    public void InstantiateKnowledge()
    {
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
    }

    public void Learn(Topic topic, float baseAmount, float limit=1f)
    {
        if (limit < 0 || limit > 1) limit = 1f;
        float learnableAmount = Mathf.Clamp(limit - knowledge.GetKnowledgeOf(topic), 0f, 1f);
        float amountLearned = Mathf.Clamp(baseAmount, 0f, learnableAmount);
        float newKnowledgeAmount = knowledge.GetKnowledgeOf(topic) + amountLearned;
        knowledge.SetKnowledgeOf(topic, newKnowledgeAmount);
        Debug.Log(name + " learned " + amountLearned + " about " + topic.name);
    }

    public List<Choice> GetPossibleChoices(Character receiver)
    {
        var allChoices = new List<Choice>();
        allChoices.AddRange(Resources.LoadAll<Choice>("Choices"));
        return allChoices
            .Where(c => c.fromTags.Intersect(tags).Count() == c.fromTags.Count)
            .Where(c => c.toTags.Intersect(receiver.tags).Count() == c.toTags.Count)
            .Where(c => c.statRanges.Where(s => 
                stats.ContainsKey(s.statName) && (stats[s.statName] >= s.range.Min) && (stats[s.statName] <= s.range.Max))
                .Count() == c.statRanges.Count())
            // todo: environemnt and topic knowledge
            // todo: scoring and weighted choices
            // temp
            .OrderBy(c => UnityEngine.Random.Range(0f, 1f))
            .Take(ConversationManager.manager.config.choiceLimit)
            .ToList();
    }
}

public enum Gender { Male, Female, Other }