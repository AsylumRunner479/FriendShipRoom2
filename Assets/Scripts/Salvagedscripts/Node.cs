using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool walkable;
    public Vector3 worldPosition;
    public Node(bool Walkable, Vector3 WorldPosition)
    {
        walkable = Walkable;
        worldPosition = WorldPosition;
    }
    public void Start()
    {
        worldPosition = this.transform.position;
    }
}
