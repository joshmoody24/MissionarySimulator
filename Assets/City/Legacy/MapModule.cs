using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapModule : IWeighable
{
    public GameObject gameObj;
    public MapModulePrototype prototype;

    public Connector up;
    public Connector right;
    public Connector down;
    public Connector left;

    public int rotation;

    public float weight;

    public MapModule(MapModulePrototype p, GameObject obj, Connector up, Connector right, Connector down, Connector left, int rotation, float weight)
    {
        prototype = p;
        gameObj = obj;
        this.up = up;
        this.right= right;
        this.down = down;
        this.left = left;
        this.rotation = rotation;
        this.weight = weight;
    }

    public bool CanConnect(MapModule other, MapSlot.Direction dir)
    {
        switch (dir)
        {
            case MapSlot.Direction.Up:
                return other.down == up; //&& !up.excluded.Contains(other.prototype) && !other.down.excluded.Contains(prototype);
            case MapSlot.Direction.Right:
                return other.left == right; //&& !right.excluded.Contains(other.prototype) && !other.left.excluded.Contains(prototype);
            case MapSlot.Direction.Down:
                return other.up == down; //&& !down.excluded.Contains(other.prototype) && !other.up.excluded.Contains(prototype);
            case MapSlot.Direction.Left:
                return other.right == left; // && !left.excluded.Contains(other.prototype) && !other.right.excluded.Contains(prototype);
        }

        return false;
    }

    public float GetWeight()
    {
        return weight;
    }
}
