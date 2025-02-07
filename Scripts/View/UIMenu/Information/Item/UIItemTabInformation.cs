using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemTabInformation : MonoBehaviour
{
    

    [Header("[Hover]")]
    [SerializeField] private UIButtonClickHover ui_Selection;
    [SerializeField] private string key;


    // [private]
    // private DataActor _data;
    private Action<string> _cbClicked;




    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    public void Init(Action<string> callback)
    {
        // _data = data;
        _cbClicked = callback;

        LoadData();
    }


    private void LoadData()
    {
        
    }


    public void OnSelectItem()
    {
        ui_Selection.OnChoosenSelect();
    }


    public void OnUnSelectItem()
    {
        ui_Selection.OnChoosenUnselect();
    }


    public void OnClickedButtonItem()
    {
        _cbClicked?.Invoke(key);
        OnSelectItem();
    }


}
