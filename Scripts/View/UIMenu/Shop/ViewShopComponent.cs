using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewShopComponent : MonoBehaviour
{


    public enum EComponent
    {
        Chest = 0,
        Gold = 1,
        Gem = 2,
    }


    [Header("[Component]")]
    [SerializeField] private EComponent _component;
    [SerializeField] private ComponentShopGold compGold;
    [SerializeField] private ComponentShopGem compGem;
    [SerializeField] private ComponentShopChest compChest;


    [Header("[Text]")]
    [SerializeField] private TMP_Text txtGem;
    [SerializeField] private TMP_Text txtGold;


    [Header("[Button]")]
    [SerializeField] private List<UIButtonClickHover> buttonTabs;


    // [private]
    private ComponentState _stateComponent;




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
        compChest.Load();
        compGold.Load();
        compGem.Load();
        
        LoadData();
        UpdateButtonChoosen();
        SetViewComponent(_component);
    }


    private void LoadData()
    {
        var userInfo = GameManager.Instance.Data.UserInfo;
        txtGem.SetText(userInfo.gem.ToString());
        txtGold.SetText(userInfo.gold.ToString());
    }


    public void SetViewComponent(EComponent character)
    {
        _component = character;
        switch (_component)
        {
            case EComponent.Chest:
                SetCurrentComponent(compChest); break;
            case EComponent.Gold:
                SetCurrentComponent(compGold); break;
            case EComponent.Gem:
                SetCurrentComponent(compGem); break;
        }
    }


    private void SetCurrentComponent(ComponentState newComponent)
    {
        if (_stateComponent != null)
            _stateComponent.EndState();

        _stateComponent = newComponent;
        _stateComponent.StartState();
        SetActiveComponent(_component.ToString());
    }


    private void SetActiveComponent(string nameComponent)
    {
        compChest.gameObject.SetActive(compChest.name.Contains(nameComponent));
        compGold.gameObject.SetActive(compGold.name.Contains(nameComponent));
        compGem.gameObject.SetActive(compGem.name.Contains(nameComponent));
    }


    private void ClearButtonSelected()
    {
        buttonTabs.ForEach(x => x.OnChoosenUnselect());
    }


    private void UpdateButtonChoosen()
    {
        buttonTabs.ForEach(x => x.OnChoosenUnselect());
        buttonTabs[(int)_component]?.OnChoosenSelect();
    }



    public void OnClickButtonChest()
    {
        SetViewComponent(EComponent.Chest);
        ClearButtonSelected();
        UpdateButtonChoosen();
    }


    public void OnClickButtonGold()
    {
        SetViewComponent(EComponent.Gold);
        ClearButtonSelected();
        UpdateButtonChoosen();
    }


    public void OnClickButtonGem()
    {
        SetViewComponent(EComponent.Gem);
        ClearButtonSelected();
        UpdateButtonChoosen();
    }



}
