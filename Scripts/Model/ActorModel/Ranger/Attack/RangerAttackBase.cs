using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttackBase : MonoBehaviour
{
    
    
    [Header("[Setting]")]
    [SerializeField] protected bool isReady = false;
    [SerializeField] protected bool isDisabled = false;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Timer timerCountdown = new();


    // [private]
    protected ActorRanger owner;
    protected SOSkillRanger data;


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



}
