using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CommonTween))]
public class ViewBase : MonoBehaviour
{


    // [protected]
    [SerializeField] protected CommonTween tween;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected GameObject viewObject;
    [SerializeField] protected bool isShowing = false;


    // [properties]
    public bool IsShowing { get => isShowing; set => isShowing = value; }
    public GameObject ViewObject { get => viewObject;}




    #region VIRTUAL
    public virtual void Awake()
    {
        tween = GetComponent<CommonTween>();
        canvasGroup = GetComponent<CanvasGroup>();
        viewObject = GetComponent<Transform>().GetChild(0).gameObject;
    }


    public virtual void StartState()
    {
        isShowing = true;
        canvasGroup.blocksRaycasts = true;
        OnShowView(() => tween.ResetTween());
    }


    public virtual void UpdateState()
    {
        // TODO
    }


    public virtual void EndState()
    {
        isShowing = false;
        canvasGroup.blocksRaycasts = false;
        OnHideView();
        Reset();
    }


    public virtual void InitView() { }
    public virtual void Reset() { }
    #endregion



    /// <summary>  </summary>
    public void ActiveView(string viewName)
    {
    //    Debug.Log($"[ui] viewname {viewName} - this.name {this.name} = {viewName.Contains(this.name)}");
        viewObject.gameObject.SetActive(viewName.Equals(this.name));
    }


    /// <summary>  </summary>
    public void OnShowView(System.Action actionShow = null)
    {
        tween.PlayTween(actionShow);
    }


    /// <summary>  </summary>
    public void OnHideView(System.Action actionHide = null)
    {
        tween.PlayFading(tween.duration * 2, () =>
        {
            actionHide?.Invoke();
            HideView();
        });
    }


    protected void ShowView()
    {
        viewObject.gameObject.SetActive(true);
    }

    protected void HideView()
    {
        tween?.ResetTween();
        viewObject.gameObject.SetActive(false);
    }



    // <summary> Cheat_ShowPopup </summary>
    [ContextMenu("Cheat_ShowPopup")]
    public void CheatShowView()
    {
        isShowing = true;
        ShowView();
    }

    // <summary> Cheat_HidePopup </summary>
    [ContextMenu("Cheat_HidePopup")]
    public void CheatHideView()
    {
        isShowing = false;
        HideView();
    }

    // <summary> Cheat_LoadComponent </summary>
    [ContextMenu("Cheat_LoadComponent")]
    public void LoadComponent()
    {
        tween = GetComponent<CommonTween>();
        canvasGroup = GetComponent<CanvasGroup>();
        viewObject = GetComponent<Transform>().GetChild(0).gameObject;
    }


}
