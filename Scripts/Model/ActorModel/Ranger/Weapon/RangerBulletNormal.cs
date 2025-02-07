using UnityEngine;

public class RangerBulletNormal : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float destroyTime = 3;


    [Header("[DEBUG]")]
    [SerializeField] private DataAttackRanger attack;
    [SerializeField] private ActorRanger owner;


    // [private]
    private Timer _timerDestroy = new();




    #region UNITY
    private void Start()
    {
        _timerDestroy.SetDuration(destroyTime);
    }

    private void FixedUpdate()
    {
        _timerDestroy.UpdateTime(Time.deltaTime);
        if (_timerDestroy.IsDone())
        {
            SpawnParticleHit(transform.position);
            SelfDestroy();
        }

        transform.Translate(Vector3.forward * attack.moveSpeed * Time.deltaTime);
    }
    #endregion



    public void Init(ActorBase actor, DataAttackRanger data)
    {
        attack = data;
        owner = actor.GetComponent<ActorRanger>();
        // owner = actor.GetComponent<ActorRanger>();
        // owner.SFX.Play_RangerAttackNormalShot();
        LootAtEnemyDownOnGround();
    }


    private void LootAtEnemyDownOnGround()
    {
        transform.LookAt(owner.CurrentEnemy.ActorPointHit);
    }


    private void SpawnParticleHit(Vector3 position)
    {
        if (attack.vfxHit != null)
            attack.vfxHit.SpawnToGarbage(position, Quaternion.identity);
    }


    private void SelfDestroy()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        var victim = other.GetComponent<ActorBase>();
        if (victim == null || !owner.IsEnemy(victim))
            return;

        // add damage to the target
        owner.ApplyDamageMagic(victim, attack);
        SpawnParticleHit(victim.ActorPointHit.position);

        // destroy the bullet
        SelfDestroy();
    }


}
