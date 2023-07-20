using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneRaycast : MonoBehaviour
{

    public Transform Pointer;
    public Transform SphereHockey;
    bool Pressed;
    bool Throw;
    int throwPower=3;
    public float distantion;
    public float speed;
    Rigidbody rb;
    private void Start()
    {
        Pressed = false;
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
                    if (hit.collider.CompareTag(Enums.Tag.TagDraggable.ToString()) && !hit.collider.CompareTag(Enums.Tag.TagEarth.ToString())
                    && !hit.collider.CompareTag(Enums.Tag.TagRoad.ToString()))
                    {
                        hit.transform.position = cameraAr.transform.position + cameraAr.transform.forward * 3;
                        hit.rigidbody.isKinematic = true;

                        Debug.Log(Enums.Result.TTarget.ToString());
                    }
                }
                else if(!Pressed && !hit.collider.CompareTag(Enums.Tag.TagEarth.ToString())
                    && !hit.collider.CompareTag(Enums.Tag.TagRoad.ToString()))
                {
                    hit.rigidbody.isKinematic = false;
                }
            }
            if (Throw)
            {
                Debug.Log(Enums.Result.Thrown.ToString());
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