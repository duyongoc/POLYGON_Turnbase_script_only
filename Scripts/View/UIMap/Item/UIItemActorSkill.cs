using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemActorSkill : MonoBehaviour
{


    [Header("[Image]")]
    [SerializeField] private Image imgAvatar;

    [Header("[Text]")]
    [SerializeField] private GameObject panelDescription;
    [SerializeField] private TMP_Text txtDescription;


    // [private]
    private BaseSkill _skill;
    private Action<string> _cbClicked;



    public void Init(BaseSkill skill, Action<string> callback)
    {
        _skill = skill;
        _cbClicked = callback;

        LoadData();
        HideDescription();
    }


    private void LoadData()
    {
        txtDescription.text = _skill.GetDesciption();
        imgAvatar.sprite = _skill.icon;
        imgAvatar.SetActive(true);
    }


    public void ShowDescription()
    {
        panelDescription.SetActive(true);
    }


    public void HideDescription()
    {
        panelDescription.SetActive(false);
    }


    public void OnClickButtonItem()
    {
        // _cbClicked.CheckInvoke(_skill.key);
    }


}
