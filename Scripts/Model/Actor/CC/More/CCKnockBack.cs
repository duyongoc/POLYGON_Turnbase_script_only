using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CCKnockBack : MonoBehaviour
{
    

    // [private ]
    private Sequence ccSequence;



    public void DoAction(ActorBase owner, SOCC data, Action cbFinish)
    {
        print("do action knockback with nothing");
        // var ccKnockback = (SOCCKnockBack)data;
        // var force = ccKnockback.forceKnockBack;

        // // get vector knock back from the enemy or it's weapon or something
        // var vecKnockBack = owner.PointKnockBack != null ? owner.PointKnockBack : owner.CurrentEnemy.transform;
        // var nomalized = (vecKnockBack.position - transform.position).normalized * force;
        // var direction = transform.position - nomalized;
        
        // owner.transform.LookAt(vecKnockBack);
        // owner.PlayAnimationHit();

        // // play sequense
        // ccSequence = DOTween.Sequence();
        // ccSequence.Append(owner.transform.DOMove(direction, .75f));
        // ccSequence.AppendCallback(() =>
        // {
        //     owner.CC.CheckCCBonus(data.bonus);
        //     cbFinish.CheckInvoke();
        //     ccSequence.Kill(true);
        // });
    }



    // private void ActionStart() { }
    // private void ActionFinish() { }

}
