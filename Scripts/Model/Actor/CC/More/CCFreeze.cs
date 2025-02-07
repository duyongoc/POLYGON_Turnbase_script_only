using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CCFreeze : MonoBehaviour
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
        owner.SetAnimationSpeed(0);
        owner.Effect.ApplyEffectFreeze(data.duration, 0);

        // add sequense effect sleep
        ccSequence = DOTween.Sequence();
        ccSequence.AppendInterval(data.duration);
        ccSequence.AppendCallback(() =>
        {
            // owner.SetCanMove();
            // owner.IsAttacking = false;
            // owner.CC.CheckCCBonus(data.bonus);
            owner.IsStun = false;
            owner.SetAnimationSpeed(1);

            cbFinish.CheckInvoke();
            ccSequence.Kill(true);
        });
    }



    // private void ActionStart() { }
    // private void ActionFinish() { }


}
