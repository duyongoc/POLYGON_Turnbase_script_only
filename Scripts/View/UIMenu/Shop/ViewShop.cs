using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewShop : ViewBase
{


    [Header("[Component]")]
    [SerializeField] private ViewShopComponent compShop;


    // [Header("[Popup]")]
    // [SerializeField] private PopupActorSelection popupSelection;




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
        compShop.Load();
    }


    public void OnClickButtonReturn()
    {
        UIMenuScene.Instance.SetView(EViewGame.Lobby);
    }


    public void OnClickButtonShop()
    {
        ViewManager.Instance.ShowPopupOk("", "Sorry, this feature is not available for now!\n Update coming soon!");
    }


}
