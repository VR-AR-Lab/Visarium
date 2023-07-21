using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Transform Pointer;
    public Transform Person;
    public Transform Clushka;
    public Transform Goal;
    public Transform Camera;
    public Transform SphereHockey;
    [SerializeField] private TextMeshProUGUI _textOut;
    Vector3 LookDir;
    public float MoveSpeed = 3.0f;
    bool Hits = false;
    bool Rasst = false;
    int attempt;
    int count;

     void FixedUpdate()
    {
        float distantions = Vector3.Distance(Clushka.transform.position, SphereHockey.transform.position);
        if (Rasst&&Hits)
        {
            LookDir = Camera.forward;
            LookDir.y = 0;
            SphereHockey.GetComponent<Rigidbody>().velocity = LookDir;
            SphereHockey.transform.Translate(LookDir * MoveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        float dist = Vector3.Distance(Clushka.transform.position, SphereHockey.transform.position);
        float distantion = Vector3.Distance(Goal.transform.position, SphereHockey.transform.position);
        if (!collision.gameObject.CompareTag(Enums.Tag.TagClushka.ToString()) && !collision.gameObject.CompareTag(Enums.Tag.TagEarth)
            && !collision.gameObject.CompareTag(Enums.Tag.TagPlayer.ToString()))
        {
            Hits = false;
            count += 10;
            attempt += 1;
        }
        if (dist <= 1.0f)
        {
            Rasst = true;
        }
        else if (collision.gameObject.CompareTag(Enums.Tag.TagGoal.ToString()))
        {
            Hits = false;
            Debug.Log(Enums.Result.Win.ToString());//d
            Goal.GetComponent<Renderer>().material.color = Color.green;
            attempt = 60;
            SphereHockey.transform.position = transform.position = new Vector3(0, 0, 0);
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
        else if (distantion >= 5.0f)
        {
            Hits = false;
            Debug.Log(Enums.Result.Distance.ToString());//d
            Goal.GetComponent<Renderer>().material.color = Color.red;
            attempt += 1;
            count += 10;
            Hits = false;
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