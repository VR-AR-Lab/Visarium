using UnityEngine;
using Photon.Pun;
public class Cleared : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Clear")
            PhotonNetwork.Destroy(gameObject);
    }
}
