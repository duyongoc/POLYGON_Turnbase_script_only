using UnityEngine;

public class ActorHeroVFX : MonoBehaviour
{


    [Header("[Particle Aura]")]
    [SerializeField] private ActorVFXPlay vfxWeapon;



    public void Play_Weapon(float duration, System.Action callbackFinish = null)
    {
        vfxWeapon?.Play(duration, callbackFinish);
    }


}
