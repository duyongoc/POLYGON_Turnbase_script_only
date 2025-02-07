using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{


    [Header("[Base]")]
    [SerializeField] protected UnitBaseComponent component;


    // [properties]
    public bool IsDead => component.IsDead;
    public string ActorKey => component.actorKey;
    public int PriorityFocus => component.PriorityFocus;
    public ERole Role => component.Role;
    public EActorType ActorType => component.ActorType;

    public UnitBase CurrentEnemy => component.CurrentEnemy;
    public UnitBase NearestEnemy => component.NearestEnemy;
    public List<UnitBase> Detected => component.Detected;

    public ActorHealth ActorHealth => component.ActorHealth;
    public DataActorStat CurrentStats => component.CurrentStats;
    public DataActorStat DefaultStats => component.DefaultStats;

    public Vector3 Position => transform.position;
    public Vector3 PointHitPosition => component.ActorPointHit.position;
    
    // public Transform PointHitTransform => component.ActorPointHit;
    public Transform ActorPointHit => component.ActorPointHit;




    public void IncreaseHP(float hp)
    {
        ActorHealth.IncreaseHP(hp);
    }

    public void DecreaseHP(float hp)
    {
        ActorHealth.DecreaseHP(hp);
    }


    protected void SetActorDead()
    {
        component.IsDead = true;
    }


    public void SetActorRole(ERole role)
    {
        component.Role = role;
    }


    public void SetActorKey(string key)
    {
        component.actorKey = key;
    }


    protected void SetActorCollider(bool value)
    {
        component.ActorCollider.enabled = value;
    }




    // use this function in-case use crown control bash skill
    public virtual void ApplyDamageMagic(UnitBase victim, DataAttackBase dataAttack)
    {
        // apply magic damage
        var damageMagic = Utils.GetMagicDamgeByPercent(CurrentStats, victim.CurrentStats, dataAttack.damagePercent);
        victim.TakeDamage(this, damageMagic, dataAttack.cc, dataAttack.playAnimHit);
    }


    // use this function in-case use crown control bash skill
    public virtual void ApplyDamagePhysic(UnitBase victim, DataAttackBase dataAttack)
    {
        // apply physic damage
        var damagePhysic = Utils.GetPhysicDamgeByPercent(CurrentStats, victim.CurrentStats, dataAttack.damagePercent);
        victim.TakeDamage(this, damagePhysic, dataAttack.cc, dataAttack.playAnimHit);
    }



    public virtual void TakeDamage(UnitBase source, float damage, SOCC cc, bool playAimHit)
    {
        // actor have already dead - do nothing
        if (IsDead)
            return;

        DecreaseHP(damage);
        UpdateHit(cc, playAimHit);
    }



    protected virtual void OnUpdateEnemy()
    {
        component.Enemies.Clear();
        foreach (UnitBase ene_detected in component.Detected)
        {
            if (ene_detected == null || ene_detected.IsDead)
                continue;

            component.Enemies.Add(ene_detected);
        }

        // check if dont have any enemies
        if (component.Enemies == null || component.Enemies.Count <= 0)
        {
            component.NearestEnemy = null;
        }

        // refresh current enemy
        if (component.Enemies.Count > 0)
        {
            component.NearestEnemy = component.Enemies[0];
            FindEnemiesByPriority();
            FindEnemiesByNestestTarget();
        }
    }


    protected void FindEnemiesByPriority()
    {
        // first check the priority target
        foreach (var enemy in component.Enemies)
        {
            // check the shorstet priority 
            if (component.NearestEnemy.PriorityFocus > enemy.PriorityFocus)
            {
                component.NearestEnemy = enemy;
            }
        }
    }


    protected void FindEnemiesByNestestTarget()
    {
        // find the nestest target
        foreach (var enemy in component.Enemies)
        {
            float dist1 = Vector3.Distance(transform.position, component.NearestEnemy.transform.position);
            float dist2 = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist1 > dist2)
            {
                component.NearestEnemy = enemy;
            }
        }
    }


    // ! its important to determine the enemy or not
    public bool IsEnemy(UnitBase victim)
    {
        if (component.Role != victim.Role)
            return true;

        return false;
    }



    public virtual void UpdateHit(SOCC cc, bool playAimHit = false) { }



}


[Serializable]
public class UnitBaseComponent
{
    public bool IsDead;
    public int TimeDestroy = 4;
    public int PriorityFocus = 100;
    public ERole Role;
    public EActorType ActorType;
    public string actorKey = "default";

    [Header("[Actor's Enemies]")]
    public List<UnitBase> Detected;
    public List<UnitBase> Enemies;
    public UnitBase DefaultTarget;
    public UnitBase NearestEnemy;
    public UnitBase CurrentEnemy;

    [Header("[Actor's Health]")]
    public DataActorStat CurrentStats;
    public DataActorStat DefaultStats;
    public ActorHealth ActorHealth;
    public Collider ActorCollider;
    public Transform ActorPointHit;

}



[Serializable]
public class ActorAnimationComponent
{
    public bool HasAnimStun;
    public bool HasAnimKnockdown;
}