using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewLoadingComponent : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private float totalProgress = 1;
    [SerializeField] private Slider barLoading;
    [SerializeField] private TMP_Text txtLoading;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public async void Load()
    {
        await LoadingAsync(UpdateLoadProgress);
    }


    private async UniTask LoadingAsync(Action callback)
    {
        float process = 0;
        barLoading.value = 0;

        while (process <= totalProgress)
        {
            await UniTask.Delay(10);

            process += 0.01f;
            barLoading.value = process / totalProgress;
            txtLoading.text = string.Format("Loading.. {0:P1}", process / totalProgress);
        }

        callback?.Invoke();
    }


    private void UpdateLoadProgress()
    {
        // if (GameManager.Instance.Data.HasInitUserData)
        {
            UIMenuScene.Instance.SetView(EViewGame.Lobby);
        }
    }


}
