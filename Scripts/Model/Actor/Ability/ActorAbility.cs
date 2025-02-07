using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAbility : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private AbilityHeal abilityHeal;
    [SerializeField] private AbilityBasicMelee basicMelee;
    [SerializeField] private AbilityBasicRanger basicRanger;
    [SerializeField] private AbilityLocationTarget locationTarget;



    // [private]
    private ActorBase owner;



    public void Init(ActorBase actor)
    {
        owner = actor;
    }



    public void DoTrigger(UnitBase target, DataAttackBase data, Transform attackPoint, Action cbFinish = null)
    {
        switch (data.type)
        {
            case EAttackType.Melee:
                DoMelee(target, data, attackPoint, cbFinish); break;

            case EAttackType.Ranger:
                DoRanger(target, data, attackPoint, cbFinish); break;

            case EAttackType.LocaltionTarget:
                DoLocationTarget(target, data, cbFinish); break;
        }
    }


    // do basic melee attack
    private void DoMelee(UnitBase target, DataAttackBase data, Transform attackPoint, Action cbFinish)
    {
        basicMelee.DoAction(owner, data, attackPoint, cbFinish);
    }

    // do basic ranger attack
    private void DoRanger(UnitBase target, DataAttackBase data, Transform attackPoint, Action cbFinish)
    {
        basicRanger.DoAction(owner, data, attackPoint, cbFinish);
    }

    // do attack to target's localtion
    private void DoLocationTarget(UnitBase target, DataAttackBase data, Action cbFinish)
    {
        locationTarget.DoAction(owner, target, data, cbFinish);
    }




}
