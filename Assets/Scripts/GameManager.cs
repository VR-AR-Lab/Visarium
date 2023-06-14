using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject VROrigin;
    public GameObject AROrigin;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
#if UNITY_ANDROID
        VROrigin.SetActive(false);
        AROrigin.SetActive(true);
#endif

#if UNITY_STANDALONE_WIN
        VROrigin.SetActive(true);
        AROrigin.SetActive(false);
#endif
    }
    void Start()
    {
        //PhotonStartUp();
        PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    //private void PhotonStartUp()
    //{
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinLobby();
    //    base.OnConnectedToMaster();
    //}

    //public override void OnJoinedLobby()
    //{
    //    RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
    //    PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
    //    base.OnJoinedLobby();
    //}
    //public override void OnJoinedRoom()
    //{
    //    PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0f, 0f, 0f), Quaternion.identity);
    //    base.OnJoinedRoom();
    //}
}
