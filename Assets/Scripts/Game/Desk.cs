using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI.Extensions;

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
            line.transform.GetComponent<LineRenderer>().useWorldSpace = false;
            line.transform.parent = this.transform;
            oldposy = other.transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (line != null)
        {
            var collisionPoint = other.ClosestPoint(transform.position);
            var collisionNormal = transform.position - collisionPoint;
            if (other.tag == "Pencil" && Vector3.Distance(oldposy, collisionNormal) > 0.1f)
            {
                line.transform.GetComponent<LineRenderer>().positionCount++;
                line.transform.GetComponent<LineRenderer>().SetPosition(line.transform.GetComponent<LineRenderer>().positionCount - 1, new Vector3(-collisionNormal.x, -collisionNormal.y, Zposy));
                oldposy = collisionNormal;
                
                Mesh mesh = new Mesh();
                line.transform.GetComponent<LineRenderer>().BakeMesh(mesh, true);
                line.transform.GetComponent<MeshCollider>().sharedMesh = mesh;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        line = null;
        oldposy = new Vector3(0,0,0);
    }
}