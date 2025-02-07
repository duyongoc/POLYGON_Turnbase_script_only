using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMenuScene : Singleton<UIMenuScene>
{


    [Header("[Master Canvas]")]
    [SerializeField] private Camera uiCamera;
    [SerializeField] private UIModelShower uiModelShower;
    [SerializeField] private UITutorial uiTutorial;
    [SerializeField] private bool openCurrentScene = false;


    [Header("[View]")]
    [SerializeField] private EViewGame currentView;
    [SerializeField] private EViewGame previousView;
    [SerializeField] private ViewBase viewState;
    [SerializeField] private List<ViewBase> viewGames;


    [Header("[View UI]")]
    [SerializeField] private ViewLoading viewLoading;
    [SerializeField] private ViewSelection viewSelection;
    [SerializeField] private ViewLobby viewMenu;
    [SerializeField] private ViewInformation viewInformation;
    [SerializeField] private ViewStage viewStage;
    [SerializeField] private ViewShop viewShop;
    [SerializeField] private ViewGuild viewGuild;
    [SerializeField] private ViewBoss viewBoss;
    [SerializeField] private ViewBattlePass viewBattlePass;
    [SerializeField] private ViewRoulette viewRoulette;


    // [private]
    private string _pathViewActive;


    // [properties]
    public UITutorial Tutorial { get => uiTutorial; }
    public UIModelShower Model { get => uiModelShower; }




    #region UNITY
    private void Start()
    {
        LoadView();
    }

    private void Update()
    {
        if (viewState == null)
            return;

        viewState.UpdateState();
        // GetEventSystemRaycastResults();
    }
    #endregion




    // returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    // gets all event systen raycast results of current mouse or touch position.
    public static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        foreach (var result in raysastResults)
            print($"result: {result.gameObject.name}");

        return raysastResults;
    }



    public void LoadView()
    {
        if (openCurrentScene)
        {
            SetView(currentView);
            return;
        }

        SetView(EViewGame.Loading);
    }


    /// <summary> Update View </summary>
    public void UpdateView(EViewGame newView)
    {
        switch (newView)
        {
            case EViewGame.Loading:
                viewLoading.Load(); break;

            case EViewGame.Selection:
                viewSelection.Load(); break;

            case EViewGame.Information:
                viewInformation.Load(); break;

            case EViewGame.Lobby:
                viewMenu.Load(); break;

            case EViewGame.Stage:
                viewStage.Load(); break;

            case EViewGame.Shop:
                viewShop.Load(); break;

            case EViewGame.Guild:
                viewGuild.Load(); break;

            case EViewGame.Boss:
                viewBoss.Load(); break;

            case EViewGame.BattlePass:
                viewBattlePass.Load(); break;

            case EViewGame.Roulette:
                viewRoulette.Load(); break;
        }
    }


    //<summary> </summary>
    public void SetView(EViewGame newView)
    {
        previousView = currentView;
        currentView = newView;

        // get name of current view - set active for the views
        UpdateView(newView);
        SetCurrentView(GetViewPath());
    }


    //<summary> </summary>
    public void HideViews()
    {
        currentView = EViewGame.None;
        SetActiveView("None");
    }


    //<summary> </summary>
    private ViewBase GetViewPath()
    {
        _pathViewActive = string.Format($"View{currentView}");
        var pathView = viewGames.Find(x => x.name.Contains(currentView.ToString()));
        // pathViewActive = string.Concat(pathViewActive, $" | View{_previousView}");
        return pathView;
    }


    //<summary> Set state of view </summary>
    private void SetCurrentView(ViewBase viewBase)
    {
        if (viewState != null)
            viewState.EndState();

        viewState = viewBase;
        viewState.StartState();
        SetActiveView(_pathViewActive);
    }


    //<summary> Active views </summary>
    private void SetActiveView(string viewName)
    {
        // viewGames.ForEach(x => x.ActiveView("None"));
        viewGames.ForEach(x => x.ActiveView(viewName));
    }



    public void HideCamera()
    {
        uiCamera.gameObject.SetActive(false);
        HideAllView();
    }


    public void ShowCamera()
    {
        uiCamera.gameObject.SetActive(true);
        HideAllView();
    }



    // <summary> Cheat_LoadViewEditor </summary>
    [ContextMenu("Cheat_LoadViewEditor")]
    public void LoadViewEditor()
    {
        var views = GetComponentsInChildren<ViewBase>();
        viewGames = new List<ViewBase>(views);
    }


    // <summary> Cheat_HideAllView </summary>
    [ContextMenu("Cheat_HideAllView")]
    public void HideAllView()
    {
        viewGames.ForEach(x => x.IsShowing = false);
        viewGames.ForEach(x => x.ViewObject.SetActive(false));
    }


}
