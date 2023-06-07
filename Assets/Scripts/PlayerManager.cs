using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public Transform head;
    public Transform lefthand;
    public Transform righthand;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            head.gameObject.SetActive(false);
            lefthand.gameObject.SetActive(false);
            righthand.gameObject.SetActive(false);

            MapPosition(head, XRNode.Head);
            MapPosition(lefthand, XRNode.LeftHand);
            MapPosition(righthand, XRNode.RightHand);
        }

    }

    private void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotation);
        target.position = position;
        target.rotation = rotation;
    }
}
