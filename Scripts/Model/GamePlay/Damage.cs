using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingRigidbody))]
public class Damage : BaseDamage
{


    public enum SpawnMode
    {
        SpawnAtAttacker,
        SpawnAtTarget,
    }

    public enum HitSpawnMode
    {
        HitAtBody,
        HitAtFloor,
    }


    [Header("Effects")]
    public CharacterEffectData hitEffects;
    public AudioClip soundHit;

    [Header("Range Attack")]
    public float missileSpeed;

    [Header("Spawn")]
    public SpawnMode spawnMode = SpawnMode.SpawnAtAttacker;
    public HitSpawnMode hitSpawnMode = HitSpawnMode.HitAtBody;
    public float spawnOffsetY = 0;


    // [private]
    private GameObject vfxEffect;
    private GameObject vfxEffectInject;
    private CharacterEntity attacker;
    private CharacterEntity target;
    private TargetingRigidbody tempTargetingRigidbody;
    private float pAtkRate;
    private float mAtkRate;
    private int hitCount;
    private int fixDamage;



    public TargetingRigidbody TempTargetingRigidbody
    {
        get
        {
            if (tempTargetingRigidbody == null)
                tempTargetingRigidbody = GetComponent<TargetingRigidbody>();

            return tempTargetingRigidbody;
        }
    }


    private void FixedUpdate()
    {
        if (target == null)
            return;

        if (!TempTargetingRigidbody.IsMoving)
        {
            if (vfxEffect != null)
            {
                Instantiate(vfxEffect, target.bodyEffectContainer.position, attacker.transform.rotation);
            }

            if (vfxEffectInject != null)
            {
                Instantiate(vfxEffectInject, target.bodyEffectContainer.position, attacker.transform.rotation, attacker.transform);
            }

            SoundManager.Instance.PlaySFX(soundHit);
            attacker.Attack(target, pAtkRate, mAtkRate, hitCount, fixDamage);
            attacker.Damages.Remove(this);

            target = null;
            Destroy(gameObject);
            return;
        }
    }


    public void Setup(CharacterEntity attacker, CharacterEntity target, float pAtkRate = 1f, float mAtkRate = 1f, int hitCount = 1, int fixDamage = 0)
    {
        this.attacker = attacker;
        this.target = target;
        this.pAtkRate = pAtkRate;
        this.mAtkRate = mAtkRate;
        this.hitCount = hitCount;
        this.fixDamage = fixDamage;

        var targetPosition = Vector3.zero;
        switch (hitSpawnMode)
        {
            case HitSpawnMode.HitAtBody:
                targetPosition = target.bodyEffectContainer.position;
                break;

            case HitSpawnMode.HitAtFloor:
                targetPosition = target.floorEffectContainer.position;
                break;
        }

        if (missileSpeed == 0)
        {
            TempTransform.position = targetPosition;
        }
        else
        {
            switch (spawnMode)
            {
                case SpawnMode.SpawnAtAttacker:
                    TempTargetingRigidbody.StartMove(targetPosition + (Vector3.up * spawnOffsetY), missileSpeed); break;

                case SpawnMode.SpawnAtTarget:
                    TempTargetingRigidbody.StartMove(targetPosition + (Vector3.up * spawnOffsetY), missileSpeed); break;
            }
        }

        this.attacker.Damages.Add(this);
    }



    public void SpawnEffect(GameObject projectile, GameObject vfx, AudioClip clip)
    {
        soundHit = clip;
        SpawnVFX_Effect(vfx);

        if (projectile != null)
        {
            var atker = attacker == null ? attacker.bodyEffectContainer : transform;
            var effect = Instantiate(projectile, atker.position, atker.rotation, transform);
            effect.transform.localPosition = Vector3.zero;
            effect.transform.LookAt(atker);
        }
    }


    public void SpawnVFX_Effect(GameObject vfx)
    {
        vfxEffect = vfx;
    }

    public void SpawnVFX_EffectInject(GameObject vfx)
    {
        vfxEffectInject = vfx;
    }


}
