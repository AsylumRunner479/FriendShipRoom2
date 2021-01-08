using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject Building, home, knife, map, medic;
    public int x = 5;
    public int y = 5;
    public int size;
   private void Start()
    {
        //Grid grid = new Grid(x, y, 3, Building);
        FindObjectOfType<GridManager>().Setup(x, y, size, Building, home, knife, map, medic);
    }
}
