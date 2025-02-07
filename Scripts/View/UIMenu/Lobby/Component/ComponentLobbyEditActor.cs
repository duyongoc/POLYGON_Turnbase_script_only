using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentLobbyEditActor : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private float scrollDefault = 100;
    [SerializeField] private float scrollOffset = 100;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private UIItemActor prefabItem;
    [SerializeField] private List<UIItemActor> cachePrefabs;


    // [private]
    private string _key;
    private bool _itemLocked;
    private Action<string> _cbConfirmed;
    private Action<string> _cbItemClicked;



    public void Load(Action<string> cbConfirm, Action<string> cbItemClick)
    {
        _cbConfirmed = cbConfirm;
        _cbItemClicked = cbItemClick;

        LoadCharacter(GameManager.Instance.Data.GetCharacters());
        LoadFormationStatus(GameManager.Instance.Data.GetFormation());
    }


    private void LoadCharacter(List<string> actors)
    {
        // clear prefab cache
        ClearCache();
        ResetScrollSize(scrollDefault);

        // create prefab
        foreach (var item in actors)
        {
            var prefab = Instantiate(prefabItem, scrollContent.transform, false);

            prefab.Init(item, Callback_ItemClick);
            cachePrefabs.Add(prefab);
            ChangeScrollSize(scrollOffset);
        }
    }


    public void LoadFormationStatus(List<DataFormation> formation)
    {
        if (formation == null || formation.Count < 0)
            return;

        var result = cachePrefabs.Where(x => formation.Where(value => !string.IsNullOrEmpty(value.character))
                                                       .Any(value => value.character.Equals(x.Data)))
                                   .ToList();

        result.ForEach(x =>
        {
            x.OnHoverSelect();
            x.OnItemUnselect();
        });
    }



    private void Callback_ItemClick(string key, bool isLocked)
    {
        _key = key;
        _itemLocked = isLocked;
        _cbItemClicked?.Invoke(_key);

        UnSelectItems();
    }



    private void UnSelectItems()
    {
        foreach (var ui in cachePrefabs)
            ui.OnItemUnselect();
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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
        scroll.sizeDelta = new Vector2(scroll.sizeDelta.x, scroll.sizeDelta.y + value);
    }


    public void OnClickButtonCancel()
    {
        Hide();
    }


    public void OnClickButtonConfirm()
    {
        _cbConfirmed?.Invoke(_key);
        Hide();
    }




}
