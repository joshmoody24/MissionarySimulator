using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public GameObject cityTiles;
    public int cityWidth;
    public int cityHeight;
    public QuantumRoad[,] city;
    private List<GameObject> temps;

    public int maxAttempts = 3;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Starting city generation");
            // usually works after the first try, but not always
            for(int i = 0; i < maxAttempts; i++)
            {
                try
                {
                    GenerateCity();
                    break;
                }
                catch
                {
                    Debug.Log("City generation failed");
                }
            }
            if(transform.childCount == 0)
            {
                Debug.Log("Max number of city generation attempts reached");
            }
        }
    }

    public void GenerateCity()
    {
        // delete previous city
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // construct a set of tiles from the cityTiles gameobject
        Debug.Log("Generating tile set");
        HashSet<RoadTile> tiles = new HashSet<RoadTile>();
        foreach(RoadTile tile in cityTiles.transform.GetComponentsInChildren<RoadTile>())
        {
            foreach(RoadTile permutation in GeneratePermutations(tile))
            tiles.Add(permutation);
        }

        // start with each tile being able to be any tile
        Debug.Log("Initializing quantum roads");
        city = new QuantumRoad[cityWidth,cityHeight];
        for(int i = 0; i < cityWidth; i++)
        {
            for(int j = 0; j < cityHeight; j++)
            {
                city[i, j] = new QuantumRoad(new HashSet<RoadTile>(tiles, tiles.Comparer));
            }
        }

        while(GetNumCollapsed() != cityWidth * cityHeight)
        {
            // randomly collapse a tile that has low entropy
            Vector3Int toCollapse = GetLowestEntropyQuantumRoad();
            CollapseTile(toCollapse.x, toCollapse.z);
        }

        // spawn in the tiles
        for(int i = 0; i < cityWidth; i++)
        {
            for(int j = 0; j < cityHeight; j++)
            {
                GameObject tile = Instantiate(city[i, j].GetTile().gameObject, transform.position + new Vector3(i*RoadTile.width, 0*RoadTile.width, j*RoadTile.width), city[i, j].GetTile().transform.rotation, transform);
                
                // rotate the spawned tile accordingly
                RoadTile rt = tile.GetComponentInChildren<RoadTile>();
                switch (rt.rotation)
                {
                    case Rotation.rot90:
                        tile.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case Rotation.rot180:
                        tile.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case Rotation.rot270:
                        tile.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                }
            }
        }

        foreach(GameObject temp in temps)
        {
            Destroy(temp);
        }
    }

    int GetNumCollapsed()
    {
        int num = 0;
        for (int i = 0; i < cityWidth; i++)
        {
            for (int j = 0; j < cityHeight; j++)
            {
                if (city[i, j].collapsed) num++;
            }
        }
        return num;
    }

    // does not find roads that are fully collapsed
    Vector3Int GetLowestEntropyQuantumRoad()
    {
        int minEntropy = int.MaxValue;
        HashSet<Vector3Int> minTiles = new HashSet<Vector3Int>();

        for(int i = 0; i < cityWidth; i++)
        {
            for(int j = 0; j < cityHeight; j++)
            {
                int entropy = city[i, j].GetEntropy();
                if (entropy <= minEntropy && city[i,j].collapsed == false)
                {
                    minEntropy = entropy;
                    minTiles.Add(new Vector3Int(i, 0, j));
                }
            }
        }

        int randIndex = Random.Range(0, minTiles.Count);
        Vector3Int selected = minTiles.ElementAt(randIndex);
        return selected;
    }

    void CollapseTile(int x, int z)
    {
        city[x, z].Collapse();
        UpdateTiles(x, z);
    }

    // recursively update tiles
    void UpdateTiles(int startX, int startZ)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        UpdateTile(startX-1, startZ);
        UpdateTile(startX +1, startZ);
        UpdateTile(startX, startZ-1);
        UpdateTile(startX, startZ+1);

        Debug.Log("updating tiles after collapse took " + watch.ElapsedMilliseconds);
        watch.Stop();

        // reset all to not updated
        foreach (QuantumRoad r in city)
        {
            r.updated = false;
        }
    }

    // cause a tile to look at its neighbors and change its possible states
    void UpdateTile(int x, int z)
    {
        if (x < 0 || x >= cityWidth || z < 0 || z >= cityHeight) return;
        if (city[x, z].updated) return;
        // make sure it's only updated once
        city[x, z].updated = true;

        // check neighbors and update state accordingly
        QuantumRoad neighborLeft = x > 0 ? city[x - 1, z] : null;
        QuantumRoad neighborRight = x < cityWidth - 1 ? city[x + 1, z] : null;
        QuantumRoad neighborDown = z > 0 ? city[x, z - 1] : null;
        QuantumRoad neighborUp = z < cityHeight - 1 ? city[x, z + 1] : null;
        bool changed = city[x,z].UpdateTiles(neighborRight, neighborLeft, neighborUp, neighborDown);
        if (changed)
        {
            // update neightbors if this cell changed
            if (x > 0) UpdateTile(x - 1, z);
            if (x < cityWidth - 1) UpdateTile(x + 1, z);
            if (z > 0) UpdateTile(x, z - 1);
            if (z < cityHeight - 1) UpdateTile(x, z + 1);
        }
    }

    HashSet<RoadTile> GeneratePermutations(RoadTile tile)
    {
        if (temps == null) temps = new List<GameObject>();
        HashSet<RoadTile> permutations = new HashSet<RoadTile>
        {
            // rot 0
            tile
        };

        RoadTile rt = tile.GetComponentInChildren<RoadTile>();

        if (rt.IsFullySymmetrical() == false)
        {
            // rot 90
            GameObject rot90 = Instantiate(tile.gameObject);
            RoadTile r90 = rot90.GetComponentInChildren<RoadTile>();
            r90.rotation = Rotation.rot90;
            Edge temp90 = r90.edges.posX;
            r90.edges.posX = r90.edges.posZ;
            r90.edges.posZ = r90.edges.negX;
            r90.edges.negX = r90.edges.negZ;
            r90.edges.negZ = temp90;
            permutations.Add(r90);
            temps.Add(rot90);

            // rot 270
            GameObject rot270 = Instantiate(tile.gameObject);
            RoadTile r270 = rot270.GetComponentInChildren<RoadTile>();
            r270.rotation = Rotation.rot270;
            Edge temp270 = r270.edges.posX;
            r270.edges.posX = r270.edges.posZ;
            r270.edges.posZ = r270.edges.negX;
            r270.edges.negX = r270.edges.negZ;
            r270.edges.negZ = temp270;
            permutations.Add(r270);
            temps.Add(rot270);
        }

        if (rt.IsHalfSymmetrical() == false){
            // rot 180
            GameObject rot180 = Instantiate(tile.gameObject);
            RoadTile r180 = rot180.GetComponentInChildren<RoadTile>();
            r180.rotation = Rotation.rot180;
            Edge temp180 = r180.edges.posX;
            r180.edges.posX = r180.edges.posZ;
            r180.edges.posZ = r180.edges.negX;
            r180.edges.negX = r180.edges.negZ;
            r180.edges.negZ = temp180;
            permutations.Add(r180);
            temps.Add(rot180);
        }

        return permutations;
    }
}

// represents a road in an undetermined state
public class QuantumRoad {

    // used to track if updated state (frequently fluctuates)
    public bool updated = false;

    public bool collapsed = false;

    // the possible tiles this road tile can be
    public HashSet<RoadTile> tiles;

    public QuantumRoad(HashSet<RoadTile> tiles)
    {
        this.tiles = tiles;
    }

    public void Collapse()
    {
        int randIndex = Random.Range(0, tiles.Count);
        tiles = new HashSet<RoadTile>() { tiles.ElementAt(randIndex) };
        collapsed = true;
    }

    public HashSet<RoadTile> GetPossibleTiles()
    {
        return tiles;
    }

    public int GetEntropy()
    {
        return tiles.Count;
    }

    // return tile if only one option, otherwise null
    public RoadTile GetTile()
    {
        if(tiles.Count == 1)
        {
            return tiles.ElementAt(0);
        }
        return null;
    }

    public HashSet<Edge> GetEdges(Direction direction)
    {
        switch (direction)
        {
            case Direction.posZ:
                return tiles.Select(g => g.GetComponentInChildren<RoadTile>().edges.posZ).Distinct().ToHashSet();
            case Direction.posX:
                return tiles.Select(g => g.GetComponentInChildren<RoadTile>().edges.posX).Distinct().ToHashSet();
            case Direction.negX:
                return tiles.Select(g => g.GetComponentInChildren<RoadTile>().edges.negX).Distinct().ToHashSet();
            case Direction.negZ:
                return tiles.Select(g => g.GetComponentInChildren<RoadTile>().edges.negZ).Distinct().ToHashSet();
            default:
                throw new System.Exception("Improper direction");
        }
    }

    // update possible tiles to match neightbors
    // returns number of remaining possiblities
    public bool UpdateTiles(QuantumRoad posX, QuantumRoad negX, QuantumRoad posZ, QuantumRoad negZ)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        if (collapsed) return false;
        int previousEntropy = GetEntropy();
        if(posX != null) tiles = tiles.Where(t => posX.GetEdges(Direction.negX).Contains(t.edges.posX)).ToHashSet();
        if(negX != null) tiles = tiles.Where(t => negX.GetEdges(Direction.posX).Contains(t.edges.negX)).ToHashSet();
        if(posZ != null) tiles = tiles.Where(t => posZ.GetEdges(Direction.negZ).Contains(t.edges.posZ)).ToHashSet();
        if(negZ != null) tiles = tiles.Where(t => negZ.GetEdges(Direction.posZ).Contains(t.edges.negZ)).ToHashSet();
        if (tiles.Count == 1)
        {
            collapsed = true;
        }
        watch.Stop();
        if (watch.ElapsedMilliseconds > 1) Debug.Log("Updating single tile took " + watch.ElapsedMilliseconds);
        return GetEntropy() < previousEntropy;
    }
}
