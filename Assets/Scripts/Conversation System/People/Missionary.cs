using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Missionary : IRole
{
    public int specialPoints;
    public List<AbstractMissionaryAction> actions;

    public Missionary(IEnumerable<AbstractMissionaryAction> actions)
    {
        this.actions = actions.ToList();
    }

    public void Pray()
    {

    }

    public IEnumerable<AbstractAction> GetPossibleActions()
    {
        return actions;
    }

    public IEnumerable<ActionCategory> GetPossibleCategories()
    {
        return actions.Select(a => a.category).Distinct();
    }
}
