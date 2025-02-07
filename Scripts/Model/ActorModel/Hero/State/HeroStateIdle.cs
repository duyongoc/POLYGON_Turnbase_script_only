using UnityEngine;

public class HeroStateIdle : ActorStateIdle
{


    // [private]
    private ActorHero _owner;



    public void Init(ActorHero actor)
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
    }



}
