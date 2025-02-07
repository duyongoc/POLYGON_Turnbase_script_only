using UnityEngine;

public class HeroStateAttack : ActorStateAttack
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


        if (_owner.IsAttacking || _owner.IsNegativeEffect())
            return;

        _owner.UpdateClosestEnemy();
        if (_owner.IsInAttackRange())
        {

            _owner.SetNavMeshStop(true);
            _owner.LookAtZeroCurrentEnemy();
            Attack();
        }
        else
        {
            _owner.SetStateMove();
        }
    }


    private void Attack()
    {
        _owner.AttackDelayTimer += Time.deltaTime;
        if (_owner.AttackDelayTimer >= _owner.AttackDelay)
        {
            _owner.Attack.DoAttack(_owner.CurrentEnemy);
            _owner.AttackDelayTimer = 0;
        }
    }


}
