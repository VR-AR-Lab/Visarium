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
    int popitka = 0;
    int count = 0;
    bool Hits = false;

    void Update()
    {
        if (Hits)
        {
            SphereHockey.GetComponent<Rigidbody>().velocity =  (Pointer.transform.position =
                new Vector3(Pointer.transform.position.x, 0, Pointer.transform.position.z));
            Vector3 position = SphereHockey.position;
            position.y = -0.443f;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        float distantions = Vector3.Distance(Goal.transform.position, SphereHockey.transform.position);
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("You Win Round");
            Hits = false;
            Goal.GetComponent<Renderer>().material.color = Color.green;
            popitka += 1;
            count = 60;
        }
        else if (distantions > 7.0f)
        {
            Debug.Log("Distance exceeded");
            Hits = false;
            popitka += 1;
            count += count / 2;
        }
        else if (popitka == 4 && collision.gameObject.tag != "Goal")
        {
            Hits = false;
            Debug.Log("You Lose!");
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