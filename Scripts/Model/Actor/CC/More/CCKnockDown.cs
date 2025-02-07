using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CCKnockDown : MonoBehaviour
{


    // public event Action eventStart;
    // public event Action eventFinish;


    // [private ]
    private Sequence ccSequence;



    public void KillAction()
    {
        ccSequence.Kill(true);
    }


    public void DoAction(ActorBase owner, SOCC data, Action cbFinish)
    {
        // eventStart?.Invoke();
        var ccKnockDown = data as SOCCKnockDown;
        var duration = ccKnockDown.duration;
        var force = ccKnockDown.forceKnockBack;

        // owner.CanMove = false;
        // owner.Rigidbody.isKinematic = true;
        // owner.SetStopMove();
        // owner.SetAnimGetUpFalse();
        owner.IsDownOnGround = true;
        owner.PlayAnimationKnockDown();
        owner.LookAtZeroCurrentEnemy();

        // get vector knock back from the enemy or it's weapon or something
        var positionBack = owner.CurrentEnemy != null ? owner.CurrentEnemy.Position : -owner.transform.forward;
        var nomalized = (positionBack - owner.Position).normalized * force;
        var direction = owner.Position - nomalized;


        // play sequense
        ccSequence = DOTween.Sequence();
        ccSequence.Append(owner.transform.DOMove(direction, .75f));
        ccSequence.AppendInterval(duration);
        ccSequence.AppendCallback(owner.StopAnimationKnockDown);
        ccSequence.AppendInterval(1);
        ccSequence.AppendCallback(() =>
        {
            // owner.CanMove = true;
            // owner.IsStun = false; 
            // owner.SetCanMove();
            owner.IsDownOnGround = false;
            owner.OnFinishedAttack();
            // owner.SetNavAgentEnable();
            // owner.ActorCC.CheckCCBonus(data.bonus); 

            cbFinish.CheckInvoke();
            // eventFinish?.Invoke();
            ccSequence.Kill(true);
        });
    }



    // private void ActionStart() { }
    // private void ActionFinish() { }

}
