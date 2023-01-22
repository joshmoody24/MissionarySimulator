using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class MapSlot
{
    public List<MapModule> possibleModules;
    public Vector3Int position;

    public MapSlot neighborNorth;
    public MapSlot neighborEast;
    public MapSlot neighborSouth;
    public MapSlot neighborWest;
    public MapSlot neighborAbove;
    public MapSlot neighborBelow;

    public bool collapsed = false;

    public MapSlot(List<MapModule> possibleModules, int x, int y, int z)
    {
        this.position = new Vector3Int(x, y, z);
        this.possibleModules = possibleModules;
    }

    public void SetNeighbors((MapSlot, MapSlot, MapSlot, MapSlot, MapSlot, MapSlot) slots)
    {
        neighborNorth = slots.Item1;
        neighborEast = slots.Item2;
        neighborSouth = slots.Item3;
        neighborWest = slots.Item4;
        neighborAbove = slots.Item5;
        neighborBelow = slots.Item6;
    }

    public void Collapse(MapModule forcedModule=null)
    {
        MapModule selected = forcedModule == null ? WeightedRandomChoice(possibleModules) : forcedModule;
        Debug.Log("Collapsed " + position.x + "," + position.y + "," + position.z + " to become " + selected.gameObj.name);
        possibleModules = new List<MapModule>() { selected };
        neighborNorth?.Reevaluate();
        neighborEast?.Reevaluate();
        neighborSouth?.Reevaluate();
        neighborWest?.Reevaluate();
        collapsed = true;
    }

    // returns true if possibilities changed
    public bool Reevaluate()
    {
        if (collapsed) return false;
        // get possible connections
        List<MapModule> northPossibilities = neighborNorth?.possibleModules.ToList();
        List<MapModule> eastPossibilities = neighborEast?.possibleModules.ToList();
        List<MapModule> southPossibilities = neighborSouth?.possibleModules.ToList();
        List<MapModule> westPossibilities = neighborWest?.possibleModules.ToList();
        List<MapModule> abovePossibilities = neighborAbove?.possibleModules.ToList();
        List<MapModule> belowPossibilities = neighborBelow?.possibleModules.ToList();


        List<MapModule> newlyPossible = possibleModules
            .Where(m => northPossibilities == null || northPossibilities.Where(c => c.CanConnect(m, Direction.South)).Count() > 0)
            .Where(m => eastPossibilities == null || eastPossibilities.Where(c => c.CanConnect(m, Direction.West)).Count() > 0)
            .Where(m => southPossibilities == null || southPossibilities.Where(c => c.CanConnect(m, Direction.North)).Count() > 0)
            .Where(m => westPossibilities == null || westPossibilities.Where(c => c.CanConnect(m, Direction.East)).Count() > 0)
            .Where(m => abovePossibilities == null || abovePossibilities.Where(c => c.CanConnect(m, Direction.Below)).Count() > 0)
            .Where(m => belowPossibilities == null || belowPossibilities.Where(c => c.CanConnect(m, Direction.Above)).Count() > 0)
            .ToList();

        bool changed = newlyPossible.Count < possibleModules.Count;
        possibleModules = newlyPossible;
        if (possibleModules.Count == 1) collapsed = true;
        // no possibilities is okay because it's just air
        // if(possibleModules.Count == 0) throw new System.Exception("Map slot (" + position.x + "," + position.y + "," + position.z + ") has no possible modules");

        if (changed)
        {
            neighborNorth?.Reevaluate();
            neighborEast?.Reevaluate();
            neighborSouth?.Reevaluate();
            neighborWest?.Reevaluate();
            neighborAbove?.Reevaluate();
            neighborBelow?.Reevaluate();
        }

        return changed;

    }

    private T RandomChoice<T>(List<T> list)
    {
        if (list.Count == 0) throw new System.Exception("Tried to choose from empty list");
        return list[Random.Range(0, list.Count)];
    }

    private T WeightedRandomChoice<T>(List<T> list) where T : IWeighable
    {
        if (list.Count == 0) throw new System.Exception("Tried to choose from empty list");
        float totalWeight = 0;
        foreach(T w in list)
        {
            totalWeight += w.GetWeight();
        }
        float chosenWeight = Random.Range(0f, totalWeight);
        foreach(T w in list)
        {
            chosenWeight -= w.GetWeight();
            if (chosenWeight < 0) return w;
        }
        throw new System.Exception("Weighted random choice broke math");
    }

    public enum Direction { North, East, South, West, Above, Below };
}
