using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject character;
    public Image hair1;
    public Image hair2;
    int iHair = 1;
    bool swap = true;
    public List<Sprite> Hairs = new List<Sprite>();
    public List<GameObject> HairsModels = new List<GameObject>();
    int countHair;
    GameObject subjectsPrefab;
    void Start()
    {
        countHair = Hairs.Count;
    }

    // Update is called once per frame
    public void MenuCheck()
    {
        if (menu.activeSelf)
        {
            Object.Destroy(subjectsPrefab);
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
        hair1.sprite = Hairs[iHair-1];
        hair2.sprite = Hairs[iHair];
    }
    public void ChooseHairUp()
    {
        if (!swap)
        {
            iHair++;
        }
        swap = true;
        iHair++;
        if (iHair > countHair - 1)
        {
            iHair = 0;
        }
        hair1.sprite = hair2.sprite;
        hair2.sprite = Hairs[iHair];
    }
    public void ChooseHairDown()
    {
        if (swap)
        {
            iHair--;
        }
        swap = false;
        iHair--;
        if (iHair < 0)
        {
            iHair = countHair - 1;
        }
        hair2.sprite = hair1.sprite;
        hair1.sprite = Hairs[iHair];
    }
    public void SpawnHair1()
    {
        Object.Destroy(subjectsPrefab);
        if (swap)
        {
            subjectsPrefab = Instantiate(HairsModels[iHair-1], character.transform);
            subjectsPrefab.transform.position = character.transform.position + new Vector3(0, 0.5f, 0.2f);
        }
        else
        {
            subjectsPrefab = Instantiate(HairsModels[iHair], character.transform);
            subjectsPrefab.transform.position = character.transform.position + new Vector3(0, 0.5f, 0.2f);
        }
    }
    public void SpawnHair2()
    {
        Object.Destroy(subjectsPrefab);
        if (swap)
        {
            subjectsPrefab = Instantiate(HairsModels[iHair], character.transform);
            subjectsPrefab.transform.position = character.transform.position + new Vector3(0, 0.5f, 0.2f);
        }
        else
        {
            subjectsPrefab = Instantiate(HairsModels[iHair+1], character.transform);
            subjectsPrefab.transform.position = character.transform.position + new Vector3(0, 0.5f, 0.2f);
        }
    }

}
