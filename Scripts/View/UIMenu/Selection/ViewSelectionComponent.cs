using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewSelectionComponent : MonoBehaviour
{


    [Header("[Actor Type]")]
    [SerializeField] private Transform tranType;
    [SerializeField] private UIItemActorType prefabActorType;

    [Header("[Actor Skill]")]
    [SerializeField] private Transform tranSkill;
    [SerializeField] private UIItemActorSkill prefabActorSkill;

    [Header("[Text Information]")]
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private TMP_Text txtType;
    [SerializeField] private TMP_Text txtDescription;
    [SerializeField] private Image imgActorType;

    [Header("[Text Stats]")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtPhysicAttack;
    [SerializeField] private TMP_Text txtPhysicDefense;
    [SerializeField] private TMP_Text txtMagicAttack;
    [SerializeField] private TMP_Text txtMagicDefense;
    [SerializeField] private TMP_Text txtAttackSpeed;

    [Header("[Cache]")]
    [SerializeField] private List<UIItemActorType> cacheActorTypes;
    [SerializeField] private List<UIItemActorSkill> cacheActorSkills;


    // [private]
    private DataActor _actor;



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
        // var actorList = ConfigManager.Instance.GameActorSelection;
        // for (int i = 0; i < actorList.Count; i++)
        // {
        //     var ui = Instantiate(prefabActorType, tranType);
        //     ui.Init(actorList[i], CallbackClicked);

        //     // add to the list
        //     cacheActorTypes.Add(ui);
        // }

        // cacheActorTypes[0]?.OnClickedButtonItem();
    }


    private void CallbackClicked(string key)
    {
        // _actor = ConfigManager.Instance.GetDataActorByKey(key);
        if (_actor == null)
            return;

        UnSelectItems();
        LoadActorData();
        LoadActorSkill();

        // fire event show character 
        this.PostEvent(EventID.ShowCharacter, key);
    }


    private void UnSelectItems()
    {
        foreach (var ui in cacheActorTypes)
            ui.OnUnSelectItem();
    }


    private void LoadActorData()
    {
        // load actor data
        txtName.SetText(_actor.name);
        txtType.SetText(_actor.type);
        txtDescription.SetText(_actor.description);
        // imgActorType.sprite = GameController.Instance.LoadSpriteActorType(_actor.type);

        // load actor stats  
        txtHealth.SetText(_actor.stats.hp.ToString());
        txtPhysicAttack.SetText(_actor.stats.p_attack.ToString());
        txtPhysicDefense.SetText(_actor.stats.p_defense.ToString());
        txtMagicAttack.SetText(_actor.stats.m_attack.ToString());
        txtMagicDefense.SetText(_actor.stats.m_defense.ToString());
        txtAttackSpeed.SetText(_actor.stats.attack_speed.ToString());
    }


    private void LoadActorSkill()
    {
        // cacheActorSkills.ForEach(x => { if (x) Destroy(x.gameObject); });
        // foreach (var item in _actor.skills)
        // {
        //     var ui = Instantiate(prefabActorSkill, tranSkill);
        //     ui.Init(item, CallbackActorSkill);
        //     cacheActorSkills.Add(ui);
        // }
    }


    private void CallbackActorSkill(string key)
    {
        UIMenuScene.Instance.Model.PlayMainRoratorAnimation(key);
    }


    public void OnSelectCharacter()
    {
        GameManager.Instance.Data.SaveInit(_actor);
        UIMenuScene.Instance.SetView(EViewGame.Lobby);
    }


}
