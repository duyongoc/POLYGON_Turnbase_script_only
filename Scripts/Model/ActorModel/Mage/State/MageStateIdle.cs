using UnityEngine;

public class MageStateIdle : ActorStateIdle
{
    

    // [private]
    private ActorMage _owner;


    public void Init(ActorMage actor)
    {
        _owner = actor;
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
    }

}
