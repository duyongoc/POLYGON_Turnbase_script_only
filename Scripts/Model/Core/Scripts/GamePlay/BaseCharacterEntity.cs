﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class BaseCharacterEntity : MonoBehaviour
{

    public const string ANIM_ACTION_STATE = "_Action";


    [Header("Animator")]
    public RuntimeAnimatorController animatorController;
    public Animator cacheAnimator;
    protected AnimatorOverrideController cacheAnimatorController;

    [Header("Entities Containers")]
    [Tooltip("The transform where we're going to spawn uis")]
    public Transform uiContainer;

    [Tooltip("The transform where we're going to spawn body effects")]
    public Transform bodyEffectContainer;

    [Tooltip("The transform where we're going to spawn floor effects")]
    public Transform floorEffectContainer;

    [Tooltip("The transform where we're going to spawn damage")]
    public Transform damageContainer;


    public CharacterItem Data;
    public BaseGamePlayFormation Formation;
    public List<BaseCharacterSkill> Skills = new List<BaseCharacterSkill>();
    public List<BaseAttackAnimationData> AttackAnimations { get { return Data.attackAnimations; } }
    public Dictionary<string, BaseCharacterBuff> Buffs = new Dictionary<string, BaseCharacterBuff>();
    public float hp;
    public int Position;


    private Transform container;
    private Transform tempTransform;



    public int MaxHp
    {
        get { return (int)GetTotalAttributes().hp; }
    }

    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
                Dead();

            if (hp >= MaxHp)
                hp = MaxHp;
        }
    }

    public Transform Container
    {
        get { return container; }
        set
        {
            container = value;
            TempTransform.SetParent(container);
            TempTransform.localPosition = Vector3.zero;
            TempTransform.localEulerAngles = Vector3.zero;
            TempTransform.localScale = Vector3.one;
            gameObject.SetActive(true);
        }
    }

    public Transform TempTransform
    {
        get
        {
            if (tempTransform == null)
                tempTransform = GetComponent<Transform>();
            return tempTransform;
        }
    }

    protected virtual void Awake()
    {
        if (uiContainer == null)
            uiContainer = TempTransform;

        if (bodyEffectContainer == null)
            bodyEffectContainer = TempTransform;

        if (floorEffectContainer == null)
            floorEffectContainer = TempTransform;

        if (damageContainer == null)
            damageContainer = TempTransform;
    }


    public void Init(CharacterItem item)
    {
        Data = item;
        Skills.Clear();
        Revive();

        foreach (var skill in item.skills)
        {
            if (skill != null)
            {
                // TODO: Implement skill level
                Skills.Add(NewSkill(1, skill));
            }
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        var cacheAnimator = GetComponent<Animator>();
        if (animatorController == null && cacheAnimator != null)
            animatorController = cacheAnimator.runtimeAnimatorController;

        EditorUtility.SetDirty(gameObject);
    }
#endif


    public void Revive()
    {
        // if (Item == null)
        //     return;

        Hp = MaxHp;
    }

    public virtual void Dead()
    {
        var keys = new List<string>(Buffs.Keys);
        for (var i = keys.Count - 1; i >= 0; --i)
        {
            var key = keys[i];
            if (!Buffs.ContainsKey(key))
                continue;

            var buff = Buffs[key];
            buff.BuffRemove();
            Buffs.Remove(key);
        }
    }

    public CalculationAttributes GetTotalAttributes()
    {
        var result = Data.calculationAttributes;
        // var equipmentBonus = Item.EquipmentBonus;
        // result += equipmentBonus;

        var buffs = new List<BaseCharacterBuff>(Buffs.Values);
        foreach (var buff in buffs)
        {
            result += buff.Attributes;
        }

        // If this is character item, applies rate attributes
        result.hp += Mathf.CeilToInt(result.hpRate * result.hp);
        result.pAtk += Mathf.CeilToInt(result.pAtkRate * result.pAtk);
        result.pDef += Mathf.CeilToInt(result.pDefRate * result.pDef);
        result.mAtk += Mathf.CeilToInt(result.mAtkRate * result.mAtk);
        result.mDef += Mathf.CeilToInt(result.mDefRate * result.mDef);
        result.spd += Mathf.CeilToInt(result.spdRate * result.spd);
        result.eva += Mathf.CeilToInt(result.evaRate * result.eva);
        result.acc += Mathf.CeilToInt(result.accRate * result.acc);

        result.hpRate = 0;
        result.pAtkRate = 0;
        result.pDefRate = 0;
        result.mAtkRate = 0;
        result.mDefRate = 0;
        result.spdRate = 0;
        result.evaRate = 0;
        result.accRate = 0;

        return result;
    }

    public virtual void SetFormation(BaseGamePlayFormation formation, int position, Transform container)
    {
        if (container == null)
            return;

        Formation = formation;
        Position = position;
        Container = container;
    }

    public virtual void ApplyBuff(BaseCharacterEntity caster, int level, BaseSkill skill, int buffIndex)
    {
        if (skill == null || buffIndex < 0 || buffIndex >= skill.GetBuffs().Count || skill.GetBuffs()[buffIndex] == null || Hp <= 0)
            return;

        var buff = NewBuff(level, skill, buffIndex, caster, this);
        if (buff.GetRemainsDuration() > 0f)
        {
            // Buff cannot stack so remove old buff
            if (Buffs.ContainsKey(buff.Id))
            {
                buff.BuffRemove();
                Buffs.Remove(buff.Id);
            }
            Buffs[buff.Id] = buff;
        }
        else
            buff.BuffRemove();
    }


    public abstract BaseCharacterSkill NewSkill(int level, BaseSkill skill);
    public abstract BaseCharacterBuff NewBuff(int level, BaseSkill skill, int buffIndex, BaseCharacterEntity giver, BaseCharacterEntity receiver);
}
