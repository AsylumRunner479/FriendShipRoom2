
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Connect();
        }
        
    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();  
    }
    public void Play(int team)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
        else
        {
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
            {
                { "Team", team }
        };
            PhotonNetwork.SetPlayerCustomProperties(playerProps);
        }
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join room and failed");
        //most likely there is no room
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10});
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }

}
