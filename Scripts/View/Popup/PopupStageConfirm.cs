using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupStageConfirm : PopupBase
{


    [Header("[Unit Force]")]
    [SerializeField] private float scrollDefault = 100;
    [SerializeField] private float scrollOffset = 100;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private UIItemReward prefabItem;
    [SerializeField] private List<UIItemReward> cachePrefabs;



    public void Load(string[] rewards, Action cbOK, Action cbCancel)
    {
        Show(cbOK, cbCancel);
        LoadActor(rewards);
    }


    public void LoadActor(string[] rewards)
    {
        // clear prefab cache
        ClearCache();
        ResetScrollSize(scrollDefault);

        // create prefab
        foreach (var item in rewards)
        {
            var prefab = Instantiate(prefabItem, scrollContent.transform, false);
            prefab.Init(item, CallbackClick);

            cachePrefabs.Add(prefab);
            ChangeScrollSize(scrollOffset);
        }
    }


    private void CallbackClick(string key)
    {
        // this.PostEvent(EventID.ShowCharacter, key);
    }


    private void ClearCache()
    {
        cachePrefabs.ForEach(x => { if (x != null) Destroy(x.gameObject); });
        cachePrefabs.Clear();
    }


    private void ResetScrollSize(float value)
    {
        var scroll = scrollContent.AsRectTransform();
        scroll.sizeDelta = new Vector2(scroll.sizeDelta.x, value);
    }


    private void ChangeScrollSize(float value)
    {
        var scroll = scrollContent.AsRectTransform();
        scroll.sizeDelta = new Vector2(scroll.sizeDelta.x + value, scroll.sizeDelta.y);
    }



}
