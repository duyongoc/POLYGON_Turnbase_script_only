using UnityEngine;

public class CONST
{


    // key save user data save local
    public const string KEY_SAVE = "DATA";
    public const string KEY_SAVE_LOCAL = "DATA_LOCAL";
    public const string KEY_TUTORIAL = "TUTORIAL";


    // [default damage]
    public const int DEFAULT_DAMAGE = 10;
    public const int DEFAULT_FORMATION = 5;
    public const float DELAY_ATTACK = 0.5f;

    // [target priority]
    public const int PRIORITY_DEFAULT = 100;


    public const string ACTOR_FIGHTER = "fighter";
    public const string ACTOR_RANGER = "ranger";
    public const string ACTOR_TANKER = "tanker";
    public const string ACTOR_SUPPORT = "support";
    public const string ACTOR_MAGE = "mage";


    // public const string STAGE_DEFAULT = "default";
    public const string STAGE_COMPLETED = "completed";
    public const string STAGE_INCOMPLETED = "incompleted";


}



// [view game]
public enum EViewGame
{
    None,
    Loading,
    Selection,
    Information,
    Lobby,
    Stage,
    Map,
    Shop,
    Guild,
    Boss,
    BattlePass,
    Roulette,
}
