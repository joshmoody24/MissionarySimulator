using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    public Transform modulePrototypeParent;
    // public List<MapModulePrototype> modulePrototypes;
    public Vector3Int mapSize;
    public int sizeOfTile = 42;
    public int heightOfTile = 1;

    public List<MapModule> generatedModules;

    public Dictionary<Vector3Int, MapSlot> slots;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // destroy previous city
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GenerateModules();
            InitializeMap();
            int iterations = mapSize.x * mapSize.y * mapSize.z;
            for (int i = 0; i < iterations; i++)
            {
                // if(slots[Vector3Int.up].possibleModules.Count == 1) Debug.Log(slots[Vector3Int.up].possibleModules[0].gameObj.name);
                GetLowestEntropy()?.Collapse();
            }
            Visualize();
            Debug.Log("City Generation Completed");
        }
    }

    private void Visualize()
    {
        foreach (MapSlot slot in slots.Values){
            if(slot.possibleModules.Count == 1)
            {
                GameObject module = Instantiate(slot.possibleModules[0].gameObj, new Vector3(slot.position.x * sizeOfTile, slot.position.y * heightOfTile, slot.position.z * sizeOfTile), Quaternion.identity, transform);
                module.transform.Rotate(new Vector3(0, (int)slot.possibleModules[0].rotation, 0));
            }
        }
    }

    private void GenerateModules()
    {
        List<MapModulePrototype> modulePrototypes = new List<MapModulePrototype>();
        foreach(Transform child in modulePrototypeParent)
        {
            var proto = child.GetComponent<MapModulePrototype>();
            if(proto.selectable) modulePrototypes.Add(proto);
        }

        generatedModules = new List<MapModule>();
        foreach (MapModulePrototype prototype in modulePrototypes)
        {
            generatedModules.AddRange(prototype.generateAllModules());
        }
    }

    private void InitializeMap()
    {
        slots = new Dictionary<Vector3Int, MapSlot>();
        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                for(int z = 0; z < mapSize.z; z++)
                {
                    slots[new Vector3Int(x, y, z)] = new MapSlot(generatedModules.ToList(), x, y, z);
                }
            }
        }
        // link them all to each other
        foreach(MapSlot slot in slots.Values)
        {
            slot.SetNeighbors(GetNeighbors(slot));
        }
    }

    public MapSlot GetLowestEntropy()
    {
        // when done debugging, remove the y thing and change back to slots.Values for simplicity
        if (slots.Count == 0) return null;
        List<MapSlot> fuzzy = slots.Values.Where(s => s.possibleModules.Count > 1).ToList();
        if (fuzzy.Count == 0) return null;
        int lowest = fuzzy.Min(s => s.possibleModules.Count);
        List<MapSlot> lowestList = fuzzy.Where(s => s.possibleModules.Count == lowest).ToList();
        return lowestList[Random.Range(0, lowestList.Count)];
    }

    MapSlot GetSlot(int x, int y, int z)
    {
        MapSlot slot = null;
        slots.TryGetValue(new Vector3Int(x, y, z), out slot);
        return slot;
    }

    // u, r, d, l
    public (MapSlot, MapSlot,  MapSlot, MapSlot, MapSlot, MapSlot) GetNeighbors(MapSlot slot)
    {
        Vector3Int p = slot.position;
        int x = p.x;
        int y = p.y;
        int z = p.z;
        MapSlot up = GetSlot(x, y, z+1);
        MapSlot right = GetSlot(x + 1, y, z);
        MapSlot down = GetSlot(x, y, z - 1);
        MapSlot left = GetSlot(x - 1, y, z);
        MapSlot above = GetSlot(x, y + 1, z);
        MapSlot below = GetSlot(x, y - 1, z);
        return (up, right, down, left, above, below);
    }
}
