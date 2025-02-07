using UnityEngine;

public class BulletBase : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] protected float destroyTime = 3f;
    [SerializeField] protected Collider mCollider;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected DataAttackRanger attack;


    // [protected]
    protected bool dontSpawnHitWhenDestroy = false;
    protected Timer timerDestroy = new();
    protected ActorBase owner;
    protected UnitBase enemy;


    // [private]
    private float _count = 0f;
    private bool _rangerBullet;
    private Vector3 _vecOrigin;
    private Vector3 _vecPosition1;


    #region UNITY
    private void Start()
    {
        timerDestroy.SetDuration(destroyTime);
    }

    private void FixedUpdate()
    {
        UpdateTimeDestroy();
        UpdateMovement();
    }
    #endregion





    public virtual void Init(ActorBase actor, DataAttackRanger data) { }
    public virtual void UpdateMovement() { }




    private void UpdateTimeDestroy()
    {
        timerDestroy.UpdateTime(Time.deltaTime);
        if (timerDestroy.IsDone())
        {
            // dont spawn effect - this is important
            if (!dontSpawnHitWhenDestroy)
            {
                SpawnVFXHit(transform.position);
            }

            // destroy gameojbect after time
            SelfDestroy();
        }
    }


    protected void SpawnVFXHit(Vector3 position)
    {
        attack.SpawnVFXHit(position);
    }
    protected void SpawnVFXHit(Vector3 position, Quaternion rotation)
    {
        attack.SpawnVFXHit(position,rotation);
    }


    protected void SpawnVFXFlash(Vector3 position)
    {
        attack.SpawnVFXFlash(position);
    }
    protected void SpawnVFXFlash(Vector3 position, Quaternion rotation)
    {
        attack.SpawnVFXFlash(position, rotation);
    }


    protected void SpawnVFXExplosion(Vector3 position)
    {
        attack.SpawnVFXExplosion(position);
    }
    protected void SpawnVFXExplosion(Vector3 position, Quaternion rotation)
    {
        attack.SpawnVFXExplosion(position, rotation);
    }



    protected void LookAtEnemy()
    {
        transform.LookAt(enemy.PointHitPosition);
    }


    protected void SelfDestroy()
    {
        Destroy(gameObject);
    }


    protected void CheckDestroy()
    {
        // the bullet will pass through the enemies
        if (attack.passThrough)
            return;

        SelfDestroy();
    }


    protected void MoveTowardsTheEnemy()
    {
        if (enemy == null)
            return;

        var target = enemy.PointHitPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, attack.moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }


    protected void MoveTowardsTheTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, attack.moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }


    protected void MoveForwardTheEnemy()
    {
        transform.Translate(Vector3.forward * attack.moveSpeed * Time.deltaTime);
    }



    protected void Seek()
    {
        Vector3 direction = enemy.PointHitPosition - transform.position;
        Vector3 moveVector = direction.normalized * 8 * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 1 * Time.deltaTime);
        transform.position += moveVector;
    }


    protected void PrepareBezierToTarget()
    {
        _count = 0.0f;
        _vecOrigin = transform.position;
        _vecPosition1 = _vecOrigin + (transform.right.normalized * Random.Range(-2.5f, 2.5f));
        // vecPosition2 = enemy.PointHitAttack.position;
    }


    protected void MoveBezierToTarget()
    {
        // this function will make the bullet move by bezier path
        // to make this function work, we must call PrepareBezierToTarget first
        if (_count > 1)
            return;

        _count += 1.0f * Time.deltaTime;
        Vector3 m1 = Vector3.Lerp(_vecOrigin, _vecPosition1, _count);
        Vector3 m2 = Vector3.Lerp(_vecPosition1, enemy.PointHitPosition, _count);
        transform.position = Vector3.Lerp(m1, m2, _count);
        transform.LookAt(enemy.PointHitPosition);
    }



}
