using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemActor : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private UIButtonClickHover ui_Button;
    [SerializeField] private Image imgAvatar;


    // [private]
    private bool _isLocked;
    private string _data;
    private Action<string, bool> _cbClicked;


    // [properties]
    public string Data { get => _data; }
    public bool IsLocked { get => _isLocked; }


    public void Init(string data, Action<string, bool> callback)
    {
        _data = data;
        _isLocked = false;
        _cbClicked = callback;

        LoadData();
    }


    private void LoadData()
    {
        imgAvatar.sprite = GameController.Instance.LoadSpriteCharacter(_data);
        imgAvatar.SetActive(true);
    }


    public void OnItemSelect()
    {
        ui_Button.OnChoosenSelect();
    }

    public void OnItemUnselect()
    {
        ui_Button.OnChoosenUnselect();
    }


    public void OnHoverSelect()
    {
        ui_Button.OnHoverSelect();
    }



    public void OnLockItem()
    {
        _isLocked = true;
    }

    public void OnUnlockItem()
    {
        _isLocked = false;
    }


    public void OnClickButtonItem()
    {
        _cbClicked?.Invoke(_data, _isLocked);
        OnItemSelect();
    }


}
