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
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Groups W-C")]
    public GameObject[] Group;

    [Header("Sounds")]
    public AudioSource player;
    public AudioClip fonsound;
    public AudioClip click;

    [Header("Network room")]
    public ScrollRect scrollRect;
    public GameObject scrollContent;
    public GameObject connectPrefab;

    [Header("Room options")]
    public TMP_InputField roomname;
    public Toggle Visible;
    public Toggle Public;
    public TMP_InputField maxplayers;
    public TMP_Text errortxt;

    [Header("Login component")]
    public GameObject login;
    public GameObject registr;
    public GameObject confirm;
    public GameObject reset;
    LoadScript loadScript;

    public string nick;
    public string url;
    static AssetBundle assetBundle;
    private string SceneNameToLoad;

    public void GoGroup(int ID)
    {
        player.PlayOneShot(click);
        for (int i = 0; i < Group.Length; i++)
        {
            if (i == ID)
            {
                Group[i].SetActive(true);
                continue;
            }
            Group[i].SetActive(false);
        }
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        StartCoroutine(DownloadFiles());
        scrollRect.verticalNormalizedPosition = 1;
        errortxt.text = "";  
    }
    [System.Obsolete]
    IEnumerator DownloadFiles()
    {
        if (!assetBundle)
        {
            using WWW www = new(url);
            Debug.Log("in using www");
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }
            assetBundle = www.assetBundle;
        }

        string[] scenes = assetBundle.GetAllScenePaths();
        Debug.Log("scenes.Length:: " + scenes.Length);
        foreach (string scenename in scenes)
        {
            SceneNameToLoad = Path.GetFileNameWithoutExtension(scenename).ToString();
            Debug.Log("SceneNamesInPath(foreach):: " + Path.GetFileNameWithoutExtension(scenename));
        }
    }
    public void LoadAssetBundleScene()
    {
        SceneManager.LoadScene(SceneNameToLoad);
        Debug.Log("Clicked on button to play the scene");
    }

    public void SetGroup(string roomname, int countPlayers, int maxPlayers)
    {
        GameObject scrolltemobj = Instantiate(connectPrefab);
        scrolltemobj.name =  roomname;
        scrolltemobj.transform.Find("room name").GetComponent<TMP_Text>().text = roomname;
        scrolltemobj.transform.Find("players").GetComponent<TMP_Text>().text = countPlayers + "/" + maxPlayers + " players";
        scrolltemobj.transform.Find("connect").GetComponent<Button>().onClick.AddListener(delegate () { ConnectToGameRoom(roomname); });
        scrolltemobj.transform.SetParent(scrollContent.transform, false);
    }

    public void ChangeLoginGroup()
    {
        player.PlayOneShot(click);
        if (login.activeSelf)
        {
            login.SetActive(false);
            registr.SetActive(true);
        }
        else if(registr.activeSelf)
        {
            login.SetActive(true);
            registr.SetActive(false);
        }
        else if(confirm.activeSelf)
        {
            login.SetActive(true);
            confirm.SetActive(false);
            registr.SetActive(false);
        }
        else if (reset.activeSelf)
        {
            reset.SetActive(false);
            login.SetActive(true);
            confirm.SetActive(false);
            registr.SetActive(false);
        }
    }
    public void ConfirmEmailGroup()
    {
        registr.SetActive(false);
        confirm.SetActive(true);
        reset.SetActive(false);
    }

    public void PlaySoundCLick()
    {
        player.PlayOneShot(click);
    }
    public void Logout()
    {
        GetComponent<AuthManager>().FirebaseLogOut();
        GoGroup(2);
    }
    public void ActiveResetGroup()
    {
        player.PlayOneShot(click);
        login.SetActive(false);
        reset.SetActive(true);
    }

    // Photon Start

    public void PhotonStartUp()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
    }

    public void CreateRoom()
    {
        player.PlayOneShot(click);
        int max;
        bool isInt = Int32.TryParse(maxplayers.text, out max);
        if(max<=0)
        {
            errortxt.text = "ERROR: the number of players is incorrect";
        }
        else if (roomname.text.Length==0)
        {
            errortxt.text = "ERROR: come up with the name of the room";
        }
        else
        {
            PhotonNetwork.NickName = nick;//GetComponent<AuthManager>().User.DisplayName;
            RoomOptions roomOptions = new RoomOptions() { IsVisible = Visible, IsOpen = Public, MaxPlayers = (byte)max };
            PhotonNetwork.JoinOrCreateRoom(roomname.text, roomOptions, TypedLobby.Default);
            
            
        }
    }

    private void ConnectToGameRoom(string roomname)
    {
        PhotonNetwork.NickName = nick;//GetComponent<AuthManager>().User.DisplayName;
        PhotonNetwork.JoinRoom(roomname);
    }

    public override void OnJoinedRoom()
    {
        // PhotonNetwork.LoadLevel("Game");
        //PhotonNetwork.LoadLevel("Game");
        LoadAssetBundleScene();
        //base.OnJoinedRoom();
    }

    private void OnDestroyRoomPrefab(string roomname)
    {
        Destroy(GameObject.Find(roomname));
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                print("removed "+info.Name);
                OnDestroyRoomPrefab(info.Name);
            }
            else
            {
                SetGroup(info.Name, info.PlayerCount, info.MaxPlayers);
            }
        }
        base.OnRoomListUpdate(roomList);
    }
}