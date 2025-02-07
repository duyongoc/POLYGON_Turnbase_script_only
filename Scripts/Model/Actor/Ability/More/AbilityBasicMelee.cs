using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBasicMelee : MonoBehaviour
{



    public void DoAction(ActorBase owner, DataAttackBase data, Transform attackPoint, Action cbFinish)
    {
        var hitColliders = Physics.OverlapSphere(attackPoint.position, data.damageRange);
        foreach (var hit in hitColliders)
        {
            var victim = hit.GetComponent<ActorBase>();
            if (victim == null || !owner.IsEnemy(victim))
                continue;

            // if (data.singleTarget)
            //     continue;

            owner.ApplyDamagePhysic(victim, data);
            data.SpawnVFXHit(victim.PointHitPosition);
            data.SpawnVFXExplosion(victim.PointHitPosition, Quaternion.identity);
        }

        data.SpawnVFXFlash(attackPoint.position, attackPoint.rotation);
    }

}
