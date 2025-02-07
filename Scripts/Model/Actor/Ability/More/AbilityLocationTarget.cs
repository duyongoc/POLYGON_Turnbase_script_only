using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLocationTarget : MonoBehaviour
{

    public void DoAction(ActorBase owner, UnitBase target, DataAttackBase data, Action cbFinish)
    {
        var ranger = data as DataAttackRanger;
        var prefab = ranger.prefabShoot.SpawnToGarbage(target.transform.position, Quaternion.identity);
        prefab.GetComponent<BulletBase>().Init(owner, ranger);
    }


}
