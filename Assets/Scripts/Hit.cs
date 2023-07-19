using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Transform Pointer;
    public Transform Goal;
    public Transform SphereHockey;
    [SerializeField] private TextMeshProUGUI _textOut;
    bool Hits = false;

    void Update()
    {
        if (Hits)
        {
            SphereHockey.GetComponent<Rigidbody>().velocity =  (Pointer.transform.position =
                new Vector3(Pointer.transform.position.x, 0, Pointer.transform.position.z));
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        int popitka = 0;
        int count = 0;
        float distantions = Vector3.Distance(Goal.transform.position, SphereHockey.transform.position);
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You Win Round");
            Goal.GetComponent<Renderer>().material.color = Color.green;
            popitka += 1;
            count = 60;
            Hits = false;
            SphereHockey.transform.position= SphereHockey.transform.position =
                new Vector3(78.88f, 22.01f, 42.18f); ;
        }
        else if (distantions >= 5.0f)
        {
            Debug.Log("Distance exceeded");
            Goal.GetComponent<Renderer>().material.color = Color.red;
            popitka += 1;
            count += 10;
            Hits = false;
        }
        else if (!collision.gameObject.CompareTag("Goal")
            && !collision.gameObject.CompareTag("Earth") 
            && !collision.gameObject.CompareTag("Player"))
        {
            popitka += 1;
            count += 10;
            Hits = false;
        }
        else if (popitka == 4)
        {
            Debug.Log("You Lose!");
            Goal.GetComponent<Renderer>().material.color = Color.red;
            Hits = false;
            _textOut.text = "";
        }
        _textOut.text = "pop " + popitka.ToString() + "\n"
    + "count " + count.ToString();
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