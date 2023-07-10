using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRaycast : MonoBehaviour
{

    public Transform Pointer;
    bool Pressed = false;
    void Update()
    {
        Camera cameraAr = Camera.main;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            Pointer.position = hit.point;
        }
        if (Pressed)
        {
            hit.transform.position = cameraAr.transform.position + cameraAr.transform.forward + cameraAr.transform.forward + cameraAr.transform.forward;
            hit.rigidbody.isKinematic = true;
            Debug.Log("Take target");

        }
        else if (Physics.Raycast(ray, out hit) && (Pressed))
        {
            hit.rigidbody.isKinematic = true;
        }else if (!Pressed)
        {
            hit.rigidbody.isKinematic = false;
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
