using UnityEngine;

public class HeroAttackNormal : HeroAttackBase
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




    public void Init(SOSkillHero skill, ActorHero actor)
    {
        data = skill;
        owner = actor;
        Setup();
    }


    public void Attack()
    {
        owner.Ability.DoTrigger(owner.CurrentEnemy, data.attack, attackPoint);
        isReady = false;
    }


}
