using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneRaycast : MonoBehaviour
{

    public Transform Pointer;
    bool Pressed = false;
    public float distantion;
    void Update()
    {
        Camera cameraAr = Camera.main;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Pointer.position = hit.point;
            distantion = Vector3.Distance(Pointer.position, transform.position);
            if (distantion < 4.0)
            {
                if (Pressed)
                {
                    if (hit.collider.tag == "Draggable")
                        hit.transform.position = cameraAr.transform.position + cameraAr.transform.forward *3;
                    hit.rigidbody.isKinematic = true;

                    Debug.Log("Take target");
                }
                else if (!Pressed)
                {
                    Pressed = false;
                    hit.rigidbody.isKinematic = false;
                }
            }
        }
    }
    public void onDown()
    {
        Pressed = true;
    }

    public void onUp()
    {
        Pressed = false;
    }
}