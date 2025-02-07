using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoBuild : MonoBehaviour
{

    public static void ExecuteFromExternal()
    {
// #if UNITY_EDITOR
//         WebBuildPipeline.BuildWebGLVersion();
// #endif
    }
    
    
    public static void CopyAndPushToGit()
    {
// #if UNITY_EDITOR
//         WebBuildPipeline.CopyAndPushToGit();
// #endif
    }

}
