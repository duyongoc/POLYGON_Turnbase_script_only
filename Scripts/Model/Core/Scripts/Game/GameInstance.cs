﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if ENABLE_PURCHASING && UNITY_PURCHASING && (UNITY_IOS || UNITY_ANDROID)
using UnityEngine.Purchasing;
#endif

#if ENABLE_PURCHASING && UNITY_PURCHASING && (UNITY_IOS || UNITY_ANDROID)
public partial class GameInstance : MonoBehaviour, IStoreListener
#else
public partial class GameInstance : MonoBehaviour
#endif
{


    public enum LoadAllPlayerDataState
    {
        GoToManageScene,
        GoToBattleScene,
    }


    [Header("[DB]")]
    public GameDatabase Database;

    // [Header("[Setting]")]
    // public GameObject loadingObject;
    // public UIInputDialog inputDialog;
    // public UIItemList rewardItemsDialog;
    // public UIMessageDialog messageDialog;

    public static GameInstance Singleton;
    public static GameDatabase GameDatabase;
    public static BaseGameService GameService;
    public static List<string> AvailableLootBoxes = new List<string>();
    public static List<string> AvailableIapPackages = new List<string>();

    private Queue<UIMessageDialog.Data> messageDialogData = new Queue<UIMessageDialog.Data>();
    private LoadAllPlayerDataState loadAllPlayerDataState;
    private static bool isPlayerAuthListLoaded;
    private static bool isPlayerCurrencyListLoaded;
    private static bool isPlayerFormationListLoaded;
    private static bool isPlayerItemListLoaded;
    private static bool isPlayerStaminaListLoaded;
    private static bool isPlayerUnlockItemListLoaded;
    private static bool isPlayerClearStageListLoaded;
    private static bool isAvailableLootBoxListLoaded;
    private static bool isAvailableIapPackageListLoaded;



    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        // DontDestroyOnLoad(gameObject);
        // GameService = GetComponent<BaseGameService>();
        // GameService.onServiceStart.RemoveListener(OnGameServiceStart);
        // GameService.onServiceStart.AddListener(OnGameServiceStart);
        // GameService.onServiceFinish.RemoveListener(OnGameServiceFinish);
        // GameService.onServiceFinish.AddListener(OnGameServiceFinish);

        HideMessageDialog();
        HideInputDialog();
        HideRewardItemsDialog();
        HideLoading();
    }



    private void OnGameServiceStart()
    {
        ShowLoading();
    }

    private void OnGameServiceFinish()
    {
        HideLoading();
    }

    public void OnGameServiceError(string error, UnityAction errorAction)
    {
        Debug.LogError("OnGameServiceError: " + error);
        var errorText = string.IsNullOrEmpty(error) || !LanguageManager.Texts.ContainsKey(error) ? "" : LanguageManager.Texts[error];
        messageDialogData.Enqueue(new UIMessageDialog.Data(LanguageManager.Texts[GameText.TITLE_ERROR_DIALOG], errorText, errorAction));
        ShowError();
    }

    public void OnGameServiceError(string error)
    {
        OnGameServiceError(error, null);
    }

    public void OnGameServiceLogin(PlayerResult result)
    {
        if (!result.Success)
            return;

        var player = result.player;
        Player.CurrentPlayer = player;
        print(Player.CurrentPlayer.SelectedFormation);
        GameService.SetPrefsLogin(player.Id, player.LoginToken);

        if (string.IsNullOrEmpty(player.ProfileName) || string.IsNullOrEmpty(player.ProfileName.Trim()))
            SetProfileName();
        else
            GetAllPlayerData(LoadAllPlayerDataState.GoToManageScene);
    }

    public void OnGameServiceLogout()
    {
        isPlayerAuthListLoaded = false;
        isPlayerCurrencyListLoaded = false;
        isPlayerFormationListLoaded = false;
        isPlayerItemListLoaded = false;
        isPlayerStaminaListLoaded = false;
        isPlayerUnlockItemListLoaded = false;
        isPlayerClearStageListLoaded = false;
        isAvailableLootBoxListLoaded = false;
        isAvailableIapPackageListLoaded = false;
        LoadLoginScene();
    }

    public void OnGameServiceItemResult(ItemResult result)
    {
        if (!result.Success)
            return;

        PlayerItem.SetDataRange(result.createItems);
        PlayerItem.SetDataRange(result.updateItems);
        PlayerItem.RemoveDataRange(result.deleteItemIds);
    }

    public void OnGameServiceStartStageResult(StartStageResult result)
    {
        if (!result.Success)
            return;

        PlayerStamina.SetData(result.stamina);
    }

    public void OnGameServiceFinishStageResult(FinishStageResult result)
    {
        if (!result.Success)
            return;

        Player.SetData(result.player);
        PlayerCurrency.SetDataRange(result.updateCurrencies);
        PlayerItem.SetDataRange(result.createItems);
        PlayerItem.SetDataRange(result.updateItems);
        PlayerItem.RemoveDataRange(result.deleteItemIds);
        PlayerClearStage.SetData(result.clearStage);
    }

    public void OnGameServiceSetProfileNameResult(PlayerResult result)
    {
        if (!result.Success)
            return;

        var currentPlayer = Player.CurrentPlayer;
        if (currentPlayer != null)
            currentPlayer.ProfileName = result.player.ProfileName;
    }

    public void OnGameServiceAuthListResult(AuthListResult result)
    {
        if (!result.Success)
            return;

        PlayerAuth.SetDataRange(result.list);
    }

    public void OnGameServiceCurrencyListResult(CurrencyListResult result)
    {
        if (!result.Success)
            return;

        PlayerCurrency.SetDataRange(result.list);
    }

    public void OnGameServiceFormationListResult(FormationListResult result)
    {
        if (!result.Success)
            return;

        PlayerFormation.SetDataRange(result.list);
    }

    public void OnGameServiceItemListResult(ItemListResult result)
    {
        if (!result.Success)
            return;

        PlayerItem.SetDataRange(result.list);
    }

    public void OnGameServiceStaminaListResult(StaminaListResult result)
    {
        if (!result.Success)
            return;

        PlayerStamina.SetDataRange(result.list);
    }

    public void OnGameServiceUnlockItemListResult(UnlockItemListResult result)
    {
        if (!result.Success)
            return;

        PlayerUnlockItem.SetDataRange(result.list);
    }

    public void OnGameServiceClearStageListResult(ClearStageListResult result)
    {
        if (!result.Success)
            return;

        PlayerClearStage.SetDataRange(result.list);
    }

    public void OnGameServiceAvailableLootBoxListResult(AvailableLootBoxListResult result)
    {
        if (!result.Success)
            return;

        AvailableLootBoxes.Clear();
        AvailableLootBoxes.AddRange(result.list);
    }

    public void OnGameServiceAvailableIapPackageListResult(AvailableIapPackageListResult result)
    {
        if (!result.Success)
            return;

        AvailableIapPackages.Clear();
        AvailableIapPackages.AddRange(result.list);
    }

    #region Current Player Data Validation
    /// <summary>
    /// Set profile name first time, when it's not already set.
    /// </summary>
    private void SetProfileName()
    {
        ShowProfileNameInputDialog(OnSetProfileNameSuccess, (error) => OnGameServiceError(error, SetProfileName));
    }

    private void OnSetProfileNameSuccess(PlayerResult result)
    {
        OnGameServiceSetProfileNameResult(result);
        GetAllPlayerData(LoadAllPlayerDataState.GoToManageScene);
    }

    /// <summary>
    /// Get all current player data after login
    /// </summary>
    public void GetAllPlayerData(LoadAllPlayerDataState loadAllPlayerDataState)
    {
        this.loadAllPlayerDataState = loadAllPlayerDataState;
        GetAuthList();
        GetCurrencyList();
        GetFormationList();
        GetItemList();
        GetStaminaList();
        GetUnlockItemList();
        GetClearStageList();
        GetAvailableLootBoxList();
        GetAvailableIAPPackageList();
    }

    /// <summary>
    /// Get authentication list for current player
    /// </summary>
    private void GetAuthList()
    {
        isPlayerAuthListLoaded = false;
        GameService.GetAuthList(OnGetAuthListSuccess, (error) => OnGameServiceError(error, GetAuthList));
    }

    private void OnGetAuthListSuccess(AuthListResult result)
    {
        OnGameServiceAuthListResult(result);
        isPlayerAuthListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get currency list for current player
    /// </summary>
    private void GetCurrencyList()
    {
        isPlayerCurrencyListLoaded = false;
        GameService.GetCurrencyList(OnGetCurrencyListSuccess, (error) => OnGameServiceError(error, GetCurrencyList));
    }

    private void OnGetCurrencyListSuccess(CurrencyListResult result)
    {
        OnGameServiceCurrencyListResult(result);
        isPlayerCurrencyListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get formation list for current player
    /// </summary>
    private void GetFormationList()
    {
        isPlayerFormationListLoaded = false;
        GameService.GetFormationList(OnGetFormationListSuccess, (error) => OnGameServiceError(error, GetFormationList));
    }

    private void OnGetFormationListSuccess(FormationListResult result)
    {
        OnGameServiceFormationListResult(result);
        isPlayerFormationListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get item list for current player
    /// </summary>
    private void GetItemList()
    {
        isPlayerItemListLoaded = false;
        GameService.GetItemList(OnGetItemListSuccess, (error) => OnGameServiceError(error, GetItemList));
    }

    private void OnGetItemListSuccess(ItemListResult result)
    {
        OnGameServiceItemListResult(result);
        isPlayerItemListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get stamina list for current player
    /// </summary>
    private void GetStaminaList()
    {
        isPlayerStaminaListLoaded = false;
        GameService.GetStaminaList(OnGetStaminaListSuccess, (error) => OnGameServiceError(error, GetStaminaList));
    }

    private void OnGetStaminaListSuccess(StaminaListResult result)
    {
        OnGameServiceStaminaListResult(result);
        isPlayerStaminaListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get unlock item list for current player
    /// </summary>
    private void GetUnlockItemList()
    {
        isPlayerUnlockItemListLoaded = false;
        GameService.GetUnlockItemList(OnGetUnlockItemListSuccess, (error) => OnGameServiceError(error, GetUnlockItemList));
    }

    private void OnGetUnlockItemListSuccess(UnlockItemListResult result)
    {
        OnGameServiceUnlockItemListResult(result);
        isPlayerUnlockItemListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get clear stage list for current player
    /// </summary>
    private void GetClearStageList()
    {
        isPlayerClearStageListLoaded = false;
        GameService.GetClearStageList(OnGetClearStageListSuccess, (error) => OnGameServiceError(error, GetClearStageList));
    }

    private void OnGetClearStageListSuccess(ClearStageListResult result)
    {
        OnGameServiceClearStageListResult(result);
        isPlayerClearStageListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get list of available to open loot boxes
    /// </summary>
    private void GetAvailableLootBoxList()
    {
        isAvailableLootBoxListLoaded = false;
        GameService.GetAvailableLootBoxList(OnGetAvailableLootBoxListSuccess, (error) => OnGameServiceError(error, GetClearStageList));
    }

    private void OnGetAvailableLootBoxListSuccess(AvailableLootBoxListResult result)
    {
        OnGameServiceAvailableLootBoxListResult(result);
        isAvailableLootBoxListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// Get list of available to open iap packages
    /// </summary>
    private void GetAvailableIAPPackageList()
    {
        isAvailableIapPackageListLoaded = false;
        GameService.GetAvailableIapPackageList(OnGetAvailableIAPPackageListSuccess, (error) => OnGameServiceError(error, GetClearStageList));
    }

    private void OnGetAvailableIAPPackageListSuccess(AvailableIapPackageListResult result)
    {
        OnGameServiceAvailableIapPackageListResult(result);
        isAvailableIapPackageListLoaded = true;
        ValidatePlayerData();
    }

    /// <summary>
    /// When receive all current player data, load manage scene
    /// </summary>
    private void ValidatePlayerData()
    {
        if (isPlayerAuthListLoaded &&
            isPlayerCurrencyListLoaded &&
            isPlayerFormationListLoaded &&
            isPlayerItemListLoaded &&
            isPlayerStaminaListLoaded &&
            isPlayerUnlockItemListLoaded &&
            isPlayerClearStageListLoaded &&
            isAvailableLootBoxListLoaded &&
            isAvailableIapPackageListLoaded)
        {
            // Setup purchasing when all data loaded
            SetupPurchasing();
            switch (loadAllPlayerDataState)
            {
                case LoadAllPlayerDataState.GoToManageScene:
                    LoadManageScene();
                    break;
                case LoadAllPlayerDataState.GoToBattleScene:
                    LoadBattleScene();
                    break;
            }
        }
    }
    #endregion

    #region Error/Warning/Loading Handler
    private void ShowError()
    {
        if (messageDialogData.Count > 0)
        {
            var data = messageDialogData.Dequeue();
            ShowMessageDialog(data.title, data.content, () =>
            {
                ShowError();
                if (data.actionYes != null)
                    data.actionYes.Invoke();
            }, data.actionNo, data.actionCancel);
        }
    }

    public void ShowMessageDialog(string title,
        string content,
        UnityAction actionYes = null,
        UnityAction actionNo = null,
        UnityAction actionCancel = null)
    {
        // if (messageDialog == null)
        // {
        //     Debug.LogWarning("`Message Dialog` has not been set");
        //     return;
        // }
        // if (!messageDialog.IsVisible())
        // {
        //     messageDialog.Title = title;
        //     messageDialog.Content = content;
        //     messageDialog.actionYes = actionYes;
        //     messageDialog.actionNo = actionNo;
        //     messageDialog.actionCancel = actionCancel;
        //     messageDialog.Show();
        // }
    }

    public void HideMessageDialog()
    {
        // if (messageDialog == null)
        // {
        //     Debug.LogWarning("`Message Dialog` has not been set");
        //     return;
        // }
        // messageDialog.Hide();
    }

    private void ShowProfileNameInputDialog(UnityAction<PlayerResult> onSuccess, UnityAction<string> onError)
    {
        // if (inputDialog == null)
        // {
        //     Debug.LogWarning("`Input Dialog` has not been set");
        //     return;
        // }
        // ShowInputDialog(LanguageManager.Texts[GameText.TITLE_PROFILE_NAME_DIALOG],
        //     LanguageManager.Texts[GameText.CONTENT_PROFILE_NAME_DIALOG],
        //     () =>
        //     {
        //         var input = inputDialog.InputContent;
        //         GameService.SetProfileName(input, onSuccess, onError);
        //     });
        // inputDialog.InputPlaceHolder = LanguageManager.Texts[GameText.PLACE_HOLDER_PROFILE_NAME];
    }

    public void ShowInputDialog(string title,
        string content,
        UnityAction actionYes = null,
        UnityAction actionNo = null,
        UnityAction actionCancel = null)
    {
        // if (inputDialog == null)
        // {
        //     Debug.LogWarning("`Input Dialog` has not been set");
        //     return;
        // }
        // inputDialog.SetInputPropertiesToDefault();
        // if (!inputDialog.IsVisible())
        // {
        //     inputDialog.Title = title;
        //     inputDialog.Content = content;
        //     inputDialog.actionYes = actionYes;
        //     inputDialog.actionNo = actionNo;
        //     inputDialog.actionCancel = actionCancel;
        //     inputDialog.Show();
        // }
    }

    public void HideInputDialog()
    {
        // if (inputDialog == null)
        // {
        //     Debug.LogWarning("`Input Dialog` has not been set");
        //     return;
        // }
        // inputDialog.Hide();
    }

    public void ShowRewardItemsDialog(List<PlayerItem> items)
    {
        // if (rewardItemsDialog == null)
        // {
        //     Debug.LogWarning("Reward Items Dialog` has not been set");
        //     return;
        // }
        // rewardItemsDialog.SetListItems(items);
        // rewardItemsDialog.Show();
    }

    public void HideRewardItemsDialog()
    {
        // if (rewardItemsDialog == null)
        // {
        //     Debug.LogWarning("Reward Items Dialog` has not been set");
        //     return;
        // }
        // rewardItemsDialog.Hide();
    }

    public void WarnNotEnoughSoftCurrency()
    {
        OnGameServiceError(GameServiceErrorCode.NOT_ENOUGH_SOFT_CURRENCY);
    }

    public void WarnNotEnoughHardCurrency()
    {
        OnGameServiceError(GameServiceErrorCode.NOT_ENOUGH_HARD_CURRENCY);
    }

    public void WarnNotEnoughStageStamina()
    {
        OnGameServiceError(GameServiceErrorCode.NOT_ENOUGH_STAGE_STAMINA);
    }

    public void ShowLoading()
    {
        // if (loadingObject == null)
        // {
        //     Debug.LogWarning("`Loading Object` has not been set");
        //     return;
        // }
        // ++countLoading;
        // if (countLoading > 0)
        //     loadingObject.SetActive(true);
    }

    public void HideLoading()
    {
        // if (loadingObject == null)
        // {
        //     Debug.LogWarning("`Loading Object` has not been set");
        //     return;
        // }
        // --countLoading;
        // if (countLoading <= 0)
        // {
        //     loadingObject.SetActive(false);
        //     countLoading = 0;
        // }
    }
    #endregion



    #region Simplified Scene Loading Functions
    public void LoadLoginScene()
    {
        // LoadSceneIfNotLoaded(loginScene, loadIfNotLoaded);
    }

    public void LoadManageScene()
    {
        // LoadSceneIfNotLoaded(manageScene, loadIfNotLoaded);
    }

    public void LoadBattleScene()
    {
        LoadScene("Battle");
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    #endregion


}
