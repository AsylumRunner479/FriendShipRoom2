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
        FindSpawn();

    }

    public Transform GetRandomSpawn()
    {
        return SpawnPoint[Random.Range(0, SpawnPoint.Length)].transform;
    }
    public void FindSpawn()
    {
        SpawnPoint = GameObject.FindGameObjectsWithTag("Spawn");
    }
}
