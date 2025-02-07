using System;
using UnityEngine;


public enum EAttackType
{
    Melee,
    Ranger,
    LocaltionTarget,
}


[Serializable]
public class DataAttackBase
{
    [Header("[Common]")]
    public bool playAnimHit = false;
    public bool singleTarget = false;
    public float damageRange = .75f;
    public float damagePercent = 100;
    public EAttackType type = EAttackType.Melee;

    [Header("[Crowd Control]")]
    public SOCC cc;

    [Header("[Effect]")]
    public GameObject vfxHit;
    public GameObject vfxFlash;
    public GameObject vfxExplosion;



    public void SpawnVFXHit(Vector3 position)
    {
        if (vfxHit != null)
            vfxHit.SpawnToGarbage(position, vfxHit.transform.rotation);
    }
    public void SpawnVFXHit(Vector3 position, Quaternion rotation)
    {
        if (vfxHit != null)
            vfxHit.SpawnToGarbage(position, rotation);
    }


    public void SpawnVFXFlash(Vector3 position)
    {
        if (vfxFlash != null)
            vfxFlash.SpawnToGarbage(position, vfxFlash.transform.rotation);
    }
    public void SpawnVFXFlash(Vector3 position, Quaternion rotation)
    {
        if (vfxFlash != null)
            vfxFlash.SpawnToGarbage(position, rotation);
    }


    public void SpawnVFXExplosion(Vector3 position)
    {
        if (vfxExplosion != null)
            vfxExplosion.SpawnToGarbage(position, vfxFlash.transform.rotation);
    }
    public void SpawnVFXExplosion(Vector3 position, Quaternion rotation)
    {
        if (vfxExplosion != null)
            vfxExplosion.SpawnToGarbage(position, rotation);
    }

}


[Serializable]
public class DataAttackMelee : DataAttackBase
{
}


[Serializable]
public class DataAttackRanger : DataAttackBase
{
    [Header("[Ranger]")]
    public bool passThrough;
    public float moveSpeed = 8f;
    public GameObject prefabShoot;
}
