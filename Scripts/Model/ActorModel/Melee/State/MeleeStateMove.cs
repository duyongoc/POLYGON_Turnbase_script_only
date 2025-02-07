using UnityEngine;

public class MeleeStateMove : ActorStateMove
{


    // [private]
    private bool _isMoving;
    private ActorMelee _owner;
    private Timer timerBlockMove = new();



    #region UNITY
    // private void Start()
    // {
    // }

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        timerBlockMove.UpdateTime(Time.deltaTime);
        if (timerBlockMove.IsDone())
        {
            _isMoving = false;
            timerBlockMove.Reset();
        }
    }
    #endregion



    public void Init(ActorMelee actor)
    {
        _owner = actor;
    }

    public void SetMovePosition(Vector3 pos)
    {
        _isMoving = true;
        timerBlockMove.SetDuration(2);
    }


    public override void StartState()
    {
    }

    public override void EndState()
    {
    }


    public override void UpdateState()
    {
        base.UpdateState();


        if (_isMoving || _owner.IsAttacking)
        {
            return;
        }

        _owner.UpdateClosestEnemy();
        if (_owner.IsInAttackDetection())
        {
            if (_owner.IsInAttackRange())
            {
                _owner.SetStateAttack();
            }
            else
            {
                _owner.SetNavMeshMoveToEnemy();
            }
        }
        else
        {
            _owner.SetStateIdle();
        }
    }


}
