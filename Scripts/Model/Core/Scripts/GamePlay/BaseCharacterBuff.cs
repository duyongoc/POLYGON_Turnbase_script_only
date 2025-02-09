﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterBuff
{


    public BaseCharacterEntity Giver { get; protected set; }
    public BaseCharacterEntity Receiver { get; protected set; }
    public BaseSkill Skill { get; protected set; }
    public int Level { get; protected set; }
    public int BuffIndex { get; protected set; }
    public string Id { get { return Skill.Id + "_" + BuffIndex; } }

    public BaseSkillBuff Buff { get { return Buffs[BuffIndex]; } }
    public CalculationAttributes Attributes { get { return Buff.GetAttributes(Level); } }
    public float PAtkHealRate { get { return Buff.GetPAtkHealRate(Level); } }
    public float MAtkHealRate { get { return Buff.GetMAtkHealRate(Level); } }

    private List<BaseSkillBuff> buffs;
    protected readonly List<GameEffect> effects = new List<GameEffect>();
   
   
    public List<BaseSkillBuff> Buffs
    {
        get
        {
            if (buffs == null)
                buffs = Skill.GetBuffs();
            return buffs;
        }
    }

    public BaseCharacterBuff(int level, BaseSkill skill, int buffIndex, BaseCharacterEntity giver, BaseCharacterEntity receiver)
    {
        Level = level;
        Skill = skill;
        BuffIndex = buffIndex;
        Giver = giver;
        Receiver = receiver;

        if (Buff.buffEffects != null)
            effects.AddRange(Buff.buffEffects.InstantiatesTo(receiver));
    }

    public void BuffRemove()
    {
        foreach (var effect in effects)
        {
            if (effect != null)
                effect.DestroyEffect();
        }
        effects.Clear();
    }

    public abstract float GetRemainsDurationRate();
    public abstract float GetRemainsDuration();

}
