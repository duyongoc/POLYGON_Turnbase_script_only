using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PreventDoubleClicked : MonoBehaviour
{

    [SerializeField] private Button btnCommon;
    [SerializeField] private float delayClicked = 1;



    #region 
    private void OnEnable()
    {
    }

    // private void OnDisable()
    // {
    // }
    #endregion



    public async void PreventClicked()
    {
        btnCommon.interactable = false;
        await EnableButtonAfterDelay(delayClicked);
        // print("[ui] PreventClicked " + this.name);
    }


    public void PreventClickedCouroutine()
    {
        btnCommon.interactable = false;
        StartCoroutine(EnableButtonAfterDelayCouroutine(delayClicked));
    }


    private async UniTask EnableButtonAfterDelay(float seconds)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(seconds));
        btnCommon.interactable = true;
    }


    private IEnumerator EnableButtonAfterDelayCouroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        btnCommon.interactable = true;
    }

}
