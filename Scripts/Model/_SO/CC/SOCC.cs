using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Crowd Control", menuName = "CONFIG/CC Base", order = 20)]
public class SOCC : ScriptableObject
{

    [Header("[Setting]")]
    public CC type;
    public float duration = 1;
    public float bonusDamage = 0;

    [Header("[Addition]")]
    public List<SOCC> bonus;


    // public void OnBeforeSerialize() { }
    // public void OnAfterDeserialize() { }

}



public enum CC
{
    None = 0,

    // punch back in n seconds
    KnockBack = 10,

    // punch - down, stop all action in n seconds
    KnockDown = 20,

    // take x damage per seconds and play posion effect
    Airborne = 30,

    // reduce move speed and attack speed in n seconds
    Slow = 40,

    // stop all action in n seconds
    Stun = 50,

    // stop all action in n seconds
    Freeze = 60,

    // take x damage per seconds and play burn effect
    Burn = 70,




}
