using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int height;
    public int width;
    private float cellSize;
    private int[,] gridArray;
    Node[,] node;
    Transform[,] buildings;
    public Transform buildingHolder;
    public float moveSpeed = 0;
    private int homeX, homeY;
    public bool homeSpawn = false;
    private int random0;
    //Vector2Int currentNodeIndex
    //Vector2Int nextNodeIndex
    //Vector3.lerp(node[currentNodeIndex.x][currentNodeIndex.y], node[nextNodeIndex.x][nextNodeIndex.y]
    public void RandomX(int random)
    {
        random0 = Random.Range(0, random);
    }
   

    public void Setup(int width, int height, float cellSize, GameObject Building, GameObject home, GameObject knife, GameObject map, GameObject medic)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new int[width, height];
        buildings = new Transform[width, height];
        homeX = Random.Range(0, 2);
        homeY = Random.Range(0, 2);
        while (homeSpawn == false)
        {
            foreach (Transform child in buildingHolder)
            {
                Destroy(child.gameObject);
            }
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Vector3 worldPoint = new Vector3(x * cellSize, 0, y * cellSize);

                 
                            buildings[x, y] = Instantiate(knife, worldPoint, Quaternion.identity, buildingHolder).transform;
                       

                }
            }
        }
        
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
    public void SetValue(int x, int y, int value)
    {
        //if (x >= 0 && y >= 0 && x <)
        {

        }
    }
}
