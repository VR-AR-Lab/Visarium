using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneRaycast : MonoBehaviour
{

    public Transform Pointer;
    public Transform SphereHockey;
    bool Pressed = false;
    bool Throw = false;
    int throwPower=3;
    public float distantion;
    public float speed;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
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
            if (Throw) {
                hit.rigidbody.isKinematic = false;
                hit.rigidbody.velocity = transform.forward * throwPower;
                Pressed = false;
                Throw = false;

            }
        }
    }
    public void onDown()
    {
        Pressed = false;
    }
    public void onUp()
    {
        Pressed = true;
    }
    public void Pbrosok()
    {
        Throw = true;
    }
    public void Nbrosok()
    {
        Throw = false;
    }
}