using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Nonmember : IRole
{
    public float interest;
    public float energy;
    [SerializeField]
    private List<AbstractNonmemberAction> actions;

    public Nonmember(IEnumerable<AbstractNonmemberAction> actions)
    {
        this.actions = actions.ToList();
    }
    public float getEngagement()
    {
        return interest * energy;
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
