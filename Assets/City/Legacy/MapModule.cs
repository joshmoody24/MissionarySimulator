using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapModule : IWeighable
{
    public GameObject gameObj;
    public MapModulePrototype prototype;

    public Connector north;
    public Connector east;
    public Connector south;
    public Connector west;
    public Connector above;
    public Connector below;

    public MapModulePrototype.Rotation rotation;

    public float weight;

    public MapModule(MapModulePrototype p, GameObject obj, Connector up, Connector right, Connector down, Connector left, Connector above, Connector below, MapModulePrototype.Rotation rotation, float weight)
    {
        prototype = p;
        gameObj = obj;
        this.north = up;
        this.east= right;
        this.south = down;
        this.west = left;
        this.above = above;
        this.below = below;
        this.rotation = rotation;
        this.weight = weight;
    }

    public bool CanConnect(MapModule other, MapSlot.Direction dir)
    {
        switch (dir)
        {
            case MapSlot.Direction.North:
                return other.south == MapModulePrototype.getConnectable(north); //&& !up.excluded.Contains(other.prototype) && !other.down.excluded.Contains(prototype);
            case MapSlot.Direction.East:
                return other.west == MapModulePrototype.getConnectable(east); //&& !right.excluded.Contains(other.prototype) && !other.left.excluded.Contains(prototype);
            case MapSlot.Direction.South:
                return other.north == MapModulePrototype.getConnectable(south); //&& !down.excluded.Contains(other.prototype) && !other.up.excluded.Contains(prototype);
            case MapSlot.Direction.West:
                return other.east == MapModulePrototype.getConnectable(west); // && !left.excluded.Contains(other.prototype) && !other.right.excluded.Contains(prototype);
            case MapSlot.Direction.Above:
                return other.below == MapModulePrototype.getConnectable(above);
            case MapSlot.Direction.Below:
                return other.above == MapModulePrototype.getConnectable(below);
        }

        return false;
    }

    public float GetWeight()
    {
        return weight;
    }
}
