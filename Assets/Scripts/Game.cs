using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
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

    private List<List<TMP_Text>> TextMeshProLists = new List<List<TMP_Text>>();
    private List<GameObject> Skittles = new List<GameObject>();
    private GameObject sharx;
    private Vector3 pos;
    private int score;
    private int number;
    private int numplayer=1;
    private int numgame;
    private int Playergame;
    private List<int> xScore = new List<int>();

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
        for (int i = 0; i < 10; i++)
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
    public void Spawn()
    {
        for (int i = 0; i < spawn.Count; i++)
        {
            GameObject go = Instantiate(pin, spawn[i].transform.position, Quaternion.identity);
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
        for (int i=0; i<4;i++)
        {
            xScore.Add(1);
        }
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
        for (i = 0; i < numplayer; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                TextMeshProLists[i][j].text = null;
            }
        }
        Spawn();
    }
    public void Score(int score)
    {
        if (Playergame < numplayer-1 && numgame % 2 == 0 && numgame > 0)
        {
            numgame -= 2;
            Playergame++;
        }
        else if (Playergame == numplayer-1 && numgame % 2 == 0 && numgame > 0)
        {
            Playergame = 0;
        }
        if (numgame % 2 == 0 && score == 10)
        {
            TextMeshProLists[Playergame][numgame].text = "X";
            numgame++;
        }
        else if (numgame % 2 == 1 && Convert.ToInt32(TextMeshProLists[Playergame][numgame - 1].text) + score == 10)
        {
            TextMeshProLists[Playergame][numgame].text = "/";
        }
        else TextMeshProLists[Playergame][numgame].text = score.ToString();
        numgame++;
        if (TextMeshProLists[numplayer - 1][19].text != null)
        {
            Reset.SetActive(true);
        }
    }
    public void ResetGame()
    {
        for (int i = 0; i < numplayer; i++)
        {
            for (int j = 0; j < 20;  j++)
            {
                TextMeshProLists[i][j].text = null;
            }
        }
        for (int i = 0; i < Skittles.Count; i++)
        {
            if (Skittles[i] != null)
            {
                Destroy(Skittles[i]);
            }
        }

        ChoosePlayer();
    }
    //private void Update()
    //{
    //    print(Skittles[9].transform.localEulerAngles);
    //}
}

