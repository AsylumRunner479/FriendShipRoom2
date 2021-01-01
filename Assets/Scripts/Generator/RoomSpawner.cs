using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public AvailableRooms roomavailable;
    public GameObject[] neighbour;
    public Collider[] zone;
    private bool HasRoom;
    private int random;
    private float timer = 1;
    
    private void Awake()
    {
        roomavailable = FindObjectOfType<AvailableRooms>();
        AvailableRooms.roomsHere += 1;

    }
    private void Start()
    {
        roomavailable = FindObjectOfType<AvailableRooms>();
        for (int i = 0; i < neighbour.Length; i++)
        {
            zone = Physics.OverlapSphere(neighbour[i].transform.position, 2f);
            HasRoom = false;
            for (int j = 0; j < zone.Length; j++)
            {
                if (zone[j].tag == "Spawn")
                {
                    HasRoom = true;

                }
            }
            if (HasRoom != true && AvailableRooms.roomsHere <= 200)
            {
                random = Random.Range(0, roomavailable.rooms.Length);
                Instantiate(roomavailable.rooms[random], neighbour[i].transform.position, Quaternion.identity);
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
