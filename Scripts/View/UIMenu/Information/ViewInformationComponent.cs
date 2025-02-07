using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewInformationComponent : MonoBehaviour
{


    [Header("[Scrolling prefab]")]
    [SerializeField] private float scrollDefault = 100;
    [SerializeField] private float scrollOffset = 100;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private UIItemActor prefabItem;
    [SerializeField] private List<UIItemActor> cachePrefabs;

    [Header("[Actor Skill]")]
    [SerializeField] private Transform tranSkill;
    [SerializeField] private UIItemActorSkill prefabActorSkill;

    [Header("[Text]")]
    [SerializeField] private TMP_Text txtCoin;
    [SerializeField] private TMP_Text txtGem;
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private TMP_Text txtLevel;

    [Header("[Text Stats]")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtPhysicAttack;
    [SerializeField] private TMP_Text txtPhysicDefense;
    [SerializeField] private TMP_Text txtMagicAttack;
    [SerializeField] private TMP_Text txtMagicDefense;
    [SerializeField] private TMP_Text txtAttackSpeed;

    [Header("[Button Tab]")]
    [SerializeField] private List<UIItemTabInformation> tabInformations;


    [Header("[Cache]")]
    [SerializeField] private List<UIItemActorSkill> cacheActorSkills;


    // [private]
    private string _key;




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
        LoadCharacter(GameManager.Instance.Data.GetCharacters());
        LoadTabInformation();
        SelectDefault();
    }


    private void SelectDefault()
    {
        if (cachePrefabs == null || cachePrefabs.Count <= 0)
            return;

        cachePrefabs[0].OnClickButtonItem();
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
            prefab.Init(item, CallbackCharacter);
            cachePrefabs.Add(prefab);
            ChangeScrollSize(scrollOffset);
        }
    }


    private void CallbackCharacter(string key, bool isLocked)
    {
        _key = key;

        LoadActorData();
        LoadActorSkill();
        UnSelectPrefab();
        this.PostEvent(EventID.ShowCharacter, _key);
    }



    private void LoadActorData()
    {
        var actor = ConfigManager.Instance.GetCharacterItemByKey(_key);
        if (actor == null)
            return;

        txtName.SetText(actor.name);
        txtLevel.SetText("1");

        // load actor stats  
        txtHealth.SetText(actor.calculationAttributes.hp.ToString());
        txtPhysicAttack.SetText(actor.calculationAttributes.pAtk.ToString());
        txtPhysicDefense.SetText(actor.calculationAttributes.pDef.ToString());
        txtMagicAttack.SetText(actor.calculationAttributes.mAtk.ToString());
        txtMagicDefense.SetText(actor.calculationAttributes.mDef.ToString());
        txtAttackSpeed.SetText(actor.calculationAttributes.spd.ToString());
    }



    private void LoadActorSkill()
    {
        var actor = ConfigManager.Instance.GetCharacterItemByKey(_key);
        if (actor == null)
            return;

        cacheActorSkills.ForEach(x => { if (x) Destroy(x.gameObject); });
        foreach (var item in actor.skills)
        {
            var ui = Instantiate(prefabActorSkill, tranSkill);
            ui.Init(item, CallbackActorSkill);
            cacheActorSkills.Add(ui);
        }
    }


    private void CallbackActorSkill(string key)
    {
        UIMenuScene.Instance.Model.PlayMainRoratorAnimation(key);
    }



    private void LoadTabInformation()
    {
        // tabInformations.ForEach(x => x.Init(CallbackTab));
    }

    private void CallbackTab(string key)
    {
        tabInformations.ForEach(x => x.OnUnSelectItem());
        print("click tab: " + key);
    }



    private void UnSelectPrefab()
    {
        foreach (var ui in cachePrefabs)
            ui.OnItemUnselect();
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




}
