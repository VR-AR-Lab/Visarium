using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.IO;

public class Test : MonoBehaviourPunCallbacks
{
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
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
        base.OnJoinedLobby();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = "test";
        PhotonNetwork.LoadLevel("Game");
        base.OnJoinedRoom();
    }
}
