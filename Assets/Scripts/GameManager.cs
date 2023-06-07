using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonStartUp();
    }
    private void PhotonStartUp()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = $"Player {Random.Range(1, 100)}";
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        print("YESSSSS");
        PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0f, 0f, 0f), Quaternion.identity);
        base.OnJoinedRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
