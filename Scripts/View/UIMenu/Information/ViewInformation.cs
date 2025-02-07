using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInformation : ViewBase
{


    [Header("[Component]")]
    [SerializeField] private ViewInformationComponent compActor;



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
        compActor.Load();
    }


    public void OnClickButtonReturn()
    {
        UIMenuScene.Instance.SetView(EViewGame.Lobby);
    }


}
