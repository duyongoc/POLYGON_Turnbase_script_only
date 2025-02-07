using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemActorType : MonoBehaviour
{


    [Header("[Hover]")]
    [SerializeField] private UIButtonClickHover ui_Button;

    [Header("[UI]")]
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private TMP_Text txtNameHover;
    [SerializeField] private Image imgActorType;
    [SerializeField] private Image imgActorTypeHover;


    // [private]
    private DataActor _data;
    private Action<string> _cbClicked;




    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    public void Init(DataActor data, Action<string> callback)
    {
        _data = data;
        _cbClicked = callback;

        LoadData();
    }


    private void LoadData()
    {
        txtName.SetText(_data.name);
        txtNameHover.SetText(_data.name);
        // imgActorType.sprite = GameController.Instance.LoadSpriteActorType(_data.type);
        // imgActorTypeHover.sprite = GameController.Instance.LoadSpriteActorType(_data.type);
    }


    public void OnSelectItem()
    {
        ui_Button?.OnChoosenSelect();
    }


    public void OnUnSelectItem()
    {
        ui_Button?.OnChoosenUnselect();
    }


    public void OnClickedButtonItem()
    {
        _cbClicked.CheckInvoke(_data.key);
        OnSelectItem();
    }


}
