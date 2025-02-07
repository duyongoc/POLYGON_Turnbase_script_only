using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVFXPlay : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private bool isApplied = false;
    [SerializeField] private GameObject particleEffect;


    // [private]
    private Action cbFinished;
    private Timer timerEffect = new();



    #region UNITY
    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        UpdateEffect();
    }
    #endregion



    public void Init()
    {
        isApplied = false;
        StopParticle();
    }


    private void UpdateEffect()
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


    public void Play(float duration, Action callbackFinish)
    {
        StartEffect();
        timerEffect.SetDuration(duration);
        cbFinished = callbackFinish;
    }


    public void PlayVFX_On()
    {
        PlayParticle();
    }


    public void PlayVFX_Off()
    {
        StopParticle();
    }


    public void ActiveEffect(bool value)
    {
        particleEffect.SetActive(value);
    }


    private void StartEffect()
    {
        isApplied = true;
        PlayParticle();
    }


    private void EndEffect()
    {
        isApplied = false;
        StopParticle();
    }



    private void PlayParticle()
    {
        particleEffect.SetActive(true);

        var parParent = particleEffect.GetComponent<ParticleSystem>();
        if (parParent)
            parParent.Play();

        foreach (Transform tran in particleEffect.transform)
        {
            var particle = tran.GetComponent<ParticleSystem>();
            if (particle != null)
                particle.Play();
        }
    }


    private void StopParticle()
    {
        particleEffect.SetActive(false);

        var parParent = particleEffect.GetComponent<ParticleSystem>();
        if (parParent)
            parParent.Stop(true);

        foreach (Transform tran in particleEffect.transform)
        {
            var particle = tran.GetComponent<ParticleSystem>();
            if (particle != null)
                particle.Stop(true);
        }
    }


}
