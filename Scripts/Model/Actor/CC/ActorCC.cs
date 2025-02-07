using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCC : MonoBehaviour
{



    [Header("[Setting]")]
    [SerializeField] private CCKnockBack ccKnockBack;
    [SerializeField] private CCKnockDown ccKnockDown;
    [SerializeField] private CCStun ccStun;
    [SerializeField] private CCFreeze ccFreeze;

    // [SerializeField] private CCBurn ccBurn;
    // [SerializeField] private CCStand ccStand;
    // [SerializeField] private CCChaos ccChaos;
    // [SerializeField] private CCPoison ccPoison;



    // [private]
    private ActorBase owner;



    public void Init(ActorBase actor)
    {
        owner = actor;
    }



    public void KnockBack(SOCC data, Action cbFinish)
    {
        ccKnockBack.DoAction(owner, data, cbFinish);
    }


    public void KnockDown(SOCC data, Action cbFinish)
    {
        // kill stun to avoid stun callback function 
        ccStun.KillAction();
        ccKnockDown.KillAction();
        ccKnockDown.DoAction(owner, data, cbFinish);
    }


    public void Stun(SOCC data, Action cbFinish)
    {
        ccStun.DoAction(owner, data, cbFinish);
    }


    public void Freeze(SOCC data, Action cbFinish)
    {
        ccFreeze.DoAction(owner, data, cbFinish);
    }



    public void CheckCCBonus(SOCC data)
    {
        if (owner.IsDead)
            return;

        owner.UpdateHit(data, false);
    }




    // public void Sleep(SOCC data, Action cbFinish)
    // {
    //     ccSleep.DoAction(owner, data, cbFinish);
    // }

    // public void Lying(SOCC data, Action cbFinish)
    // {
    //     ccLying.DoAction(owner, data, cbFinish);
    // }

    // public void Chaos(SOCC data, Action cbFinish = null)
    // {
    //     ccChaos.DoAction(owner, data, cbFinish);
    // }


    // public void Slow(SOCC data, Action cbFinish = null)
    // {
    //     ccSlow.DoAction(owner, data, cbFinish);
    // }

    // public void Stand(SOCC data, Action cbFinish = null)
    // {
    //     ccStand.DoAction(owner, data, cbFinish);
    // }

    // public void Burn(SOCC data, Action cbFinish = null)
    // {
    //     ccBurn.DoAction(owner, data, cbFinish);
    // }

    // public void Poison(SOCC data, Action cbFinish = null)
    // {
    //     ccPoison.DoAction(owner, data, cbFinish);
    // }


}
