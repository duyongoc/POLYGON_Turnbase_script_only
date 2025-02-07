using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBase : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] protected bool isReady = false;
    [SerializeField] protected bool isDisabled = false;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Timer timerCountdown = new();


    // [private]
    protected ActorMelee owner;
    protected SOSkillMelee data;


    // [properties]
    // public bool IsReady { get => isReady; set => isReady = value; }
    // public bool IsDisable { get => isDisable; set => isDisable = value; }


    // function
    protected void UpdateCountdown()
    {
        if (isReady)
            return;

        timerCountdown.UpdateTime(Time.deltaTime);
        if (timerCountdown.IsDone())
        {
            isReady = true;
            timerCountdown.Reset();
        }
    }


    protected void Setup()
    {
        if (data == null)
        {
            isReady = false;
            isDisabled = true;
            return;
        }

        isReady = true;
        timerCountdown.SetDuration(data.countdownTime);
    }


    public bool CanPlayAttack()
    {
        // the skill is disabled
        if (isDisabled)
            return false;

        // the skill is not ready yet 
        if (!isReady)
            return false;

        return true;
    }




    protected void SpawnParticleHit(Vector3 position, Quaternion rotation)
    {
        if (data.attack.vfxHit != null)
            data.attack.vfxHit.SpawnToGarbage(position, rotation);
    }

    protected void SpawnParticleFlash(Vector3 position, Quaternion rotation)
    {
        if (data.attack.vfxFlash != null)
            data.attack.vfxFlash.SpawnToGarbage(position, rotation);
    }

    protected void SpawnParticleExplosion(Vector3 position, Quaternion rotation)
    {
        if (data.attack.vfxExplosion != null)
            data.attack.vfxExplosion.SpawnToGarbage(position, rotation);
    }



}
