using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewStageComponent : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private float scrollDefault = 100;
    [SerializeField] private float scrollOffset = 100;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private UIItemStage prefabItem;
    [SerializeField] private List<UIItemStage> cachePrefabs;


    [Header("[DATA]")]
    private ViewManager viewMgr;
    private ConfigManager configMgr;
    private List<DataStage> stages;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    public void Load()
    {
        viewMgr = ViewManager.Instance;
        configMgr = ConfigManager.Instance;
        stages = GameManager.Instance.Data.GetStages();

        LoadStages(stages);
        LoadStageStatus();
    }


    private void LoadStages(List<DataStage> actors)
    {
        // clear prefab cache
        ClearCache();
        ResetScrollSize(scrollDefault);

        // create prefab
        foreach (var item in actors)
        {
            var prefab = Instantiate(prefabItem, scrollContent.transform, false);
            prefab.Init(item, CallbackClick);

            cachePrefabs.Add(prefab);
            ChangeScrollSize(scrollOffset);
        }
    }


    private void LoadStageStatus()
    {
        var indexCurrent = cachePrefabs.FindIndex(x => x.Data.status.Equals(CONST.STAGE_INCOMPLETED));

        for (int i = 0; i < cachePrefabs.Count; i++)
        {
            var prefab = cachePrefabs[i];

            // stage completed
            if (prefab.Data.status.Equals(CONST.STAGE_COMPLETED) || i < indexCurrent)
            {
                prefab.SetStageCompleted();
                continue;
            }

            // stage current
            if (i == indexCurrent)
            {
                prefab.SetStageCurrent();
                continue;
            }

            // stage lock
            prefab.SetStageLock();
        }
    }



    private void CallbackClick(string key)
    {
        var currentStage = stages.Find(x => x.key.Equals(key));
        viewMgr.ShowPopupStageConfirm(currentStage.rewards, () =>
        {
            configMgr.SetCurrentStage(key);
            viewMgr.ShowLoading(1, () => { viewMgr.LoadSceneBattle(); });
        });
    }



    private void ClearCache()
    {
        cachePrefabs.ForEach(x => { if (x != null) Destroy(x.gameObject); });
        cachePrefabs.Clear();
    }


    private void ResetScrollSize(float value)
    {
        var scroll = scrollContent.AsRectTransform();
        scroll.sizeDelta = new Vector2(scroll.sizeDelta.x, value);
    }


    private void ChangeScrollSize(float value)
    {
        var scroll = scrollContent.AsRectTransform();
        scroll.sizeDelta = new Vector2(scroll.sizeDelta.x, scroll.sizeDelta.y + value);
    }




}
