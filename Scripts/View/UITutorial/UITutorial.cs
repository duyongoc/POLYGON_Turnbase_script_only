using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial : MonoBehaviour
{

    public enum EStep
    {
        Step1,
        Step2,
        Step3,
        Step4,
        Done,
    }


    [Header("[Setting]")]
    [SerializeField] private bool isFinishedTutorial;
    [SerializeField] private EStep step;
    [SerializeField] private GameObject[] stepObjects;

    [Header("[Canvas]")]
    [SerializeField] private int orderDefault = 25;
    [SerializeField] private int orderHighlight = 100;
    [SerializeField] private Canvas canvasBtnPlay;
    [SerializeField] private Canvas canvasBtnInformation;




    #region UNITY
    private void Start()
    {
        Init();
    }

    // private void Update()
    // {
    // }
    #endregion



    private void Init()
    {
        if (PlayerPrefs.HasKey(CONST.KEY_TUTORIAL))
        {
            isFinishedTutorial = true;
            return;
        }

        step = EStep.Step1;
    }


    public void ShowTutorial()
    {
        if (isFinishedTutorial)
            return;

        ShowStepCurrent();
        ShowStepStatus();
    }


    public void HideTutorial()
    {
        foreach (var item in stepObjects)
        {
            item.SetActive(false);
        }
    }


    public void ShowNext()
    {
        step++;
        ShowStepCurrent();
        ShowStepStatus();
    }


    public void UpdateNextStep()
    {
        if (isFinishedTutorial)
            return;

        step++;
    }


    private void ShowStepCurrent()
    {
        foreach (var item in stepObjects)
        {
            item.SetActive(false);
        }

        // active current step object
        if (step < EStep.Done)
        {
            stepObjects[(int)step].SetActive(true);
        }

        // check tutorial has done yet
        if (step.Equals(EStep.Done))
        {
            SetTutorialDone();
        }
    }


    private void ShowStepStatus()
    {
        canvasBtnPlay.sortingOrder = orderDefault;
        canvasBtnInformation.sortingOrder = orderDefault;

        switch (step)
        {
            case EStep.Step2:
                canvasBtnInformation.sortingOrder = orderHighlight;
                return;

            case EStep.Step3:
                canvasBtnPlay.sortingOrder = orderHighlight;
                return;
        }
    }


    private void SetTutorialDone()
    {
        isFinishedTutorial = true;
        PlayerPrefs.SetString(CONST.KEY_TUTORIAL, "tutorial_done");
    }



    public void OnClickButtonSkip()
    {
        ShowNext();
    }


}
