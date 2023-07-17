using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Transform Pointer;
    public Transform Goal;
    public Transform SphereHockey;
    bool Hits = false;
    void Start()
    {

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
        }
        if (Hits)
        {
            SphereHockey.GetComponent<Rigidbody>().velocity = cameraAr.transform.position + cameraAr.transform.forward * 3;
            float distantion = Vector3.Distance(SphereHockey.position, Goal.transform.position);
            if (distantion >= 2)
            {
                Debug.LogError("Error");
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Goal")
            {
            Debug.Log("You Win Round");
            Hits = false;
            }
    }
    public void hitHockey()
    {
        Hits = true;
    }
    public void AnHitHockey()
    {
        Hits = false;
    }
}