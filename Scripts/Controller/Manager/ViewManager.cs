using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : Singleton<ViewManager>
{


    [Header("[Setting]")]
    [SerializeField] private List<PopupBase> popupGames;


    [Header("[Popup]")]
    [SerializeField] private PopupOk popupOk;
    [SerializeField] private PopupOkCancel popupOkCancel;
    [SerializeField] private PopupLoading popupLoading;
    [SerializeField] private PopupLoadingScene popupLoadingScene;
    [SerializeField] private PopupSetting popupSetting;
    [SerializeField] private PopupStageConfirm popupStageConfirm;
    [SerializeField] private PopupStageResult popupStageResult;




    #region UNITY
    private void OnEnable()
    {
        // this.RegisterListener(EventID.UpdateCurrency, EventUpdateCurrency);
    }

    private void OnDisable()
    {
        // this.RemoveListener(EventID.UpdateCurrency, EventUpdateCurrency);
    }

    private void Start()
    {
        Init();
    }
    #endregion




    public void Init()
    {
        popupSetting.Load();
    }

    // public void ShowLoading(Action cbDone = null)
    // {
    //     popupLoading.Show(cbDone);
    // }

    // public void HideLoading(Action cbDone = null)
    // {
    //     popupLoading.Hide();
    // }




    public void ShowLoading(float time, Action cbDone = null)
    {
        popupLoading.Show(time, cbDone);
    }


    public void ShowLoadingScene(string name, Action cbFinish = null)
    {
        popupLoadingScene.Show(name, cbFinish);
    }


    public void ShowPopupOk(string title, string content, Action cbOK = null, Action cbCancel = null)
    {
        popupOk.Show(title, content, cbOK, cbCancel);
    }


    public void ShowPopupOkCancel(string title, string content, Action cbOK = null, Action cbCancel = null)
    {
        popupOkCancel.Show(title, content, cbOK, cbCancel);
    }


    public void ShowPopupSetting()
    {
        popupSetting.Showing();
    }


    public void ShowPopupStageConfirm(string[] rewards, Action cbOK = null, Action cbCancel = null)
    {
        popupStageConfirm.Load(rewards, cbOK, cbCancel);
    }


    public void ShowPopupStageResultLose(Action cbOK = null, Action cbCancel = null)
    {
        popupStageResult.ShowLose(cbOK, cbCancel);
    }


    public void ShowPopupStageResultWin(string[] rewards, Action cbOK = null, Action cbCancel = null)
    {
        popupStageResult.ShowWin(rewards, cbOK, cbCancel);
    }




    public void LoadSceneMenu()
    {
        ShowLoadingScene("Menu", OpenSceneMenu);
    }


    public void LoadSceneBattle()
    {
        ShowLoadingScene("Battle", OpenSceneBattle);
    }



    private void OpenSceneMenu()
    {
        UIMenuScene.Instance.ShowCamera();
        UIMenuScene.Instance.SetView(EViewGame.Lobby);
        SoundManager.Instance.PlaySoundMenu();
    }


    private void OpenSceneBattle()
    {
        UIMenuScene.Instance.HideCamera();
        SoundManager.Instance.PlaySoundBattle();
    }



    // <summary> Cheat_LoadViewEditor </summary>
    [ContextMenu("Cheat_LoadViewEditor")]
    public void LoadViewEditor()
    {
        var views = GetComponentsInChildren<PopupBase>();
        popupGames = new List<PopupBase>(views);
    }


    // <summary> Cheat_HideAllView </summary>
    [ContextMenu("Cheat_HideAllView")]
    public void HideAllView()
    {
        popupGames.ForEach(x => x.CheatHidePopup());
    }


}

