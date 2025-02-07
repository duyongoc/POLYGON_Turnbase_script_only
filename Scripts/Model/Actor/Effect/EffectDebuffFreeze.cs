using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDebuffFreeze : MonoBehaviour
{
    

    [Header("[Turn]")]
    [SerializeField] private bool isApplied = false;
    [SerializeField] private GameObject particleEffect;


    // [private]
    private ActorBase owner;
    private Timer timerEffect = new();
    private System.Action cbFinished;




    #region UNITY
    // private void Start()
    // {
    // }

    private void FixedUpdate()
    {
        if (!isApplied)
            return;


        timerEffect.UpdateTime(Time.deltaTime);
        if (timerEffect.IsDone())
        {
            cbFinished?.Invoke();
            EndEffect();
        }
    }
    #endregion



    public void Init()
    {
        isApplied = false;
    }


    public void Apply(float duration, float bonusDamage, System.Action callbackFinish)
    {
        StartEffect();
        timerEffect.SetDuration(duration);
        cbFinished = callbackFinish;
    }


    private void StartEffect()
    {
        isApplied = true;
        particleEffect.SetActive(true);
        particleEffect.GetComponent<ParticleSystem>().Clear();
    }


    private void EndEffect()
    {
        particleEffect.SetActive(false);
        isApplied = false;
    }


}
