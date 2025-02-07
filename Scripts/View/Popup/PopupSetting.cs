using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class PopupSetting : PopupBase
{


    [Space(20)]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private TMP_Text txtSliderMusic;
    [SerializeField] private float musicOffset = 0.1f;

    [Header("[Setting]")]
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtGraphic;
    [SerializeField] private Button btnGraphicReduce;
    [SerializeField] private Button btnGraphicIncrease;


    // [private]
    private int levelQuality;
    private GameGraphic graphic;
    private GameSetting setting;




    public void Showing(Action cbOK = null, Action cbCancel = null)
    {
        Show(cbOK, cbCancel);
        Load();
    }


    public void Load()
    {
        graphic = GameManager.Instance.Graphic;
        setting = GameManager.Instance.Setting;

        levelQuality = graphic.GetGraphicLevel();
        sliderMusic.value = setting.LocalSetting.musicVolume;
        txtSliderMusic.text = $"{(int)(sliderMusic.value * 10f)}";
    }


    /// <summary> This function called as event in: Btn_Increase_Music </summary>
    public void IncreaseMusicVolume()
    {
        sliderMusic.value += musicOffset;
        setting.SaveSetting();
    }

    /// <summary> This function called as event in: Btn_Decrease_Music </summary>
    public void DecreaseMusicVolume()
    {
        sliderMusic.value -= musicOffset;
        setting.SaveSetting();
    }


    public void UpdateMusicVolume()
    {
        // setting.UpdateVolumeSFX(sliderMusic.value);
        setting.UpdateVolumeMusic(sliderMusic.value);
        txtSliderMusic.text = $"{(int)(sliderMusic.value * 10f)}";
        // Utils.LOG($"[SettingPopup] UpdateMusicVolume {sliderMusic.value}");
    }



    private void SetGraphicText()
    {
        switch (levelQuality)
        {
            case 0: txtGraphic.SetText("Very Low"); break;
            case 1: txtGraphic.SetText("Low"); break;
            case 2: txtGraphic.SetText("Medium"); break;
            case 3: txtGraphic.SetText("High"); break;
            case 4: txtGraphic.SetText("Ultra High"); break;
        }
    }

    private void SetGraphicButton()
    {
        btnGraphicReduce.interactable = true;
        btnGraphicIncrease.interactable = true;
    }

    private void ApplySetting()
    {
        graphic.ChangeSettingGraphicLevel(levelQuality);
    }

    private void OnClickButtonGraphicReduce()
    {
        levelQuality--;
        SetGraphicText();
        SetGraphicButton();
        ApplySetting();

        if (levelQuality <= 0)
        {
            levelQuality = 0;
            btnGraphicReduce.interactable = false;
        }
    }

    private void OnClickButtonGraphicIncrease()
    {
        levelQuality++;
        SetGraphicText();
        SetGraphicButton();
        ApplySetting();

        if (levelQuality + 1 >= graphic.GetQualityLength())
        {
            levelQuality = graphic.GetQualityLength() - 1;
            btnGraphicIncrease.interactable = false;
        }
    }



    public void OnClickButtonCommingSoon()
    {
    }


    public void OnClickButtonResetAccount()
    {
    }


    public void OnClickButtonGetAllCharacters()
    {
        GameManager.Instance.Data.CheatGetAllCharacters();
        ViewManager.Instance.ShowPopupOk("INFO", "You have received all characters!");
    }


}
