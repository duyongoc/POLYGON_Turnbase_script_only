using UnityEngine;

public class MageStateMove : ActorStateMove
{


    // [private]
    private bool _isMoving;
    private Vector3 _position;
    private ActorMage _owner;


    #region UNITY
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        // if (_isMoving == false)
        //     return;

        // var distance = Vector3.Distance(_owner.transform.position, _position);
        // if (distance < 0.5f)
        // {
        //     _owner.SetStateIdle();
        //     _isMoving = false;
        // }
    }
    #endregion



    public void Init(ActorMage actor)
    {
        _owner = actor;
    }

    public void SetMovePosition(Vector3 pos)
    {
        _isMoving = true;
        _position = pos;

        Utils.Delay(2, () => { _isMoving = false; });
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
