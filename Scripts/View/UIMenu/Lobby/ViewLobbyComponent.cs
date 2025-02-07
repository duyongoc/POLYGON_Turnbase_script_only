using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ViewLobbyComponent : MonoBehaviour
{


    [Header("[Component]")]
    [SerializeField] private ComponentLobbyEditActor compChangeActor;


    [Header("[Text]")]
    [SerializeField] private TMP_Text txtGem;
    [SerializeField] private TMP_Text txtGold;
    [SerializeField] private TMP_Text txtUserName;
    [SerializeField] private TMP_Text txtUserLevel;
    [SerializeField] private TMP_Text txtLevelExp;


    [Header("[Setting]")]
    [SerializeField] private List<UIButtonEditActor> ui_BtnEditActors;


    [Header("[DATA]")]
    [SerializeField] private UserInfo userInfo;
    [SerializeField] private List<UIModelRotator> ui_Rotators;


    // [private]
    private int _editIndex;
    private string _editKey;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    public void Load()
    {
        userInfo = GameManager.Instance.Data.UserInfo;
        ui_Rotators = UIMenuScene.Instance.Model.Ui_Rotators;

        LoadData();
        LoadComponent();
        LoadUIFormation();
        LoadUIButtonEdit();
    }


    private void LoadData()
    {
        txtGem.SetText(userInfo.gem.ToString());
        txtGold.SetText(userInfo.gold.ToString());
        txtUserLevel.SetText(userInfo.level.ToString());
        txtUserName.SetText(userInfo.displayName.ToString());
        txtLevelExp.SetText($"{userInfo.expLevel}/{userInfo.maxExpLevel}");
    }


    private void LoadComponent()
    {
        compChangeActor.Load(Callback_ClickConfirm, Callback_ClickItem);
    }


    public void LoadUIFormation()
    {
        if (userInfo.formation == null || userInfo.formation.Count <= 0)
            return;

        for (int i = 0; i < ui_Rotators.Count; i++)
        {
            ui_Rotators[i].Show();
            ui_Rotators[i].LoadModel(userInfo.formation[i].character);
        }
    }


    private void LoadUIButtonEdit()
    {
        // init ui edit btn actors
        for (int i = 0; i < ui_BtnEditActors.Count; i++)
        {
            ui_BtnEditActors[i].Init(i, ui_Rotators[i], Callback_ClickBtnEdit);
            ui_BtnEditActors[i].LoadModel(userInfo.formation[i].character);
        }
    }


    private void UnLoadModelUiEditActor(string key)
    {
        foreach (var ui in ui_BtnEditActors)
        {
            if (string.IsNullOrEmpty(ui.Key))
                continue;

            if (ui.Key.Equals(key))
                ui.UnloadModel();
        }
    }




    private void Callback_ClickBtnEdit(string key, int index)
    {
        _editKey = key;
        _editIndex = index;

        compChangeActor.Show();
        compChangeActor.LoadFormationStatus(GameManager.Instance.Data.GetFormation());
    }



    private void Callback_ClickConfirm(string key)
    {
        // print($"Callback_ClickConfirm: key: {key} | _editKey: {_editKey} | _editIndex: {_editIndex}");
        if (string.IsNullOrEmpty(key) == false)
        {
            var item = userInfo.formation.Find(x => x.index == _editIndex);
            if (item != null)
            {
                GameManager.Instance.Data.RemoveFormationByKey(key);
                UnLoadModelUiEditActor(key);

                GameManager.Instance.Data.SaveFormationWithIndex(key, _editIndex);
                ui_BtnEditActors[_editIndex].LoadModel(key);
            }
        }
    }


    private void Callback_ClickItem(string item)
    {
        // print($"Callback_ClickItem: " + item);
    }


}
