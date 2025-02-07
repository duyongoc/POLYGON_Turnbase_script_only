using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLobby : ViewBase
{


    [Header("[Component]")]
    [SerializeField] private ViewLobbyComponent compLobby;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    #region STATE
    public override void UpdateState() { base.UpdateState(); }
    public override void StartState()
    {
        base.StartState();
        UIMenuScene.Instance.Model.ShowUiRotators();
        UIMenuScene.Instance.Tutorial.ShowTutorial();
    }
    public override void EndState()
    {
        base.EndState();
        UIMenuScene.Instance.Model.HideUiRotators();
        UIMenuScene.Instance.Tutorial.HideTutorial();
    }
    #endregion




    public void Load()
    {
        compLobby.Load();
    }


    public void OnClickButtonReturn()
    {
    }




    public void OnClickButtonSetting()
    {
        ViewManager.Instance.ShowPopupSetting();
    }


    public void OnClickButtonShop()
    {
        UIMenuScene.Instance.SetView(EViewGame.Shop);
    }


    public void OnClickButtonGuild()
    {
        UIMenuScene.Instance.SetView(EViewGame.Guild);
    }


    public void OnClickButtonBoss()
    {
        // UIMenuScene.Instance.SetView(EViewGame.Boss);
        ViewManager.Instance.ShowPopupOkCancel("INFO", "This is challenge mode! \nDo you want to try?",
        () =>
        {
            ConfigManager.Instance.SetChallengeMode();
            ViewManager.Instance.ShowLoading(1, ViewManager.Instance.LoadSceneBattle);
        });
    }


    public void OnClickButtonBattlePass()
    {
        UIMenuScene.Instance.SetView(EViewGame.BattlePass);
    }


    public void OnClickButtonRoulette()
    {
        UIMenuScene.Instance.SetView(EViewGame.Roulette);
    }


    public void OnClickButtonPlay()
    {
        UIMenuScene.Instance.Tutorial.UpdateNextStep();
        UIMenuScene.Instance.SetView(EViewGame.Stage);
    }


    public void OnClickButtonInformation()
    {
        UIMenuScene.Instance.Tutorial.UpdateNextStep();
        UIMenuScene.Instance.SetView(EViewGame.Information);
    }


}
