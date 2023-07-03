using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Desk : MonoBehaviour
{
    public float Zposy;
    private GameObject line;
    private Vector3 oldposy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pencil")
        {
            line = PhotonNetwork.Instantiate("LineComponent", this.transform.position, Quaternion.identity);
            line.transform.parent = this.transform;
            oldposy = other.transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (line != null)
        {
            if (other.tag == "Pencil" && Vector3.Distance(oldposy, other.transform.position) > 0.1f)
            {
                line.transform.GetComponent<LineRenderer>().positionCount++;
                line.transform.GetComponent<LineRenderer>().SetPosition(line.transform.GetComponent<LineRenderer>().positionCount - 1, new Vector3(other.transform.position.x, other.transform.position.y, Zposy));
                oldposy = other.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        line = null;
        oldposy = new Vector3(0,0,0);
    }
}