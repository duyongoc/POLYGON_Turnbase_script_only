using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundy : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    // [properties]
    public AudioSource GetAudioSource { get => audioSource; set => audioSource = value; }



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public void SetSoundVolume(float value)
    {
        audioSource.volume = value;
    }


    public void SetSoundMute(bool enable)
    {
        audioSource.enabled = !enable;
        if (audioSource.enabled)
            audioSource.Play();
    }


    public void Play(AudioClip audio)
    {
        gameObject.name = audio.name;
        audioSource.clip = audio;

        if (audioSource.enabled)
            audioSource.Play();
        SelfDestroy();
    }


    public void PlayLoop(AudioClip audio)
    {
        gameObject.name = audio.name;
        audioSource.clip = audio;
        audioSource.loop = true;

        if (audioSource.enabled)
            audioSource.Play();
    }


    private void SelfDestroy()
    {
        var time = audioSource.clip.length * 2;
        Destroy(gameObject, time);
    }

}
