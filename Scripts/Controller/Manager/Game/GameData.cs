using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class GameData : MonoBehaviour
{



    [Header("[Data]")]
    [SerializeField] private bool hasInitUserData;
    [SerializeField] private UserInfo userInfo;


    // [properties]
    public UserInfo UserInfo { get => userInfo; }
    public bool HasInitUserData { get => hasInitUserData; }



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public void Init()
    {
        SetDefaultUserData();
    }


    private void SetDefaultUserData()
    {
        // init user data
        if (GameManager.Instance.RunGameLocal)
        {
            Utils.LOG("Cheating RunGameLocal [GameManager] user data");
            LoadLocalUserData();
            return;
        }

        // load local data
        if (PlayerPrefs.HasKey(CONST.KEY_SAVE))
        {
            Utils.LOG("Load [LOCAL] save user data");
            LoadData();
        }
        else
        {
            Utils.LOG("Init [NEW] user data");
            InitNewUserData();
            SaveData();
        }
    }


    private void LoadData()
    {
        var data = PlayerPrefs.GetString(CONST.KEY_SAVE);
        userInfo = JsonUtility.FromJson<UserInfo>(data);
        // hasInitUserData = true;
    }


    private void SaveData()
    {
        var json = JsonUtility.ToJson(userInfo);
        PlayerPrefs.SetString(CONST.KEY_SAVE, json);
        print("save string data: " + json);
    }


    private void LoadLocalUserData()
    {
        userInfo = ConfigManager.Instance.UserInfo;
        // hasInitUserData = true;
    }


    private void InitNewUserData()
    {
        userInfo = new UserInfo();
        userInfo.displayName = "guest_000";
        userInfo.avatar = "actor";
        userInfo.gold = 0;
        userInfo.level = 1;
        userInfo.expLevel = 1;
        userInfo.maxExpLevel = 100;

        // init user actors
        userInfo.formation = new List<DataFormation>();
        userInfo.characters = ConfigManager.Instance.DefaultCharacters;
        userInfo.stages = ConfigManager.Instance.GameStages;

        for (int i = 0; i < CONST.DEFAULT_FORMATION; i++)
        {
            userInfo.formation.Add(new DataFormation
            {
                index = i,
                character = userInfo.characters[i]
            });
        }
    }


    public List<DataStage> GetStages()
    {
        return userInfo.stages;
    }


    public List<DataFormation> GetFormation()
    {
        // print("GetFormations: " + result.Count);
        return userInfo.formation;
    }


    public List<string> GetCharacters()
    {
        // print("GetCharacters: " + result.Count);
        return userInfo.characters;
    }


    public List<CharacterItem> GetCharacterFormation()
    {
        var character = ConfigManager.Instance.GameCharacters;
        var filter = userInfo.formation.Where(x => !string.IsNullOrEmpty(x.character));
        var list = character.Where(x => filter.Any(y => y.character.Equals(x.name)));

        // filter.ToList().ForEach(x => print(x.character));
        // list.ToList().ForEach(x => print(x.name));
        return list.ToList();
    }




    public void SaveInit(DataActor newActor)
    {
        // print($"SaveMainActor: {newActor.key}");
        // userInfo.mainActor = newActor;
        // userInfo.formation[1].actor = newActor;
        // userInfo.actors.AddIfIdNotExists(newActor);
        SaveData();
    }


    public void SaveStage(string key)
    {
        // print($"SaveStage: {data.id}");
        var stage = userInfo.stages.Find(x => x.key.Equals(key));
        stage.status = CONST.STAGE_COMPLETED;
        SaveData();
    }


    public void SaveCharacterByKey(string[] keys)
    {
        // print($"SaveStage: {keys.Count()}");
        userInfo.characters.AddRange(keys);
        SaveData();
    }


    public void RemoveFormationByKey(string key)
    {
        for (int i = 0; i < userInfo.formation.Count; i++)
        {
            if (string.IsNullOrEmpty(userInfo.formation[i].character))
                continue;

            if (userInfo.formation[i].character.Equals(key))
                userInfo.formation[i].character = "";
        }

        SaveData();
    }


    public void SaveFormationWithIndex(string key, int index)
    {
        userInfo.formation[index].character = key;
        SaveData();
    }



    public void CheatGetAllCharacters()
    {
        userInfo.stages.ForEach(x => x.status = CONST.STAGE_COMPLETED);
        userInfo.characters = ConfigManager.Instance.UserInfo.characters;
        SaveData();
    }


}