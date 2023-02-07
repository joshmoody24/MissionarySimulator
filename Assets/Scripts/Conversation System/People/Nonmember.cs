using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Pipes;

public class Nonmember : IRole
{
    public Person person;
    public float interest;
    public float attention;

    [SerializeField]
    private List<NonmemberAction> actions;

    public Nonmember(Person person, IEnumerable<NonmemberAction> actions)
    {
        this.person = person;
        this.actions = actions.ToList();
        attention = 1f;
    }
    public float getEngagement()
    {
        return interest * attention;
    }

    public IEnumerable<Choice> GetPossibleActions()
    {
        return actions.Where(a => a.minAttention <= attention && a.maxAttention >= attention);
    }

    public IEnumerable<ChoiceCategory> GetPossibleCategories()
    {
        return GetPossibleActions().Select(a => a.category).Distinct();
    }

    public void OnLearn(float amount)
    {
        // scaled down by iq because we want to adjust attention by the base amount
        // not iq scaled amount
        Debug.Log(amount + ", " + interest + ", " + person.knowledge.iq);
        // interest of 0.5 is neutral, so less than that reduces attention faster than normal
        // and more reduces attention slower than normal
        float interestFactor = (1f - person.knowledge.interest) * 2;
        ReduceAttention((amount / person.knowledge.iq) * interestFactor);
    }

    public void ReduceAttention(float amount)
    {
        attention -= amount;
        if (attention < 0) attention = 0;
        Debug.Log(person.name + " now has " + attention + " attention.");
    }
}
