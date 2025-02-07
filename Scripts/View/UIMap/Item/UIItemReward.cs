using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemReward : MonoBehaviour
{
    
    
    [Header("[Setting]")]
    [SerializeField] private Image imgAvatar;


    // [private]
    private string _key;
    private Action<string> _cbClicked;



    public void Init(string key, Action<string> callback)
    {
        _key = key;
        _cbClicked = callback;

        LoadData();
    }


    private void LoadData()
    {
        imgAvatar.sprite = GameController.Instance.LoadSpriteCharacter(_key);
        imgAvatar.SetActive(true);
    }


    public void OnClickButtonItem()
    {
        // _cbClicked.CheckInvoke(_key);
    }


}
