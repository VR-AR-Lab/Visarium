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
    bool Hits;
    int attempt;
    int count;

    void FixedUpdate()
    {
        if (Hits)
        {
            SphereHockey.GetComponent<Rigidbody>().velocity =
            new Vector3(Pointer.transform.position.x, 0, Pointer.transform.position.z);
            //SphereHockey.transform.rotation =Quaternion.Euler(0, 0,0);
        }
        FixedUpdate();
    }
    void OnCollisionEnter(Collision collision)
    {
        float distantions = Vector3.Distance(Goal.transform.position, SphereHockey.transform.position);
        if (collision.gameObject.CompareTag(Enums.Tag.TagGoal.ToString()))
        {
            Debug.Log(Enums.Result.Win.ToString());//w
            Goal.GetComponent<Renderer>().material.color = Color.green;
            attempt += 1;
            count = 60;
            Hits = false;
            _textOut.text = "";
            SphereHockey.transform.position= transform.position =
                new Vector3(0, 0, 0); ;
        }
        else if (distantions >= 5.0f)
        {
            Debug.Log(Enums.Result.Distance.ToString());//d
            Goal.GetComponent<Renderer>().material.color = Color.red;
            attempt += 1;
            count += 10;
            Hits = false;
        }
        else if (!collision.gameObject.CompareTag(Enums.Tag.TagGoal.ToString())
            && !collision.gameObject.CompareTag(Enums.Tag.TagEarth.ToString()) 
            && !collision.gameObject.CompareTag(Enums.Tag.TagPlayer.ToString()))
        {
            attempt += 1;
            count += 10;
            Hits = false;
        }
        else if (attempt == 4)
        {
            Debug.Log(Enums.Result.Lose.ToString());
            Goal.GetComponent<Renderer>().material.color = Color.red;
            Hits = false;
            attempt = 0;
            count = 0;
            _textOut.text = "";
            SphereHockey.transform.position = transform.position =
            new Vector3(0, 0, 0);
        }
        _textOut.text = "pop " + attempt.ToString() + "\n"
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