using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupFPS : MonoBehaviour
{


    [Header("FPS")]
    [SerializeField] private TMP_Text tmpTextFps;


    // [private]
    private FpsCounter fpsCounter;
    private string currentVersionCode = "";



    #region UNITY
    private void Start()
    {
        fpsCounter = FpsCounter.Instance;
        GetVersionCode();
    }

    private void FixedUpdate()
    {
        ShowFps();
    }
    #endregion



    private void GetVersionCode()
    {
#if UNITY_EDITOR
        var version = UnityEditor.PlayerSettings.bundleVersion.Replace(".", "");
        var versionCode = UnityEditor.PlayerSettings.Android.bundleVersionCode.ToString();
        currentVersionCode = $"{version}_{versionCode}";
#elif !UNITY_EDITOR && UNITY_ANDROID
        currentVersionCode = $"{Application.version.Replace(".", "")}_{Utils.GetVersionCode()}";
#endif
    }


    private void ShowFps()
    {
        var strValue = "";
        strValue = $"FPS: {fpsCounter.FPS.ToString("f0")}\n";
        strValue += $"Version: {currentVersionCode} ";
        tmpTextFps.text = strValue;
    }


}
