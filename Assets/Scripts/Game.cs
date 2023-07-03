using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Game : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;
    public float posx;
    public float posy;
    public float posz;

    [Header("Skittles")]
    public List<GameObject> spawn = new List<GameObject>();
    public GameObject pin;
    public GameObject skittles;

    [Header("Count Players")]
    public TMP_Text NumPlayer;
    public GameObject playerchoose;

    [Header("Menu Players")]
    public List<GameObject> Players = new List<GameObject>();
    public List<TMP_Text> TextMeshProList1;
    public List<TMP_Text> TextMeshProList2;
    public List<TMP_Text> TextMeshProList3;
    public List<TMP_Text> TextMeshProList4;
    public GameObject Reset;

    private List<List<TMP_Text>> TextMeshProLists;
    private List<GameObject> Skittles = new List<GameObject>();
    private GameObject sharx;
    private Vector3 pos;
    private int score;
    private int number;
    private int numplayer;
    private int numgame;
    private int Playergame;

    public void Start()
    {
        pos = new Vector3(posx, posy, posz);
        sharx = Instantiate(ball, pos, Quaternion.identity);
        number = 0;
        score = 0;
        TextMeshProLists.Add(TextMeshProList1);
        TextMeshProLists.Add(TextMeshProList2);
        TextMeshProLists.Add(TextMeshProList3);
        TextMeshProLists.Add(TextMeshProList4);
        for (int i = 0; i < 10; i++)
        {
            Skittles.Add(sharx);
        }
        ChoosePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball")
        {
            StartCoroutine(GamePause());
        }
    }

    IEnumerator GamePause()
    {
        yield return new WaitForSeconds(1f);
        Destroy(sharx);
        sharx = Instantiate(ball, pos, Quaternion.identity);
        for (int i = 0; i < Skittles.Count; i++)
        {
            if (Skittles[i] != null)
            {
                if (Skittles[i].transform.localEulerAngles != spawn[i].transform.localEulerAngles)
                {
                    Destroy(Skittles[i]);
                    score++;
                }
            }
        }
        Score(score);
        if (TextMeshProLists[numplayer - 1][19] == null)
        {
            if (number == 1)
            {
                for (int i = 0; i < Skittles.Count; i++)
                {
                    if (Skittles[i] != null)
                    {
                        Destroy(Skittles[i]);
                    }
                }
                number = 0;
                Spawn();
            }
            else
            {
                number++;
            }
            if (score == 10)
            {
                number = 0;
                Spawn();
            }
            score = 0;
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < spawn.Count; i++)
        {
            Vector3 pos = new Vector3(spawn[i].transform.position.x, spawn[i].transform.position.y, spawn[i].transform.position.z);
            GameObject go = Instantiate(pin, pos, Quaternion.identity);
            go.transform.SetParent(skittles.transform);
            Skittles[i]=go;
        }
    }
    public void ChoosePlayer()
    {
        playerchoose.SetActive(true);
    }
    public void Addplayer()
    {
        if (numplayer<4)
        {
            numplayer++;
        }
        NumPlayer.text = numplayer.ToString();
    }
    public void Deleteplayer()
    {
        if (numplayer > 1)
        {
            numplayer--;
        }
        NumPlayer.text = numplayer.ToString();
    }
    public void ConfirmPlayer()
    {
        playerchoose.SetActive(false);
        numgame = 0;
        Playergame = 0;
        ChangePlayers();
    }
    public void ChangePlayers()
    {
        int i = 0;
        while (i < numplayer)
        {
            Players[i].SetActive(true);
            i++;
        }
        Spawn();
    }
    public void Score(int score)
    {
        TextMeshProLists[Playergame][numgame].text = score.ToString();
        numgame++;
        if (Playergame < numplayer && numgame % 2 == 1)
        {
            numgame -= 2;
            Playergame++;
        }
        else
        {
            Playergame = 0;
        }
        if (TextMeshProLists[numplayer - 1][19] != null)
        {
            Reset.SetActive(true);
        }
    }
    public void ResetGame()
    {
        ChoosePlayer();
    }
    //private void Update()
    //{
    //    print(Skittles[9].transform.localEulerAngles);
    //}
}

