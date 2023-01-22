using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapModulePrototype : MonoBehaviour, IWeighable
{
    public Symmetry symmetry;
    public enum Symmetry { X, T, I, L, N }

    public enum Rotation { Degree0 = 0, Degree90 = 90, Degree180 = 180, Degree270 = 270 }

    public static Connector getConnectable(Connector source)
    {
        switch (source)
        {
            case Connector.GrassMidgrassTallgrass:
                return Connector.TallgrassMidgrassGrass;
            case Connector.TallgrassMidgrassGrass:
                return Connector.GrassMidgrassTallgrass;
            default:
                return source;
        }
    }

    public bool selectable = true;

    public Connector west;
    public Connector east;
    public Connector north;
    public Connector south;
    public Connector above;
    public Connector below;

    public float weight = 1f;

    public List<MapModule> generateAllModules()
    {
        List<MapModule> modules = new List<MapModule>();
        modules.Add(generateModule(Rotation.Degree0));
        if (symmetry == Symmetry.X) return modules;
        modules.Add(generateModule(Rotation.Degree90));
        if (symmetry == Symmetry.I) return modules;
        modules.Add(generateModule(Rotation.Degree180));
        modules.Add(generateModule(Rotation.Degree270));
        return modules;
    }

    public MapModule generateModule(Rotation rotation)
    {
        int cycles = 0;
        switch (rotation)
        {
            case Rotation.Degree0:
                cycles = 0;
                break;
            case Rotation.Degree90:
                cycles = 3;
                break;
            case Rotation.Degree180:
                cycles = 2;
                break;
            case Rotation.Degree270:
                cycles = 1;
                break;
            default:
                cycles = 0;
                break;
        }

        // rotate connectors
        Queue<Connector> shuffler = new Queue<Connector>();
        shuffler.Enqueue(north);
        shuffler.Enqueue(east);
        shuffler.Enqueue(south);
        shuffler.Enqueue(west);

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
            above,
            below,
            rotation,
            weight);
    }

    public float GetWeight()
    {
        return weight;
    }

    public Rotation Rotate90(Rotation rot)
    {
        switch (rot)
        {
            case Rotation.Degree0:
                return Rotation.Degree90;
            case Rotation.Degree90:
                return Rotation.Degree180;
            case Rotation.Degree180:
                return Rotation.Degree270;
            case Rotation.Degree270:
                return Rotation.Degree0;
            default:
                throw new System.Exception("Rotation laws of physics broken");
        }

    }
}

public enum Connector
{
    Any = 0,
    GrassGrassGrass = 10,
    GrassDirtGrass = 20,
    GrassRoadGrass = 30,
    GrassMiniroadGrass = 40,
    WaterWaterWater = 50,
    GrassWaterGrass = 60,
    GrassMidgrassTallgrass = 70,
    TallgrassMidgrassGrass = 80,
    TallgrassTallgrassTallgrass = 90,
    TallgrassTalldirtTallgrass = 100,
    Air = 110,
    SlopeCap = 120,
}