using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModulePrototype : MonoBehaviour, IWeighable
{
    public Symmetry symmetry;
    public enum Symmetry { X, T, I, L, N }

    public enum Rotation { Degree0, Degree90, Degree180, Degree270 }

    public Connector left;
    public Connector right;
    public Connector up;
    public Connector down;

    public float weight = 1f;

    public List<MapModule> generateAllModules()
    {
        List<MapModule> modules = new List<MapModule>();
        modules.Add(generateModule(0));
        if (symmetry == Symmetry.X) return modules;
        modules.Add(generateModule(90));
        if (symmetry == Symmetry.I) return modules;
        modules.Add(generateModule(270));
        modules.Add(generateModule(180));
        return modules;
    }

    public MapModule generateModule(int rotation)
    {
        int cycles = 0;
        switch (rotation)
        {
            case 0:
                cycles = 0;
                break;
            case 90:
                cycles = 3;
                break;
            case 180:
                cycles = 2;
                break;
            case 270:
                cycles = 1;
                break;
            default:
                cycles = 0;
                break;
        }

        // rotate connectors
        Queue<Connector> shuffler = new Queue<Connector>();
        shuffler.Enqueue(up);
        shuffler.Enqueue(right);
        shuffler.Enqueue(down);
        shuffler.Enqueue(left);
        for (int i = 0; i < cycles; i++)
        {
            shuffler.Enqueue(shuffler.Dequeue());
        }

        var ups = shuffler.Dequeue();
        var rights = shuffler.Dequeue();
        var downs = shuffler.Dequeue();
        var lefts = shuffler.Dequeue();

        return new MapModule(
            this,
            gameObject,
            ups,
            rights,
            downs,
            lefts,
            rotation,
            weight);
    }

    public float GetWeight()
    {
        return weight;
    }
}

public enum Connector
{
    Grass,
    DirtRoadWithGrass,
    RoadWithGrass,
    SmallRoadWithGrass,
    Water,
    WaterWithGrass,
}