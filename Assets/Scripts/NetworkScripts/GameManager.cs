using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject RedPrefab, BluePrefab, YellowPrefab, GreenPrefab, PurplePrefab, OrangePrefab, BlackPrefab, WhitePrefab;
    public GameObject pauseCanvas;
    public bool paused = false;
    public static int PlayerCount;
    public static bool InGame;
    public static GameManager instance;
    public Health[] AvailablePlayers;
    public static int HumanCount,AlienCount;
    public Text count, WinState;
    // Start is called before the first frame update
    void Start()
    {
        InGame = false;
        SetPaused();
        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        Debug.Log($"Team number {team} is being instantiated");
        Transform spawn = SpawnManager.instance.GetRandomSpawn();
        if (team == 0)
        {
            
            PhotonNetwork.Instantiate(RedPrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 1)
        {
            PhotonNetwork.Instantiate(BluePrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 2)
        {
            PhotonNetwork.Instantiate(YellowPrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 3)
        {
            PhotonNetwork.Instantiate(GreenPrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 4)
        {
            PhotonNetwork.Instantiate(PurplePrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 5)
        {
            PhotonNetwork.Instantiate(OrangePrefab.name, spawn.position, Quaternion.identity);
        }
        else if (team == 6)
        {
            PhotonNetwork.Instantiate(BlackPrefab.name, spawn.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(WhitePrefab.name, spawn.position, Quaternion.identity);
        }
        PlayerCount += 1;
        count.text = PlayerCount + " Players";
    }
    private void Awake()
    {
        instance = this;
    }
    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        PlayerCount -= 1;
    }
    public void StartLevel()
    {
        AvailablePlayers = FindObjectsOfType<Health>();
        //spawn Level
        int team = Random.Range(0, AvailablePlayers.Length);
        for (int i = 0; i < AvailablePlayers.Length; i++)
        {
            if (i == team)
            {
                AvailablePlayers[i].PlayerRole(0);
            }
            else
            {
                AvailablePlayers[i].PlayerRole(1);
            }
            AvailablePlayers[i].Damage(100);
        }
        

        HumanCount = AvailablePlayers.Length - 1;
        AlienCount = 1;
        InGame = true;
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !InGame)
        {
            StartLevel();
        }
        if (Input.GetKeyDown(KeyCode.L) && InGame)
        {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            SetPaused();
        }
        if (AlienCount == 0 && InGame && WinState.text != "Humans have won")
        {
            WinState.text = "Humans have won";
            
        }
        if (HumanCount == 0 && InGame && WinState.text != "Aliens have won")
        {
            WinState.text = "Aliens have won";
        }
    }
    void SetPaused()
    {
        //set the canvas
        pauseCanvas.SetActive(paused);
        //set the cursor lock
        Cursor.lockState = paused ?CursorLockMode.None : CursorLockMode.Locked;
        //set the cursor visible
        Cursor.visible = paused;

    }
}
