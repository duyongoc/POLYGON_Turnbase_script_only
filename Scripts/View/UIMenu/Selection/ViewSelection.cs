using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSelection : ViewBase
{


    [Header("[Component]")]
    [SerializeField] private ViewSelectionComponent compBegin;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    #region STATE
    public override void StartState() { base.StartState(); }
    public override void UpdateState() { base.UpdateState(); }
    public override void EndState() { base.EndState(); }
    #endregion




    public void Load()
    {
        compBegin.Load();
    }


    public void OnClickButtonReturn()
    {

    }


    public void OnClickButtonSelectActor()
    {
        compBegin.OnSelectCharacter();
    }


}
