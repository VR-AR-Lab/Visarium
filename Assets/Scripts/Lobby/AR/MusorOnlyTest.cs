using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MusorOnlyTest : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void PhotonStartUp()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "empty";
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
    }
    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
        base.OnJoinedLobby();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        base.OnJoinedRoom();
    }
}
