using UnityEngine;

public class RangerAttackNormal : RangerAttackBase
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



    public void Init(SOSkillRanger skill, ActorRanger actor)
    {
        data = skill;
        owner = actor;
        Setup();
    }


    public void Attack()
    {
        var prefab = data.attack.prefabShoot.SpawnToGarbage(attackPoint.position, attackPoint.rotation);
        prefab.GetComponent<RangerBulletNormal>().Init(owner, data.attack);
    }



}
