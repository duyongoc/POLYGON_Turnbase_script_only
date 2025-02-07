using UnityEngine;

public class HeroStateMove : ActorStateMove
{


    // [private]
    private bool _isMoving;
    private ActorHero _owner;
    private Timer timerBlockMove = new();
    // private Vector3 _position;


    #region UNITY
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        // if (_isMoving == false)
        //     return;

        // var distance = Vector3.Distance(_owner.transform.position, _position);
        // if (distance < _owner.GetAttackRange() - 1)
        // {
        //     _owner.SetStateIdle();
        //     _isMoving = false;
        // }


        timerBlockMove.UpdateTime(Time.deltaTime);
        if (timerBlockMove.IsDone())
        {
            _isMoving = false;
            timerBlockMove.Reset();
        }
    }
    #endregion



    public void Init(ActorHero actor)
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


        if (_isMoving)
        {
            return;
        }

        _owner.UpdateClosestEnemy();
        if (_owner.IsInAttackDetection())
        {
            _owner.LookAtZeroCurrentEnemy();
            if (_owner.IsInAttackRange())
            {
                _owner.SetNavMeshStop(true);
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
