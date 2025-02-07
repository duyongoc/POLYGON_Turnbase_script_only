using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIModelShower : MonoBehaviour
{


    [Header("[UI Rotator]")]
    [SerializeField] private Transform tranUIRotator;
    [SerializeField] private List<UIModelRotator> ui_Rotators;


    [Header("[Main-Single Rotator]")]
    [SerializeField] private UIModelRotator mainRotator;


    // [properties]
    public List<UIModelRotator> Ui_Rotators { get => ui_Rotators; }



    #region UNITY
    private void OnEnable()
    {
        this.RegisterListener(EventID.ShowCharacter, OnShowMainRotator);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.ShowCharacter, OnShowMainRotator);
    }
    #endregion




    private void OnShowMainRotator(object param)
    {
        HideUiRotators();
        mainRotator.Show();
        mainRotator.LoadModel(param.ToString());
    }


    public void PlayMainRoratorAnimation(string anim)
    {
        // mainRotator.ModelAnim.TriggerAnim(anim);
    }


    public void ShowUiRotators()
    {
        mainRotator.Hide();
        tranUIRotator.SetActive(true);
    }


    public void HideUiRotators()
    {
        // mainRotator.Hide();
        tranUIRotator.SetActive(false);
    }


}
