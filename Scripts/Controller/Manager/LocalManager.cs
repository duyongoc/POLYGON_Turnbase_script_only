using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalManager : Singleton<LocalManager>
{


    // [Header("[Setting Local]")]
    // [SerializeField] private bool runGameLocal;
    // [SerializeField] private PlayerInfoLocal playerInfoLocal;

    // [Header("[Local Load Stats]")]
    // [SerializeField] private ConfigCharacterStats configCharacterStats;

    // [Header("[Local Game Data]")]
    // [SerializeField] private SOGameData soGameData;


    // // [properties]
    // public bool RunGameLocal { get => runGameLocal; }
    // public PlayerInfoLocal PlayerInfoLocal { get => playerInfoLocal; }
    // public SOGameData Game { get => soGameData; }
    // public ConfigCharacterStats ConfigCharacterStats { get => configCharacterStats; }




    // #region UNITY
    // private void Start()
    // {
    //     Init();
    // }

    // // private void Update()
    // // {
    // // }
    // #endregion




    // public void Init()
    // {
    //     runGameLocal = GameManager.Instance.RunGameLocal;
    //     if (runGameLocal)
    //     {
    //         SetDefaultUserData();
    //     }
    // }



    // private void SetDefaultUserData()
    // {
    //     // print(PlayerPrefs.HasKey(CONST.KEY_SAVE));
    //     if (PlayerPrefs.HasKey(CONST.KEY_SAVE))
    //     {
    //         Utils.LOG("Load [LOCAL] save user data");
    //         LoadData();
    //         return;
    //     }

    //     Utils.LOG("Init [NEW] user data");
    //     InitUserData();
    //     SaveData();
    // }


    // private void LoadData()
    // {
    //     var data = PlayerPrefs.GetString(CONST.KEY_SAVE);
    //     playerInfoLocal = JsonUtility.FromJson<PlayerInfoLocal>(data);
    // }


    // private void SaveData()
    // {
    //     var json = JsonUtility.ToJson(PlayerInfoLocal);
    //     PlayerPrefs.SetString(CONST.KEY_SAVE, json);
    //     print("save string data: " + json);
    // }


    // private void InitUserData()
    // {
    //     // userData = new UserData();
    //     PlayerInfoLocal.displayName = "guest_000";
    //     PlayerInfoLocal.avatar = "warrior";
    //     PlayerInfoLocal.gold = 0;
    //     PlayerInfoLocal.ruby = 0;
    //     PlayerInfoLocal.level = 1;
    //     PlayerInfoLocal.expLevel = 1;
    //     PlayerInfoLocal.maxExpLevel = 100;

    //     // init user character
    //     // userData.cards = configMgr.Game.defaultCharacter;
    //     PlayerInfoLocal.cards = Game.gameCharacter;

    //     // init user item
    //     PlayerInfoLocal.items = Game.defaultItems;

    //     // init skill tree
    //     PlayerInfoLocal.skills = Game.gameSkillTree.Where(x => x.isActive == true).ToList();

    //     // init battle skill
    //     // userData.battleSkill = configMgr.Game.defaultSkillTree;

    //     // init battle card deck
    //     // userData.battleCard = LoadCardToBattleDefault();
    // }
    

    // public void SaveUserBattleSkill(List<DataSkillTree> data)
    // {
    //     if (data.Count > CONST.LIMIT_BATTLE_SKILL)
    //     {
    //         print($"Can't SaveUserBattleSkill more than {CONST.LIMIT_BATTLE_SKILL}");
    //         return;
    //     }

    //     playerInfoLocal.battleSkill = data;
    //     SaveData();
    // }


    // public void ChangeUserAvatar(string keyAvatar)
    // {
    //     playerInfoLocal.avatar = keyAvatar;
    // }


    // public void AddCoin(int value)
    // {
    //     playerInfoLocal.ruby += value;
    //     SaveData();
    // }


    // public void SetMissionHasFinished(string missionId)
    // {
    //     // var mission = soUserData.missions.Find(x => x.missionId.Equals(missionId));
    //     // if (mission != null)
    //     //     mission.isCompleted = true;
    //     SaveData();
    // }


    // public bool AddCardDeck(DataCharacter data)
    // {
    //     playerInfoLocal.battleCard.Add(data);
    //     SaveData();
    //     return true;
    // }


    // public void RemoveCardDeck(DataCharacter data)
    // {
    //     playerInfoLocal.battleCard.Remove(data);
    //     SaveData();
    // }



    // public bool CheckRunLocal_Login()
    // {
    //     if (runGameLocal)
    //     {
    //         UIGameScene.Instance.SetView(EViewGame.Lobby);
    //         return true;
    //     }

    //     return false;
    // }




#if UNITY_EDITOR
    [ContextMenu("Load")]
    private void Load()
    {
        // configCharacterStats.LoadStats();
    }
#endif


}
