using UnityEditor;
using System;
using UnityEngine;
public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
public static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundleFolder", BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows64);
        //Debug.Log("sssss");
    }
}
