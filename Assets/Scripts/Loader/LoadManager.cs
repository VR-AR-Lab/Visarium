using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Delay(CheckPlatform()));
    }
    private bool CheckPlatform()
    {
        //true-vr; false-mobile.
#if UNITY_STANDALONE_WIN
        return true;
#endif
#if UNITY_ANDROID
        return false;
#endif
#if UNITY_IOS
        return false;
#endif
    }
    private IEnumerator Delay(bool isPC)
    {
        yield return new WaitForSeconds(3f);
        if (isPC)
            SceneManager.LoadScene("LobbyVR");
        else
            SceneManager.LoadScene("LobbyAR");
    }
}