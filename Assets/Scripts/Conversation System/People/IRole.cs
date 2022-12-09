using System.Collections.Generic;
using UnityEngine;

public interface IRole
{
    public IEnumerable<AbstractAction> GetPossibleActions();
    public IEnumerable<ActionCategory> GetPossibleCategories();

    // when a person gains knowledge, run some side effects
    public void OnLearn(float amount);
}