using System;
using System.Collections.Generic;
using UnityEngine;

public class DataCharacter
{
}


public enum ERole
{
    BOT,
    PLAYER
}

public enum EActorType
{
    MELEE,
    RANGER,
    MAGE,
    HERO,
}


[Serializable]
public class DataActor
{
    public string id;
    public string key;
    public string name;
    public string type;
    public string description;
    public DataActorStat stats;
    public List<DataActorSkill> skills;
}

[Serializable]
public class DataActorStat
{
    public int level;
    public float hp;
    public float p_attack;
    public float p_defense;
    public float m_attack;
    public float m_defense;
    public float attack_speed;
    public float speed;
}

[Serializable]
public class DataActorSkill
{
    public string id;
    public string key;
    public string description;
}


[Serializable]
public class DataFormation
{
    public int index;
    public string character;
}


public static class ActorExtension
{
    public static void AddIfIdNotExists(this List<DataActor> list, DataActor value)
    {
        if (!list.Exists(x => x.id.Equals(value.id)))
        {
            list.Add(value);
        }
    }
}