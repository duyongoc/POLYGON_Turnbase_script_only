using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class MapController : MonoBehaviour
{


    [Header("[Cheating]")]
    public bool CanSpawnBot = false;
    public bool CanUpdateGame = false;
    public bool IsGameFinished = false;
    // public bool IsQuickTest = false;

    [Header("[Config]")]
    [SerializeField] private Transform[] positionEnemies;

    [Header("[Stage]")]
    [SerializeField] private DataStage stageCurrent;

    [Header("[Camera]")]
    [SerializeField] private CameraBattle cameraBattle;

    [Header("[UI Visual]")]
    [SerializeField] private UITextDamage uiTextDamage;

    [Header("[Actor]")]
    [SerializeField] private List<ActorBase> actors;
    [SerializeField] private List<ActorBase> enemies;


    // [private]
    private ViewManager _viewMgr;
    private GameManager _gameMgr;
    private UIBattle _uiBattle;



    #region Singleton can be destroy
    public static MapController Instance;
    public void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    private void Start()
    {
        _viewMgr = ViewManager.Instance;
        _gameMgr = GameManager.Instance;
        _uiBattle = UIBattle.Instance;

        Init();
        SetStage();
    }
    #endregion



    private void Init()
    {
        var list = new List<ActorBase>();
        var formation = _gameMgr.Data.GetFormation();

        // init list actors 
        // foreach (var actor in formation)
        // {
        //     list.Add(SpawnActor(actor.key));
        // }

        actors = list;
        cameraBattle.Init(list.FirstOrDefault(), list);
        InitUIActor();
    }


    private void SetStage()
    {
        // stageCurrent = ConfigManager.Instance.StateCurrent;
        // foreach (var bot in stageCurrent.bots)
        // {
        //     var actor = ConfigManager.Instance.GetObjectActorByKey(bot);
        //     var randTran = positionEnemies[UnityEngine.Random.Range(0, positionEnemies.Length)];
        //     var enemy = Instantiate(actor, randTran.position, Quaternion.identity).GetComponent<ActorBase>();
        //     enemy.SetActorKey(bot);
        //     enemy.SetActorRole(ERole.BOT);
        //     AddEnemy(enemy);
        // }
    }



    private ActorBase SpawnActor(string key)
    {
        // var keyObj = Instantiate(ConfigManager.Instance.GetObjectActorByKey(key), transform);
        // var actor = keyObj.GetComponent<ActorBase>();
        // actor.SetActorKey(key);
        // actor.SetActorRole(ERole.PLAYER);
        // return actor;
        return null;
    }


    private void InitUIActor()
    {
        var uiList = _uiBattle.Game.UI_Actor;
        uiList.ForEach(x => x.gameObject.SetActive(false));

        for (int i = 0; i < actors.Count; i++)
        {
            var ui = uiList[i];
            var actor = actors[i];

            if (actor != null)
            {
                ui.InitHealth(actor.ActorKey);
                ui.gameObject.SetActive(true);
                // actor.ActorHealth.InjectUIActor(ui);
            }
        }
    }




    public void AddEnemy(ActorBase character)
    {
        enemies.Add(character);
    }

    public void RemoveEnemy(ActorBase enemy)
    {
        if (enemies.Exists(x => x == enemy))
            enemies.Remove(enemy);

        if (enemies != null || enemies.Count <= 0)
        {
            if (IsGameFinished)
                return;

            IsGameFinished = true;
            ShowBattleWin();
        }
    }


    public void AddPlayer(ActorBase character)
    {
        actors.Add(character);
    }

    public void RemovePlayer(ActorBase enemy)
    {
        if (actors.Exists(x => x == enemy))
            actors.Remove(enemy);

        if (actors != null || actors.Count <= 0)
        {
            if (IsGameFinished)
                return;

            IsGameFinished = true;
            ShowBattleLose();
        }
    }



    private void ShowBattleWin()
    {
        // _gameMgr.Data.SaveStage(stageCurrent);
        // _gameMgr.Data.SaveCharacterByKey(stageCurrent.rewards);

        StartCoroutine(IE_BattleFinished(2, () =>
        {
            _viewMgr.ShowPopupStageResultWin(stageCurrent.rewards, _viewMgr.LoadSceneMenu);
        }));
    }


    private void ShowBattleLose()
    {
        StartCoroutine(IE_BattleFinished(2, () =>
        {
            _viewMgr.ShowPopupStageResultLose(_viewMgr.LoadSceneMenu);
        }));
    }



    private IEnumerator IE_BattleFinished(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }



    public void SpawnText(Transform spawnTransform, float damage, Color color, float scale)
    {
        if (damage <= 0)
            return;

        var randX = UnityEngine.Random.Range(-0.25f, 0.25f);
        var position = new Vector3(spawnTransform.position.x + randX, spawnTransform.position.y + 1, spawnTransform.position.z);
        var uiDamage = uiTextDamage.gameObject.SpawnToGarbage(position, Quaternion.identity);
        uiDamage.GetComponent<UITextDamage>().SetText(damage, color, scale);
    }


    public void SpawnText(Transform spawnTransform, string damage, Color color, float scale)
    {
        var randX = UnityEngine.Random.Range(-0.25f, 0.25f);
        var position = new Vector3(spawnTransform.position.x + randX, spawnTransform.position.y + 1, spawnTransform.position.z);
        var uiDamage = uiTextDamage.gameObject.SpawnToGarbage(position, Quaternion.identity);
        uiDamage.GetComponent<UITextDamage>().SetText(damage, color, scale);
    }


}




// public class SaveAndLoadSystem
// {
//     public static void Save(MapController data)
//     {
//         BinaryFormatter formatter = new BinaryFormatter();
//         string path = "Data/data.fun";
//         FileStream stream = new FileStream(path, FileMode.Create);
//         // MapController dt = new MapController(data);
//         // formatter.Serialize(stream, dt);
//         stream.Close();
//     }

//     public static MapController Load()
//     {
//         string path = "Data/data.fun";
//         if (File.Exists(path))
//         {
//             BinaryFormatter formatter = new BinaryFormatter();
//             FileStream stream = new FileStream(path, FileMode.Open);
//             MapController dt = formatter.Deserialize(stream) as MapController;
//             stream.Close();
//             return dt;
//         }
//         else
//         {
//             Debug.Log("Cant find data path");
//             return null;
//         }
//     }
// }