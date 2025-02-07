using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupLoading : PopupBase
{


    [Header("[Setting]")]
    [SerializeField] private float timeDelay;
    [SerializeField] private Transform tranLoading;



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public void Show(float timer, Action cbDone)
    {
        Show(cbDone);
        StartCoroutine(IE_StartDelay(timer, cbDone));
    }


    private IEnumerator IE_StartDelay(float timer, Action cbDone)
    {
        yield return new WaitForSeconds(timer);
        cbDone?.Invoke();
        Hide();
    }


    // tranLoading.DOLocalRotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);


}
