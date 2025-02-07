using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{


    [Header("[Local Setting]")]
    [SerializeField] private LocalSetting localSetting;


    // [Header("[Setting]")]
    // [SerializeField] private List<SOConfigMission> settingMissions;
    // [SerializeField] private SOConfigMission currentMission;


    // [properties]
    public LocalSetting LocalSetting => localSetting;



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public void Init()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
        WebGLInput.captureAllKeyboardInput = false;
#endif

        if (!PlayerPrefs.HasKey(CONST.KEY_SAVE_LOCAL))
        {
            localSetting = new LocalSetting();
            SaveSetting();
            return;
        }

        // load previous setting
        LoadSetting();
    }


    public void UpdateVolumeMusic(float value)
    {
        localSetting.musicVolume = value;
        SoundManager.Instance.UpdateVolumeMusic(value);
        SaveSetting();
    }


    public void UpdateVolumeSFX(float value)
    {
        localSetting.soundVolume = value;
        SoundManager.Instance.UpdateVolumeSFX(value);
        SaveSetting();
    }


    // public SOConfigMission GetCurrentMission()
    // {
    //     return currentMission;
    // }


    // public void SetCurrentMission(string missionId)
    // {
    //     var mission = settingMissions.Find(x => x.missionId.Equals(missionId));
    //     if (mission == null)
    //     {
    //         print($"SetCurrentMission is null with missionId: {missionId}");
    //         return;
    //     }
    //     currentMission = mission;
    // }


    public void LoadSetting()
    {
        var setting = PlayerPrefs.GetString(CONST.KEY_SAVE_LOCAL);
        localSetting = JsonUtility.FromJson<LocalSetting>(setting);
        // print("LoadSetting " + setting);
    }

    public void SaveSetting()
    {
        string jsonValue = JsonUtility.ToJson(localSetting);
        PlayerPrefs.SetString(CONST.KEY_SAVE_LOCAL, jsonValue);
        // print("SaveSetting " + jsonValue);
    }



}


[System.Serializable]
public class LocalSetting
{
    public int graphic;
    public float soundVolume;
    public float musicVolume;
    public bool enableFX;

    public LocalSetting()
    {
        graphic = 1;
        soundVolume = 1;
        musicVolume = 1;
        enableFX = false;
    }
}
