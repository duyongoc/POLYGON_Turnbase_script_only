using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CCStun : MonoBehaviour
{


    // [private ]
    private Sequence ccSequence;



    public void KillAction()
    {
        ccSequence.Kill(true);
    }


    public void DoAction(ActorBase owner, SOCC data, System.Action cbFinish)
    {
        // owner.SetStopMove();
        // owner.ResetAnimatorTrigger();
        // owner.LookAtZeroCurrentEnemy();

        owner.IsStun = true;
        owner.PlayAnimationStun();
        owner.Effect.ApplyEffectStun(data.duration, 0);

        // add sequense effect sleep
        ccSequence = DOTween.Sequence();
        ccSequence.AppendInterval(data.duration);
        ccSequence.AppendCallback(() =>
        {
            // owner.SetCanMove();
            // owner.IsAttacking = false;
            // owner.CC.CheckCCBonus(data.bonus);
            owner.IsStun = false;
            owner.StopAnimationStun();

            cbFinish.CheckInvoke();
            ccSequence.Kill(true);
        });
    }



    // private void ActionStart() { }
    // private void ActionFinish() { }


}
