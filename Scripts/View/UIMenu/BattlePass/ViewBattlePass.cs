using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBattlePass : ViewBase
{
    
    [Header("[Component]")]
    [SerializeField] private ViewBattlePassComponent compBattlePass;



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
        compBattlePass.Load();
    }


    public void OnClickButtonReturn()
    {
        UIMenuScene.Instance.SetView(EViewGame.Lobby);
    }

}
