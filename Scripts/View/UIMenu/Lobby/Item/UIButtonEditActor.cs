using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEditActor : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private string key;
    [SerializeField] private UIModelRotator modelRotator;


    // [private]
    private int _index;
    private Action<string, int> _cbClicked;


    // [properties]
    public string Key { get => key; }
    public UIModelRotator ModelRotator { get => modelRotator; }



    public void Init(int index, UIModelRotator model, Action<string, int> callback)
    {
        _index = index;
        _cbClicked = callback;
        modelRotator = model;

        Show();
    }


    public void LoadModel(string newKey)
    {
        if (string.IsNullOrEmpty(newKey))
            return;
            
        key = newKey;
        modelRotator.LoadModel(key);
    }


    public void UnloadModel()
    {
        key = null;
        modelRotator.ClearCache();
    }



    private void Show()
    {
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }


    public void OnClickButtonItem()
    {
        _cbClicked?.Invoke(key, _index);
    }


}
