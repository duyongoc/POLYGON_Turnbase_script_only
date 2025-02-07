using UnityEngine;

public class MeleeAttackSkill_3 : MeleeAttackBase
{



    #region UNITY
    // private void Start()
    // {
    // }

    private void FixedUpdate()
    {
        UpdateCountdown();
    }
    #endregion



    public void Init(SOSkillMelee skill, ActorMelee actor)
    {
        data = skill;
        owner = actor;
        Setup();
    }


    public void Attack()
    {
        var hitColliders = Physics.OverlapSphere(attackPoint.position, data.attack.damageRange);
        foreach (var hit in hitColliders)
        {
            var victim = hit.GetComponent<ActorBase>();
            if (victim == null || !owner.IsEnemy(victim))
                continue;

            owner.ApplyDamagePhysic(victim, data.attack);
            SpawnParticleHit(victim.PointHitPosition, Quaternion.identity);
        }

        isReady = false;
    }


}
