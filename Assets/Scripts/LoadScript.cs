using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class LoadScript : MonoBehaviour
{
    public string url;
    static AssetBundle assetBundle;
    private string SceneNameToLoad;

    [System.Obsolete]
    void Start()
    {
        StartCoroutine(DownloadFiles());
    }

    [System.Obsolete]
    IEnumerator DownloadFiles()
    {
        if (!assetBundle)
        {
            using WWW www = new(url);
            Debug.Log("in using www");
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }
            assetBundle = www.assetBundle;
        }

        string[] scenes = assetBundle.GetAllScenePaths();
        Debug.Log("scenes.Length:: " + scenes.Length);
        foreach (string scenename in scenes)
        {
            SceneNameToLoad = Path.GetFileNameWithoutExtension(scenename).ToString();
            Debug.Log("SceneNamesInPath(foreach):: " + Path.GetFileNameWithoutExtension(scenename));
        }
    }
    public void LoadAssetBundleScene()
    {
        SceneManager.LoadScene(SceneNameToLoad);
        Debug.Log("Clicked on button to play the scene");
    }
}