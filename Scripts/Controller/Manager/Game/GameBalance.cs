using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;
// using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class GameBalance : MonoBehaviour
{


    // [Header("[Data From API]")]
    // [SerializeField] private List<CharacterStats> towerDataStats;
    // [SerializeField] private List<CharacterStats> warriorDataStats;
    // [SerializeField] private List<CharacterStats> huntressDataStats;
    // [SerializeField] private List<CharacterStats> succubusDataStats;
    // [SerializeField] private List<CharacterStats> knightLancerDataStats;


    // [Header("[Setting]")]
    // [SerializeField] private SOHeroWarrior[] soWarrior;
    // [SerializeField] private SOHeroHuntress[] soHuntress;
    // [SerializeField] private SOHeroSuccubus[] soSuccubus;
    // [SerializeField] private SOHeroKnightLancer[] soKnightLancer;
    // [SerializeField] private SOTowerBot[] soTowerBot;
    // [SerializeField] private SOTowerPlayer[] soTowerPlayer;



    // [private]
    // private GameNetwork network;


    // [properties]
    // public CharacterStats GetWarriorStats => soWarrior.defaultStats;
    // public CharacterStats GetHuntressStats => soHuntress.defaultStats;



    public void Init()
    {
        // network = GameManager.Instance.Network;
        // GetDataFromAPI();
    }


    // public async void GetDataFromAPI(Action callback = null)
    // {
    //     var t_tower = network.Task_GET_CharacterStats(CONST.API_GET_STATS_TOWER, (respones) => { towerDataStats = respones.data.ToList(); });
    //     var t_warrior = network.Task_GET_CharacterStats(CONST.API_GET_STATS_WARRIOR, (respones) => { warriorDataStats = respones.data.ToList(); });
    //     var t_archer = network.Task_GET_CharacterStats(CONST.API_GET_STATS_ARCHER, (respones) => { huntressDataStats = respones.data.ToList(); });
    //     var t_mage = network.Task_GET_CharacterStats(CONST.API_GET_STATS_MAGE, (respones) => { succubusDataStats = respones.data.ToList(); ; });
    //     var t_knightlancer = network.Task_GET_CharacterStats(CONST.API_GET_STATS_KNIGHT_LANCER, (respones) => { knightLancerDataStats = respones.data.ToList(); });
    //     await UniTask.WhenAll(t_tower, t_warrior, t_archer, t_mage, t_knightlancer);
    //     // load data and do something after that
    //     LoadStatsData();
    //     // doing after data load success
    //     callback?.Invoke();
    // }



    // public void LoadStatsData()
    // {
    //     SetDataTowerByIndex(0);
    //     SetDataWarriorByIndex(0);
    //     SetDataHuntressByIndex(0);
    //     SetDataSuccubusByIndex(0);
    //     SetDataKnightLancerByIndex(0);
    // }


    // public void SetDataTowerByIndex(int index)
    // {
    //     if (towerDataStats.Count <= 0)
    //     {
    //         Debug.LogError($"{CONST.API_GET_STATS_TOWER} is null");
    //         return;
    //     }
    //     foreach (var so in soTowerBot)
    //     {
    //         so.defaultStats = towerDataStats.ElementAt(index);
    //     }
    //     foreach (var so in soTowerPlayer)
    //     {
    //         so.defaultStats = towerDataStats.ElementAt(index);
    //     }
    // }


    // public void SetDataWarriorByIndex(int index)
    // {
    //     if (warriorDataStats.Count <= 0)
    //     {            
    //         Debug.LogError($"{CONST.API_GET_STATS_WARRIOR} is null");
    //         return;
    //     }
    //     foreach (var so in soWarrior)
    //     {
    //         so.defaultStats = warriorDataStats.ElementAt(index);
    //     }
    // }


    // public void SetDataHuntressByIndex(int index)
    // {
    //     if (huntressDataStats.Count <= 0)
    //     {
    //         Debug.LogError($"{CONST.API_GET_STATS_ARCHER} is null");
    //         return;
    //     }
    //     foreach (var so in soHuntress)
    //     {
    //         so.defaultStats = huntressDataStats.ElementAt(index);
    //     }
    // }


    // public void SetDataSuccubusByIndex(int index)
    // {
    //     if (succubusDataStats.Count <= 0)
    //     {
    //         Debug.LogError($"{CONST.API_GET_STATS_ARCHER} is null");
    //         return;
    //     }
    //     foreach (var so in soSuccubus)
    //     {
    //         so.defaultStats = succubusDataStats.ElementAt(index);
    //     }
    // }


    // public void SetDataKnightLancerByIndex(int index)
    // {
    //     if (knightLancerDataStats.Count <= 0)
    //     {
    //         Debug.LogError($"{CONST.API_GET_STATS_KNIGHT_LANCER} is null");
    //         return;
    //     }
    //     foreach (var so in soKnightLancer)
    //     {
    //         so.defaultStats = knightLancerDataStats.ElementAt(index);
    //     }
    // }


    // public CharacterStats GetWarriorStatsFromLevelIndex(int index)
    // {
    //     SetDataWarriorByIndex(index);
    //     var stats = warriorDataStats.ElementAt(index);
    //     if (stats == null)
    //     {
    //         Debug.LogError($"GetWarriorStatsFromLevel: {index} is null");
    //         return null;
    //     }
    //     return stats;
    // }


    // public CharacterStats GetHuntressStatsFromLevelIndex(int index)
    // {
    //     SetDataHuntressByIndex(index);
    //     var stats = huntressDataStats.ElementAt(index);
    //     if (stats == null)
    //     {
    //         Debug.LogError($"GetHuntressStatsFromLevel: {index} is null");
    //         return null;
    //     }
    //     return stats;
    // }


    // public CharacterStats GetSuccubusStatsFromLevelIndex(int index)
    // {
    //     SetDataSuccubusByIndex(index);
    //     var stats = succubusDataStats.ElementAt(index);
    //     if (stats == null)
    //     {
    //         Debug.LogError($"GetSuccubusStatsFromLevelIndex: {index} is null");
    //         return null;
    //     }
    //     return stats;
    // }


    // public CharacterStats GetKnightLancerStatsFromLevelIndex(int index)
    // {
    //     SetDataKnightLancerByIndex(index);
    //     var stats = knightLancerDataStats.ElementAt(index);
    //     if (stats == null)
    //     {
    //         Debug.LogError($"GetWarriorStatsFromLevel: {index} is null");
    //         return null;
    //     }
    //     return stats;
    // }


}
