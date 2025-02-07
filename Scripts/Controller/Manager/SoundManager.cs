using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{


    [Header("Setting")]
    [SerializeField] private bool isMute = false;
    [SerializeField] private Soundy soundyPrefab;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioSource audioSFX;


    [Header("Music Clip")]
    public AudioClip MUSIC_MENU;
    public AudioClip MUSIC_BATTLE;


    [Header("SFX Clip")]
    public AudioClip SFX_BattleWin;
    public AudioClip SFX_BattleDefeat;


    // [private]
    private Soundy currentBGMusic;



    #region UNITY
    public override void Awake()
    {
        base.Awake();
        LoadInit();
    }

    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public void LoadInit()
    {
        LoadConfig();
        PlaySoundMenu();

        // var setting = GameManager.Instance.SettingManager;
        // UpdateVolumeMusic(setting.LocalSetting.musicVolume);
        // UpdateVolumeSFX(setting.LocalSetting.soundVolume);
        // UpdateEnableSFX(setting.LocalSetting.enableFX);
    }

    public void LoadConfig()
    {
        // LoadSoundMusic();
        // LoadSoundSFX();
    }



    public void PlaySoundMenu()
    {
        PlayMusic(MUSIC_MENU);
    }


    public void PlaySoundBattle()
    {
        PlayMusic(MUSIC_BATTLE);
    }



    public void UpdateVolumeMusic(float value)
    {
        audioMusic.volume = value;
        currentBGMusic?.SetSoundVolume(value);
    }

    public void UpdateVolumeSFX(float value)
    {
        audioSFX.volume = value;
    }

    public void UpdateEnableSFX(bool value)
    {
    }


    public void TurnSound(string value)
    {
        isMute = value.Contains("off") ? true : false;
        if (currentBGMusic == null)
            return;

        currentBGMusic.SetSoundMute(isMute);
    }



    // <summary> </summary>
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (currentBGMusic != null)
        {
            Destroy(currentBGMusic.gameObject);
        }

        currentBGMusic = Instantiate(soundyPrefab, audioMusic.transform);
        currentBGMusic.SetSoundVolume(audioMusic.volume);
        currentBGMusic.SetSoundMute(isMute);
        currentBGMusic.PlayLoop(clip);
    }


    // <summary> </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        var sound = Instantiate(soundyPrefab, audioSFX.transform);
        sound.SetSoundVolume(audioSFX.volume);
        sound.SetSoundMute(isMute);
        sound.Play(clip);
    }


    // <summary> </summary>
    public void PlaySFXOneShot(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSFX.PlayOneShot(clip);
    }


    // <summary> </summary>
    public void StopMusic()
    {
        currentBGMusic.SetSoundMute(false);
    }


    // <summary> </summary>
    public void StopSFX()
    {
        audioSFX.Stop();
    }


    // <summary> </summary>
    public void StopSFX(AudioClip clip)
    {
        audioSFX.Stop();
    }


    // <summary> </summary>
    public void PlaySFXBlend(AudioClip clip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip);
    }


    // <summary> </summary>
    public bool MusicPlaying(AudioClip audi)
    {
        return audioMusic.clip == audi && audioMusic.isPlaying;
    }


    // <summary> </summary>
    public bool SFXPlaying(AudioClip audi)
    {
        return audioSFX.clip == audi && audioSFX.isPlaying;
    }



    // public async UniTask<AudioClip> GetAudioClip(string filePath, AudioType fileType)
    // {
    //     using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(filePath, fileType))
    //     {
    //         var result = www.SendWebRequest();
    //         while (!result.isDone) { await UniTask.Delay(1000); }
    //         if (www.result == UnityWebRequest.Result.ConnectionError)
    //         {
    //             Debug.Log(www.error);
    //             return null;
    //         }
    //         else
    //         {
    //             return DownloadHandlerAudioClip.GetContent(www);
    //         }
    //     }
    // }


}
