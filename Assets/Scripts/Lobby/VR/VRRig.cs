using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap lefthand;
    public VRMap righthand;
    public Transform headConst;
    private Vector3 headBodyOffest;
    public float speed;

    private void Start()
    {
        headBodyOffest = transform.position - headConst.position;   
    }

    private void LateUpdate()
    {
        transform.position = headConst.position + headBodyOffest;
        transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConst.up, Vector3.up).normalized, Time.deltaTime*speed);
        head.Map();
        righthand.Map();
        lefthand.Map();
    }
}
