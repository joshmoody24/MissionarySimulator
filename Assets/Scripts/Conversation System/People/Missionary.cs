using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Missionary : IRole
{
    public int specialPoints;
    public List<MissionaryAction> actions;

    public Missionary(IEnumerable<MissionaryAction> actions)
    {
        this.actions = actions.ToList();
    }

    public void Pray()
    {

    }

    public IEnumerable<Choice> GetPossibleActions()
    {
        return actions;
    }

    public IEnumerable<ChoiceCategory> GetPossibleCategories()
    {
        return actions.Select(a => a.category).Distinct();
    }

    public void OnLearn(float amount)
    {

    }
}
