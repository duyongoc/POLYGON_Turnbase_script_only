using UnityEngine;

[CreateAssetMenu(fileName = "Ranger", menuName = "CONFIG/Actor/Ranger/[Ranger]", order = 400)]
public class SOActorRanger : ScriptableObject
{

    [Header("[CONFIG]")]
    public DataActorStat levelStats;
    public DataActorStat defaultStats;


    [Header("[Skill]")]
    public SOSkillRanger attackNormal;
    public SOSkillRanger attackSkill_1;
    public SOSkillRanger attackSkill_2;
    public SOSkillRanger attackSkill_3;


    [Header("[Attack]")]
    public int priorityFocus = CONST.PRIORITY_DEFAULT;
    public float combatPoint = 20;
    public float attackRange = 2.5f;
    public float attackDetection = 10f;

}
