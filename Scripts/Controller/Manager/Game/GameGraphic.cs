using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameGraphic : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private int levelQuality = 0;
    [SerializeField] private RenderPipelineAsset[] qualityLevels;



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
        // if (qualityLevels.Length <= 0)
        // {
        //     print("[Game Graphic] qualityLevels is empty.");
        //     return;
        // }
        // // high is graphic default
        // ChangeSettingGraphicLevel(levelQuality);
    }


    public int GetGraphicLevel()
    {
        levelQuality = QualitySettings.GetQualityLevel();
        return levelQuality;
    }


    public void ChangeSettingGraphicLevel(int index)
    {
        levelQuality = index;
        switch (index)
        {
            case 0: ChangeGraphicVeryLow(); break;
            case 1: ChangeGraphicLow(); break;
            case 2: ChangeGraphicMedium(); break;
            case 3: ChangeGraphicHigh(); break;
            case 4: ChangeGraphicUltraHigh(); break;
        }
    }


    public int GetQualityLength()
    {
        return qualityLevels.Length;
    }


    public void ChangeGraphicVeryLow()
    {
        ChangLevel(0);
    }


    public void ChangeGraphicLow()
    {
        ChangLevel(1);
    }


    public void ChangeGraphicMedium()
    {
        ChangLevel(2);
    }


    public void ChangeGraphicHigh()
    {
        ChangLevel(3);
    }


    public void ChangeGraphicUltraHigh()
    {
        ChangLevel(4);
    }


    public void ChangLevel(int value)
    {
        levelQuality = value;
        QualitySettings.SetQualityLevel(value, true);
        QualitySettings.renderPipeline = qualityLevels[value];
    }

}
