using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "CONFIG/Actor/Hero/[Hero]", order = 100)]
public class SOActorHero : ScriptableObject
{
    
    [Header("[CONFIG]")]
    public DataActorStat levelStats;
    public DataActorStat defaultStats;


    [Header("[Skill]")]
    public SOSkillHero attackNormal;
    public SOSkillHero attackSkill_1;
    public SOSkillHero attackSkill_2;
    public SOSkillHero attackSkill_3;


    [Header("[Attack]")]
    public int priorityFocus = CONST.PRIORITY_DEFAULT;
    public float combatPoint = 20;
    public float attackRange = 2.5f;
    public float attackDetection = 10f;

}
