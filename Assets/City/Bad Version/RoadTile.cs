using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    public Edges edges;
    public static float width = 3f;
    public static float labelBias = 1.1f;

    [HideInInspector]
    public Rotation rotation = Rotation.rot0;

    private void OnDrawGizmos()
    {
        Vector3 posX = new Vector3(0, width/8, -width/2);
        Vector3 negX = new Vector3(-width, width/8, -width/2);
        Vector3 posZ = new Vector3(-width/2, width/8, 0);
        Vector3 negZ = new Vector3(-width / 2, width / 8, -width);
        Handles.Label(transform.position + posX * labelBias, edges.posX == Edge.Default ? "Pos X" : edges.posX.ToString());
        Handles.Label(transform.position + negX * labelBias, edges.negX == Edge.Default ? "Neg X" : edges.negX.ToString());
        Handles.Label(transform.position + posZ * labelBias, edges.posZ == Edge.Default ? "Pos Z" : edges.posZ.ToString());
        Handles.Label(transform.position + negZ * labelBias, edges.negZ == Edge.Default ? "Neg Z" : edges.negZ.ToString());

    }

    public bool IsFullySymmetrical()
    {
        return edges.posX == edges.posZ && edges.posX == edges.negX && edges.posX == edges.negZ;
    }

    public bool IsHalfSymmetrical()
    {
        return edges.posX == edges.negX && edges.posZ == edges.negZ;
    }
}

[System.Serializable]
public struct Edges
{
    public Edge posX;
    public Edge negX;
    public Edge posZ;
    public Edge negZ;
}

[System.Serializable]
public enum Edge
{
    Default,
    Grass,
    DirtRoad,
    Water,
}

[System.Serializable]
public enum Direction
{
    posZ,
    posX,
    negZ,
    negX,
}

public enum Rotation {
    rot0,
    rot90,
    rot180,
    rot270,
}