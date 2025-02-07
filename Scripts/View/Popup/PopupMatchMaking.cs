using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupMatchMaking : PopupBase
{
    

    [Header("[Setting]")]
    [SerializeField] private float timeDelay;
    [SerializeField] private Transform tranLoading;


    // [private]
    private Action callbackOnDone;



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    private void Load(float timer)
    {
        timeDelay = timer;
        tranLoading.DOLocalRotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        Utils.Delay(timer, () => Hide());
    }


    public void Show(float timer, Action cbOnDone)
    {
        callbackOnDone = cbOnDone;
        // Showing();
        Load(timer);
    }



    // public override void Show(Action cbOnShow = null, Action cbOnClose = null)
    // {
    //     Showing();
    // }


    // public override void Hide(Action cbOnClose = null)
    // {
    //     isShowing = false;
    //     Hiding();
    // }


    // private void Showing()
    // {
    //     OnShow();
    //     OnEffectShow();
    // }


    // private void Hiding()
    // {
    //     OnEffectHide(OnClose);
    // }


    // public void OnCallbackButtonOK()
    // {
    //     if (!isShowing)
    //         return;

    //     Hide();
    // }


    // public void OnCallbackButtonCancel()
    // {
    //     if (!isShowing)
    //         return;

    //     Hide();
    // }


    // private void OnShow()
    // {
    //     isShowing = true;
    //     popupObject.SetActive(true);
    // }


    // private void OnClose()
    // {
    //     isShowing = false;
    //     popupObject.SetActive(false);
    //     callbackOnDone?.Invoke();

    //     // reset action
    //     ResetAction();
    // }


    // private void ResetAction()
    // {
    //     callbackOnDone = null;
    // }


}
