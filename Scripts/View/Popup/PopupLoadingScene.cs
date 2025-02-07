using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PopupLoadingScene : PopupBase
{


    [Header("[Scene]")]
    [SerializeField] private string sceneName;
    

    [Header("[UI]")]
    [SerializeField] private Slider sliderBar;
    [SerializeField] private TMP_Text txtProcess;



    public void Show(string name, Action cbFinish = null, Action cbCancel = null)
    {
        sceneName = name;
        Show(cbFinish, cbCancel);
        StartCoroutine(LoadingAsync());
    }


    private IEnumerator LoadingAsync()
    {
        var progress = 0f;
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        //scene will not load while this value is false
        asyncLoad.allowSceneActivation = false;
        while (progress < 1f)
        {

            sliderBar.value = progress;
            txtProcess.text = $"Loading {Mathf.Round(progress * 100f)}%";
            progress += .01f;
            yield return new WaitForSeconds(.01f);
        }

        while (!asyncLoad.isDone && progress >= 1f)
        {
            sliderBar.value = 1;
            txtProcess.text = $"Loading {100}%";
            // print("print progress: " + progress);

            //here the scene is definitely loaded.
            asyncLoad.allowSceneActivation = true;
            yield return null;
        }

        Hide();
        callbackButtonOK?.Invoke();
    }


}
