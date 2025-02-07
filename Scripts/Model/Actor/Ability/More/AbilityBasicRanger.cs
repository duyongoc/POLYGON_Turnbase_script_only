using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBasicRanger : MonoBehaviour
{


    public void DoAction(ActorBase owner, DataAttackBase attack, Transform attackPoint, Action cbFinish)
    {
        var ranger = attack as DataAttackRanger;
        var prefab = ranger.prefabShoot.SpawnToGarbage(attackPoint.position, attackPoint.rotation);
        prefab.GetComponent<BulletBase>().Init(owner, ranger);
    }

}
