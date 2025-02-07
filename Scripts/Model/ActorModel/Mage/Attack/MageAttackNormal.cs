using UnityEngine;

public class MageAttackNormal : MageAttackBase
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



    public void Init(SOSkillMage skill, ActorMage actor)
    {
        data = skill;
        owner = actor;
        Setup();
    }


    public void Attack()
    {
        var prefab = data.attack.prefabShoot.SpawnToGarbage(attackPoint.position, attackPoint.rotation);
        prefab.GetComponent<MageBulletNormal>().Init(owner, data.attack);
    }



}
