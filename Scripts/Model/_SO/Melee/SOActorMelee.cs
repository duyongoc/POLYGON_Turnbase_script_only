using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "CONFIG/Actor/Melee/[Melee]", order = 300)]
public class SOActorMelee : ScriptableObject
{
    
    [Header("[CONFIG]")]
    public DataActorStat levelStats;
    public DataActorStat defaultStats;


    [Header("[Skill]")]
    public SOSkillMelee attackNormal;
    public SOSkillMelee attackSkill_1;
    public SOSkillMelee attackSkill_2;
    public SOSkillMelee attackSkill_3;


    [Header("[Attack]")]
    public int priorityFocus = CONST.PRIORITY_DEFAULT;
    public float combatPoint = 20;
    public float attackRange = 2.5f;
    public float attackDetection = 10f;

}
