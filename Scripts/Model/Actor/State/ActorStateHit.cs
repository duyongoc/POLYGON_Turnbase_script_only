using UnityEngine;

public class ActorStateHit : ActorState
{


    // [Header("[Setting]")]
    [SerializeField] private float timeDelayHit = 0.35f;
    [SerializeField] private bool isAvoidKnockdown = false;
    [SerializeField] private bool isAvoidStun = false;
    [SerializeField] private bool isAvoidFreeze = false;
    // [SerializeField] private bool isAvoidEffectChaos = false;
    // [SerializeField] private bool isAvoidEffectAirborne = false;


    // [protected]
    protected SOCC ccData;
    protected ActorBase owner;
    protected Timer timerDelayHit = new();

    protected string ccType;
    protected bool delayHitDone = false;
    protected bool canPlayAnimHit = false;



    #region UNITY
    private void Start()
    {
        owner = GetComponentInParent<ActorBase>();
        timerDelayHit.SetDuration(timeDelayHit);
    }

    private void FixedUpdate()
    {
        OnUpdateState();
    }
    #endregion



    #region STATE
    public override void StartState() { }
    public override void UpdateState() { }
    public override void EndState() { }
    #endregion




    private void OnUpdateState()
    {
        // skip when we can play the anim 
        if (delayHitDone)
            return;

        timerDelayHit.UpdateTime(Time.deltaTime);
        if (timerDelayHit.IsDone())
        {
            delayHitDone = true;
            timerDelayHit.Reset();
        }
    }



    public void UpdateHit(SOCC cc, bool playAnimHit = false)
    {
        ccData = cc;
        canPlayAnimHit = playAnimHit;
        if (ccData == null)
        {
            HandleHit();
            return;
        }

        // incase the character already dead
        if (owner.IsDead)
        {
            owner.OnDead();
            return;
        }

        // print($"UpdateEffectSide: {owner.name} | effectname: " + effect.type);
        // owner.SetNavmeshStop();
        UpdateEffect();
    }


    protected void UpdateEffect()
    {
        ccType = ccData.type.ToString();
        switch (ccData.type)
        {
            case CC.KnockBack:
                OnEffect_KnockBack(); break;

            case CC.KnockDown:
                OnEffect_KnockDown(); break;

            case CC.Stun:
                OnEffect_Stun(); break;

            case CC.Freeze:
                OnEffect_Freeze(); break;

            // case CC.Slow:
            //     OnEffect_Slow(); break;

            // case CC.Burn:
            //     OnEffect_Burn(); break;

            default:
                HandleHit(); break;
        }
    }



    public virtual void OnEffect_KnockBack()
    {
        if (isAvoidKnockdown)
        {
            HandleHit();
            return;
        }
        owner.CC.KnockBack(ccData, CheckStateAfterEffect);
    }


    public virtual void OnEffect_KnockDown()
    {
        if (isAvoidKnockdown)
        {
            HandleHit();
            return;
        }
        owner.CC.KnockDown(ccData, CheckStateAfterEffect);
    }


    public virtual void OnEffect_Stun()
    {
        if (isAvoidStun)
        {
            HandleHit();
            return;
        }
        owner.CC.Stun(ccData, CheckStateAfterEffect);
    }


    public virtual void OnEffect_Freeze()
    {
        if (isAvoidFreeze)
        {
            HandleHit();
            return;
        }
        owner.CC.Freeze(ccData, CheckStateAfterEffect);
    }




    // public virtual void OnEffect_Sleep()
    // {
    //     if (isAvoidEffectSleep)
    //     {
    //         HandleHit();
    //         return;
    //     }
    //     owner.CC.Sleep(ccData, CheckStateAfterEffect);
    // }


    // public virtual void OnEffect_Chaos()
    // {
    //     if (isAvoidEffectChaos)
    //     {
    //         HandleHit();
    //         return;
    //     }
    //     owner.CC.Chaos(ccData, CheckStateAfterEffect);
    // }


    // public virtual void OnEffect_Lying()
    // {
    //     owner.CC.Lying(ccData, CheckStateAfterEffect);
    // }


    // public virtual void OnEffect_StandStill()
    // {
    //     owner.CC.Stand(ccData);
    //     HandleHit();
    // }


    // public virtual void OnEffect_Slow()
    // {
    //     owner.CC.Slow(ccData);
    //     HandleHit();
    // }


    // public virtual void OnEffect_Burn()
    // {
    //     owner.CC.Burn(ccData);
    //     HandleHit();
    // }


    // public virtual void OnEffect_Poison()
    // {
    //     owner.CC.Poison(ccData);
    //     HandleHit();
    // }



    private void HandleHit()
    {
        if (owner.IsDead)
            return;

        PlayAnimationHit();
        owner.SetPreviousState();
    }


    private void CheckStateAfterEffect()
    {
        // print("CheckStateAfterEffect: " + owner.IsDead);
        if (owner.IsDead)
            return;

        owner.SetStateAttack();
    }


    private void PlayAnimationHit()
    {
        // print($"PlayAnimationHit canPlayAnimHit: {canPlayAnimHit} - delayHitDone: {delayHitDone}  IsCastingSkill - {owner.IsCastingSkill} -" + owner.IsSleeping + " " + owner.IsStunned);
        if (!canPlayAnimHit)
            return;

        if (!delayHitDone)
            return;

        // if (owner.IsAvoidHitAnimation)
        //     return;

        // if (owner.IsSleeping || owner.IsStun)
        //     return;

        // owner.PlayAnimationHit();
        delayHitDone = false;
    }


    public void SetAvoidAllEffect(bool value)
    {
        isAvoidKnockdown = value;
        isAvoidFreeze = value;
        isAvoidStun = value;
        // isAvoidEffectChaos = value;
        // isAvoidEffectAirborne = value;
    }



}
