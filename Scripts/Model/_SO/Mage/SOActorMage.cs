using UnityEngine;

[CreateAssetMenu(fileName = "Mage", menuName = "CONFIG/Actor/Mage/[Mage]", order = 200)]
public class SOActorMage : ScriptableObject
{
   
    [Header("[CONFIG]")]
    public DataActorStat levelStats;
    public DataActorStat defaultStats;


    [Header("[Skill]")]
    public SOSkillMage attackNormal;
    public SOSkillMage attackSkill_1;
    public SOSkillMage attackSkill_2;
    public SOSkillMage attackSkill_3;


    [Header("[Attack]")]
    public int priorityFocus = CONST.PRIORITY_DEFAULT;
    public float combatPoint = 20;
    public float attackRange = 2.5f;
    public float attackDetection = 10f;

}
