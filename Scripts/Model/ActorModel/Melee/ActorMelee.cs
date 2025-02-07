using UnityEngine;

public class ActorMelee : ActorBase
{


    [Header("[Config]")]
    [SerializeField] private SOActorMelee CONFIG;
    [SerializeField] private ActorMeleeSFX actorSFX;
    [SerializeField] private ActorMeleeVFX actorVFX;
    [SerializeField] private ActorMeleeAttack actorAttack;

    [Header("[State Current]")]
    [SerializeField] private ActorState currentState;
    [SerializeField] private ActorState previousState;

    [Header("[State Component]")]
    [SerializeField] private MeleeStateHit stateHit;
    [SerializeField] private MeleeStateIdle stateIdle;
    [SerializeField] private MeleeStateMove stateMove;
    [SerializeField] private MeleeStateDead stateDead;
    [SerializeField] private MeleeStateAttack stateAttack;


    // [private]
    private float _attackRange;
    private float _attackDetection;
    private MapController _mapController;


    // [properties]
    public ActorMeleeSFX SFX { get => actorSFX; }
    public ActorMeleeVFX VFX { get => actorVFX; }
    public ActorMeleeAttack Attack { get => actorAttack; }



    #region UNITY
    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (IsDead || _mapController.IsGameFinished)
            return;

        OnUpdateEnemy();
        currentState.UpdateState();
    }
    #endregion



    public void Init()
    {
        _mapController = MapController.Instance;

        SetInit();
        SetStats();
        SetBaseStats();
        SetBaseUnityValue();

        // SetInitActor();
        // SetInitTarget();
        // SetNavAgentEnable();
        SetStateIdle();

        stateIdle.Init(this);
        stateMove.Init(this);
        stateAttack.Init(this);

        actorAttack.Init(CONFIG, this);
        ActorHealth.Init(CONFIG.defaultStats.hp);
    }


    private void SetStats()
    {
        component.DefaultStats = CONFIG.defaultStats;
        component.PriorityFocus = CONFIG.priorityFocus;

        _attackRange = CONFIG.attackRange;
        _attackDetection = CONFIG.attackDetection;
    }


    public void SetState(ActorState newState)
    {
        if (currentState != null)
            currentState.EndState();

        previousState = currentState;
        currentState = newState;
        currentState.StartState();
    }



    public override void SetStateHit()
    {
        SetState(stateHit);
    }

    public override void SetStateIdle()
    {
        SetState(stateIdle);
    }

    public override void SetStateMove()
    {
        SetState(stateMove);
    }

    public override void SetStateDead()
    {
        SetState(stateDead);
    }

    public override void SetStateAttack()
    {
        SetState(stateAttack);
    }

    public override void SetPreviousState()
    {
        SetState(previousState);
    }



    public override void SetMove(Vector3 position)
    {
        ResetAttack();
        SetStateMove();
        SetNavMeshMoveToPosition(position);
        stateMove.SetMovePosition(position);
    }


    public bool IsInAttackRange()
    {
        if (CurrentEnemy == null)
            return false;

        var distance = Vector3.Distance(CurrentEnemy.transform.position, transform.position);
        if (distance <= _attackRange)
            return true;

        return false;
    }


    public bool IsInAttackDetection()
    {
        if (CurrentEnemy == null)
            return false;

        var distance = Vector3.Distance(CurrentEnemy.transform.position, transform.position);
        if (distance <= _attackDetection)
            return true;

        return false;
    }


    public override void UpdateHit(SOCC cc, bool playAimHit = false)
    {
        SetState(stateHit);
        stateHit.UpdateHit(cc, playAimHit);
    }


    public override void OnDead()
    {
        base.OnDead();
        SetState(stateDead);
        // actorSFX.Play_MonsterDead();
        // actorVFX.Play_ParticleDeath();

        // MissionController.Instance.RemoveEnemy(this);
        // Utils.Delay(timeDestroyActor, SelfDestroy);
    }


}
