using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class BaseAttackAnimationData : AnimationData
{

    [Header("[Hit]")]
    public float hitDuration = 0;

    [Range(0f, 1f)]
    public float hitDurationRate;

    [Header("[Damage]")]
    public BaseDamage damage;

    [Header("[Damage]")]
    public AudioClip soundAttack;
    public AudioClip soundHit;

    [Header("[Ranger]")]
    public bool isRangeAttack;
    public GameObject hitEffect;
    public GameObject hitEffectInject;
    public GameObject hitProjectile;



    public void PlaySoundAttack()
    {
        SoundManager.Instance.PlaySFX(soundAttack);
    }


    [ContextMenu("Set Hit Duration Rate")]
    public void SetHitDurationRate()
    {
        if (hitDuration > 0 && AnimationDuration > 0)
            hitDurationRate = hitDuration / AnimationDuration;

        if (hitDurationRate > 1)
            hitDurationRate = 1;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }


}


public static class BaseAttackAnimationDataExtension
{
    public static float GetHitDuration(this BaseAttackAnimationData attackAnimation)
    {
        // Debug.Log($"hitDurationRate: {attackAnimation.hitDurationRate} - AnimationDuration: {attackAnimation.AnimationDuration}");
        return attackAnimation == null ? 0f : attackAnimation.hitDurationRate * attackAnimation.AnimationDuration;
    }

    public static BaseDamage GetDamage(this BaseAttackAnimationData attackAnimation)
    {
        return attackAnimation == null ? null : attackAnimation.damage;
    }
}