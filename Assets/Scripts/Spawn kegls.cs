using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnkegls : MonoBehaviour
{
    public List<GameObject> spawn = new List<GameObject>();
    public GameObject keglya;
    public GameObject kegls;

    private List<GameObject> kegli = new List<GameObject>();

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < spawn.Count; i++)
        {
            Vector3 pos = new Vector3(spawn[i].transform.position.x, spawn[i].transform.position.y, spawn[i].transform.position.z);
            GameObject go = Instantiate(keglya, pos, Quaternion.identity);
            go.transform.SetParent(kegls.transform);
        }
    }
    
    public List<GameObject> ReturnListSpawn()
    {
        return kegli;
    }
}
