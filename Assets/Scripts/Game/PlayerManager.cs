using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public Transform head;
    public Transform lefthand;
    public Transform righthand;
    public GameObject[] avatar;
    public TMP_Text nick;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        nick.text = view.Owner.NickName;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            for(int i=0;i<avatar.Length;i++)
                avatar[i].SetActive(false);
            head.gameObject.SetActive(false);
            lefthand.gameObject.SetActive(false);
            righthand.gameObject.SetActive(false);

#if UNITY_ANDROID
            head.transform.position = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.position;
            head.transform.rotation = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.rotation;
            lefthand.transform.position = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.position;
            lefthand.transform.rotation = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.rotation;
            righthand.transform.position = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.position;
            righthand.transform.rotation = GameObject.Find("XR_AR").transform.GetChild(0).transform.GetChild(0).transform.rotation;
            //�������� �������� �� ����������� ������� � ������ � ��
#endif

#if UNITY_STANDALONE_WIN
            MapPositionVR(head, XRNode.Head);
            MapPositionVR(lefthand, XRNode.LeftHand);
            MapPositionVR(righthand, XRNode.RightHand);
#endif
        }
    }

    private void MapPositionVR(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotation);
        target.position = position;
        target.rotation = rotation;
    }
}
