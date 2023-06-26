using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public GameObject Stage_1;
    public GameObject Stage_2;

    void NextStep()
    {
        Stage_1.SetActive(false);
        Stage_2.SetActive(true);   
    }
}
