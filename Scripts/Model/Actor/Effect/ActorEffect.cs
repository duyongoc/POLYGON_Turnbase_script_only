using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorEffect : MonoBehaviour
{


    [Header("[Debuff]")]
    [SerializeField] private EffectDebuffStun effectStun;
    [SerializeField] private EffectDebuffFreeze effectFreeze;


   // [private]
    private ActorBase owner;



    #region UNITY
    // private void Start()
    // {
    // }

    // private void FixedUpdate()
    // {
    // }
    #endregion




    public void Init(ActorBase actor)
    {
        owner = actor;
        effectStun.Init();
    }


    public void ApplyEffectStun(float duration, float bonusDamage, Action callbackFinish = null)
    {
        effectStun.Apply(duration, bonusDamage, callbackFinish);
    }

    public void ApplyEffectFreeze(float duration, float bonusDamage, Action callbackFinish = null)
    {
        effectFreeze.Apply(duration, bonusDamage, callbackFinish);
    }


}
