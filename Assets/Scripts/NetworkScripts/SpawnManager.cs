using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    GameObject[] SpawnPoint;
    private void Awake()
    {
        instance = this;
        SpawnPoint = GameObject.FindGameObjectsWithTag("Spawn");

    }

    public Transform GetRandomSpawn()
    {
        return SpawnPoint[Random.Range(0, SpawnPoint.Length)].transform;
    }
    
}
