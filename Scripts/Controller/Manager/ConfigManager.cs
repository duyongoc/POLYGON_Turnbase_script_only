using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{


    [Header("[Data]")]
    [SerializeField] private List<DataStage> dataGameStages;

    [Header("[Game]")]
    [SerializeField] private List<Stage> gameStages;
    [SerializeField] private List<CharacterItem> gameCharacters;
    [SerializeField] private List<string> stringCharacters;

    [Header("[Default]")]
    [SerializeField] private List<string> defaultCharacters;
    [SerializeField] private List<DataFormation> defaultFormation;


    [Header("[Config]")]
    [SerializeField] private Stage stageCurrent;
    [SerializeField] private Stage stageChallenge;


    [Header("[Data]")]
    [SerializeField] private UserInfo userInfo;



    // [properties]
    public List<DataStage> GameStages { get => dataGameStages; }
    public List<CharacterItem> GameCharacters { get => gameCharacters; }
    public List<string> DefaultCharacters { get => defaultCharacters; }


    public UserInfo UserInfo { get => userInfo; }
    public Stage StageCurrent { get => stageCurrent; }




    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion




    public CharacterEntity GetCharacterModelByKey(string key)
    {
        var actor = gameCharacters.Find(x => x.name.Equals(key));
        if (actor == null)
        {
            Debug.LogError($"GetActorInfoByKey is null: [{key}]");
        }

        // print("actor: " + actor.name);
        return actor.model as CharacterEntity;
    }


    public CharacterItem GetCharacterItemByKey(string key)
    {
        var actor = gameCharacters.Find(x => x.name.Equals(key));
        if (actor == null)
        {
            Debug.LogError($"GetCharacterItemByKey is null: [{key}]");
        }

        // print("GetCharacterItemByKey: " + skills.Count);
        return actor;
    }


    public void SetCurrentStage(string value)
    {
        stageCurrent = gameStages.Find(x => x.stageKey.Equals(value));
    }

    
    public void SetChallengeMode()
    {
        stageCurrent = stageChallenge;
    }





    // public GameObject GetObjectActorByKey(string key)
    // {
    //     // print($"TestSQL | key: {key} | level: {level} ");
    //     // var stat = from stats in listStats.Where(x => x.key.Equals(key))
    //     //            from result in stats.stats.Where(y => y.level.Equals(level))
    //     //            select result;

    //     // var stat = listStats.Where(x => x.key.Equals(key))
    //     //                     .SelectMany(x => x.stats)
    //     //                     .Where(stats => stats.level.Equals(level));

    //     var model = gamePrefabActors.Where(x => x.name.Equals(key)).FirstOrDefault();
    //     if (model == null)
    //     {
    //         Debug.LogError($"GetActorByKey is null: [{key}]");
    //     }

    //     // print("model: " + model.name);
    //     return model;
    // }




#if UNITY_EDITOR
    [ContextMenu("EditorLoad")]
    private void EditorLoad()
    {
        // userInfo.stages.ForEach(x => x.status = CONST.STAGE_COMPLETED);
        gameCharacters.ForEach(x =>
        {
            x.calculationAttributes.hp += Random.Range(400, 500);
            UnityEditor.EditorUtility.SetDirty(x);
        });
    }
#endif



}
