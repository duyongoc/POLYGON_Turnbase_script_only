using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemStage : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private TMP_Text txtLevel;
    [SerializeField] private TMP_Text txtLevelCompleted;
    [SerializeField] private GameObject objLock;
    [SerializeField] private GameObject objCurrent;
    [SerializeField] private GameObject objCompleted;


    // [private]
    private bool _isLocked;
    private DataStage _data;
    private Action<string> _cbClicked;


    // [properties]
    public DataStage Data { get => _data; set => _data = value; }



    public void Init(DataStage data, Action<string> callback)
    {
        _data = data;
        _cbClicked = callback;

        LoadData();
    }


    private void LoadData()
    {
        txtLevel.SetText(_data.key.Replace("stage_", ""));
        txtLevelCompleted.SetText(_data.key.Replace("stage_", ""));
    }


    public void SetStageLock()
    {
        _isLocked = true;
        objCompleted.SetActive(false);
        objCurrent.SetActive(false);
        objLock.SetActive(true);
    }


    public void SetStageCurrent()
    {
        _isLocked = false;
        objLock.SetActive(false);
        objCompleted.SetActive(false);
        objCurrent.SetActive(true);
    }


    public void SetStageCompleted()
    {
        _isLocked = false;
        objLock.SetActive(false);
        objCurrent.SetActive(false);
        objCompleted.SetActive(true);

    }


    public void OnClickButtonItem()
    {
        if (_isLocked)
            return;

        _cbClicked?.Invoke(_data.id);
    }


}
