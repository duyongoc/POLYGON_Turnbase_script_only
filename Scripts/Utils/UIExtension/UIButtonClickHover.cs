using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class UIButtonClickHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    

    [Header("[Setting]")]
    [SerializeField] private bool defaultHover = true;
    [SerializeField] private bool defaultChoosen = true;
    [SerializeField] private GameObject backgroundHover;
    [SerializeField] private GameObject backgroundChoosen;



    #region UNITY
    private void Awake()
    {
        OnHoverUnselect();
    }

    private void OnDisable()
    {
        OnHoverUnselect();
    }
    #endregion



    // public void SetActiveChoosen(bool value)
    // {
    //     backgroundChoosen?.SetActive(value);
    // }


    // private void SetActiveHover(bool value)
    // {
    //     backgroundHover?.SetActive(value);
    // }



    public void OnChoosenSelect()
    {
        if (backgroundChoosen)
            backgroundChoosen.SetActive(true);
    }

    public void OnChoosenUnselect()
    {
        if (backgroundChoosen)
            backgroundChoosen.SetActive(false);
    }


    public void OnHoverSelect()
    {
        if (backgroundHover)
            backgroundHover.SetActive(true);
    }


    public void OnHoverUnselect()
    {
        if (backgroundHover)
            backgroundHover.SetActive(false);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (!defaultChoosen)
            return;

        OnChoosenSelect();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!defaultHover)
            return;

        OnHoverSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!defaultHover)
            return;

        OnHoverUnselect();
    }


}
