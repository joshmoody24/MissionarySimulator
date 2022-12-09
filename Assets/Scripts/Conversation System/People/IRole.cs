using System.Collections.Generic;
using UnityEngine;

public interface IRole
{
    public IEnumerable<AbstractAction> GetPossibleActions();
    public IEnumerable<ActionCategory> GetPossibleCategories();
}