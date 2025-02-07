using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRanger : BulletBase
{



    public override void UpdateMovement()
    {
        // normal bullet movement
        MoveForwardTheEnemy();
    }


    public override void Init(ActorBase actor, DataAttackRanger data)
    {
        attack = data;
        enemy = actor.CurrentEnemy;
        owner = actor.GetComponent<ActorBase>();

        LookAtEnemy();
        SpawnVFXFlash(transform.position, transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        var victim = other.GetComponent<ActorBase>();
        if (victim == null || !owner.IsEnemy(victim))
            return;

        // add damage to the target
        owner.ApplyDamagePhysic(victim, attack);
        SpawnVFXHit(victim.ActorPointHit.position);
        SpawnVFXExplosion(victim.ActorPointHit.position);

        // destroy the bullet
        CheckDestroy();
    }


}
