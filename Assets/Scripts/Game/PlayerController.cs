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

public class PlayerController : MonoBehaviourPunCallbacks
{
    [Range(0, 1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public GameObject[] active; 
    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;
    private PhotonView view;
    public TMP_Text nick;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        nick.text = view.Owner.NickName; 
        head.vrTarget = GameObject.FindGameObjectWithTag("HeadVRTarget").transform;
        leftHand.vrTarget = GameObject.FindGameObjectWithTag("LeftHandVRTarget").transform;
        rightHand.vrTarget = GameObject.FindGameObjectWithTag("RightHandVRTarget").transform;
    }

    private void LateUpdate()
    {
        if (view.IsMine)
        {
            for (int i=0;i<active.Length;i++)
                active[i].SetActive(false);
            MapPositionVR();
        }
    }

    private void MapPositionVR()
    {
        transform.position = head.ikTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}