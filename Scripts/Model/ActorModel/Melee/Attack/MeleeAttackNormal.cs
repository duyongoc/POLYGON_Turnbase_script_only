using UnityEngine;
using UnityEngine.AI;

public class MeleeAttackNormal : MeleeAttackBase
{




    #region UNITY
    // private void Start()
    // {
    // }

    private void FixedUpdate()
    {
        // UpdateCountdown();
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
        // DebugExtension.DebugWireSphere(attackPoint.position, Color.red, data.attack.damageRange, 1);
        var hitColliders = Physics.OverlapSphere(attackPoint.position, data.attack.damageRange);
        foreach (var hit in hitColliders)
        {
            var victim = hit.GetComponent<ActorBase>();
            if (victim == null || !owner.IsEnemy(victim))
                continue;

            owner.ApplyDamagePhysic(victim, data.attack);
            SpawnParticleHit(victim.PointHitPosition, Quaternion.identity);
        }
    }


}
