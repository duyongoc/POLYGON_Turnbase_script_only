using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ActorBase : UnitBase
{


    [Header("[Animtion]")]
    [SerializeField] protected ActorAnimationComponent animComponent;

    [Header("[Actor]")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected ActorCC actorCC;
    [SerializeField] protected ActorEffect actorEffect;
    [SerializeField] protected ActorAbility actorAbility;


    // [private]
    protected bool isStun = false;
    protected bool isAttacking = false;
    protected bool isDownOnGround = false;
    protected float attackDelay = 0;
    protected float attackDelayTimer;


    // [animation]
    public const string ANIM_HIT = "Hit";
    public const string ANIM_RUN = "Run";
    public const string ANIM_DEAD = "Dead";
    public const string ANIM_MOVE = "MoveBlend";
    public const string ANIM_GET_UP = "Get_Up";
    public const string ANIM_WAKE_UP = "Wake_Up";

    public const string ANIM_EFFECT_STUN = "Effect_Stun";
    public const string ANIM_EFFECT_KNOCK_DOWN = "Effect_Knockdown";
    public const string ANIM_ATTACK_NORMAL = "Attack_Normal";
    public const string ANIM_ATTACK_SKILL_1 = "Attack_Skill_1";
    public const string ANIM_ATTACK_SKILL_2 = "Attack_Skill_2";
    public const string ANIM_ATTACK_SKILL_3 = "Attack_Skill_3";


    // [properties]
    public bool IsAttacking { get => isAttacking; }
    public bool IsStun { get => isStun; set => isStun = value; }
    public bool IsDownOnGround { get => isDownOnGround; set => isDownOnGround = value; }

    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float AttackDelayTimer { get => attackDelayTimer; set => attackDelayTimer = value; }


    public Animator Animator { get => animator; }
    public NavMeshAgent Agent { get => agent; }
    public ActorCC CC { get => actorCC; }
    public ActorEffect Effect { get => actorEffect; }
    public ActorAbility Ability { get => actorAbility; }



    // virtual function
    public virtual void SetStateHit() { }
    public virtual void SetStateIdle() { }
    public virtual void SetStateMove() { }
    public virtual void SetStateDead() { }
    public virtual void SetStateAttack() { }
    public virtual void SetPreviousState() { }
    public virtual void SetMove(Vector3 position) { }




    protected void SetInit()
    {
        actorCC.Init(this);
        actorEffect.Init(this);
        actorAbility.Init(this);
    }


    protected void SetBaseStats()
    {
        CurrentStats.level = DefaultStats.level;
        CurrentStats.hp = DefaultStats.hp;
        CurrentStats.p_attack = DefaultStats.p_attack;
        CurrentStats.m_attack = DefaultStats.m_attack;
        CurrentStats.p_defense = DefaultStats.p_defense;
        CurrentStats.m_defense = DefaultStats.m_defense;
        CurrentStats.attack_speed = DefaultStats.attack_speed;
        CurrentStats.speed = DefaultStats.speed;
    }


    protected void SetBaseUnityValue()
    {
        CurrentStats.speed = DefaultStats.speed;
        CurrentStats.attack_speed = DefaultStats.attack_speed;

        // convent character's move speed to navmesh value
        // agent.speed = Utils.ConvertMoveSpeedToNavMeshValue(CurrentStats.speed);

        // convent character's attack speed to time count down value
        // attackDelay = Utils.ConvertAttackSpeedToTimeValue(CurrentStats.attack_speed);
        attackDelayTimer = 0;
    }



    public void UpdateClosestEnemy()
    {
        // in case doesnt have any enemy - set default for it 
        if (NearestEnemy == null)
            component.CurrentEnemy = null;
        else
            component.CurrentEnemy = NearestEnemy;
    }



    public bool IsNegativeEffect()
    {
        if (IsDead || isStun || isDownOnGround)
            return true;

        return false;
    }


    public void OnStartAttack()
    {
        isAttacking = true;
    }

    public void OnFinishedAttack()
    {
        isAttacking = false;
    }

    public void ResetAttackDelay()
    {
        attackDelayTimer = 0;
    }

    public void ResetAttack()
    {
        isAttacking = false;
        attackDelayTimer = 0;
    }


    public void LookAtZeroTarget(Transform target)
    {
        var lookPos = target.position - transform.position;
        var lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        transform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
    }


    public void LookAtZeroCurrentEnemy()
    {
        if (CurrentEnemy == null)
            return;

        var lookPos = CurrentEnemy.transform.position - transform.position;
        var lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        transform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
    }


    public void SetNavMeshStop(bool value)
    {
        if (agent.enabled == false)
            return;

        agent.isStopped = value;
        if (agent.isStopped)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
    }


    public void SetNavMeshMoveToEnemy()
    {
        if (CurrentEnemy == null)
            return;

        SetNavMeshStop(false);
        agent.SetDestination(CurrentEnemy.transform.position);
    }


    public void SetNavMeshMoveToPosition(Vector3 position)
    {
        SetNavMeshStop(false);
        PlayAnimationMoveBlend();
        agent.SetDestination(position);
    }



    public void ResetAnimatorTrigger()
    {
        if (animator == null)
            return;

        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }


    protected void PlayAnimationMoveBlend()
    {
        if (animator == null)
            return;

        animator.Play("MoveBlend");
    }


    protected void PlayAnimationDead()
    {
        if (animator == null)
            return;

        animator.SetTrigger("Dead");
    }


    public void SetAnimationSpeed(float value)
    {
        animator.speed = value;
    }



    public void PlayAnimationStun()
    {
        if (animComponent.HasAnimStun)
        {
            animator.SetBool(ANIM_WAKE_UP, false);
            animator.SetTrigger(ANIM_EFFECT_STUN);
        }
        else
        {
            SetAnimationSpeed(0);
        }
    }

    public void StopAnimationStun()
    {
        if (animComponent.HasAnimStun)
        {
            animator.SetBool(ANIM_WAKE_UP, true);
        }
        else
        {
            SetAnimationSpeed(1);
        }
    }


    public void PlayAnimationKnockDown()
    {
        if (animComponent.HasAnimKnockdown)
        {
            animator.SetBool(ANIM_GET_UP, false);
            animator.SetTrigger(ANIM_EFFECT_KNOCK_DOWN);
        }
        else
        {
            SetAnimationSpeed(0);
        }
    }

    public void StopAnimationKnockDown()
    {
        if (animComponent.HasAnimKnockdown)
        {
            animator.SetBool(ANIM_GET_UP, true);
        }
        else
        {
            SetAnimationSpeed(1);
        }
    }



    protected IEnumerator IEAnimationFreezeSpeed(float duration)
    {
        var animSpeed = animator.speed;
        animator.speed = 0;
        yield return new WaitForSeconds(duration);
        animator.speed = animSpeed;
    }



    protected void SendActorDead()
    {
        if (Role.Equals(ERole.BOT))
        {
            MapController.Instance.RemoveEnemy(this);
        }
        if (Role.Equals(ERole.PLAYER))
        {
            MapController.Instance.RemovePlayer(this);
        }
    }


    public virtual void OnDead()
    {
        SetActorDead();
        SetActorCollider(false);
        SetNavMeshStop(true);
        ResetAnimatorTrigger();

        SendActorDead();
        PlayAnimationDead();
        DOTween.Kill(transform);
    }



}
