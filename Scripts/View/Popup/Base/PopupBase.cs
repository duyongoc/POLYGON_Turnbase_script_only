using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(CommonTween))]
public abstract class PopupBase : MonoBehaviour
{


    // [protected]
    [SerializeField] protected CommonTween _tween;
    [SerializeField] protected GameObject popupObject;
    [SerializeField] protected bool isShowing = false;


    // [private]
    protected Action callbackButtonOK;
    protected Action callbackButtonCancel;



    #region  UNITY
    private void Awake()
    {
        InitPopup();
    }

    private void Update()
    {
        if (!isShowing)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
    #endregion


    // virtual
    // public virtual void Show(string content, Action cbOnShow = null, Action cbOnClose = null) { }
    // public virtual void Show(string[] content, Action cbOnShow = null, Action cbOnClose = null) { }
    // public virtual void SetCallback(Action cbButtonOK = null, Action cbButtonCancel = null) { }



    public virtual void Show(Action cbButtonOK = null, Action cbButtonCancel = null)
    {
        callbackButtonOK = cbButtonOK;
        callbackButtonCancel = cbButtonCancel;
        Showing();
    }


    public virtual void Hide()
    {
        isShowing = false;
        Hiding();
    }


    private void Showing()
    {
        OnShow();
        OnEffectShow();
    }


    private void Hiding()
    {
        OnEffectHide(OnClose);
    }



    public void OnCallbackButtonOK()
    {
        if (!isShowing)
            return;

        Hide();
        callbackButtonOK?.Invoke();
    }


    public void OnCallbackButtonCancel()
    {
        if (!isShowing)
            return;

        Hide();
        callbackButtonCancel?.Invoke();
    }



    protected void OnShow()
    {
        isShowing = true;
        popupObject.SetActive(true);
    }


    protected void OnClose()
    {
        isShowing = false;
        popupObject.SetActive(false);
        ResetAction();
    }


    protected void ResetAction()
    {
        callbackButtonOK = null;
        callbackButtonCancel = null;
    }



    protected void InitPopup()
    {
        isShowing = false;
        popupObject.SetActive(false);
    }


    protected void OnEffectShow(Action actionShow = null)
    {
        _tween.PlayTween(actionShow);
    }


    protected void OnEffectHide(Action actionHide = null)
    {
        _tween.PlayFading(_tween.duration, actionHide);
    }



    // <summary> Cheat_ShowPopup </summary>
    [ContextMenu("Cheat_ShowPopup")]
    public void CheatShowPopup()
    {
        if (Application.isEditor && Application.isPlaying)
        {
            Show();
            return;
        }

        isShowing = true;
        popupObject.SetActive(true);
    }


    // <summary> Cheat_HidePopup </summary>
    [ContextMenu("Cheat_HidePopup")]
    public void CheatHidePopup()
    {
        if (Application.isEditor && Application.isPlaying)
        {
            Hide();
            return;
        }

        isShowing = false;
        popupObject.SetActive(false);
    }


}
