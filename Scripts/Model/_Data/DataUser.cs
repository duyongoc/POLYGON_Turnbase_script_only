using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{

    public string userId;
    public string displayName;
    public string email;
    public string avatar;
    public float gold = 0;
    public float gem = 0;

    [Header("[Level]")]
    public float level = 1;
    public float expLevel = 0;
    public float maxExpLevel = 100;

    [Header("[Data List]")]
    public List<string> characters;
    public List<DataFormation> formation;
    public List<DataStage> stages;
    
}