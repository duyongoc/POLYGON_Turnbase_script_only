using UnityEngine;

[CreateAssetMenu(fileName = "Skill Ranger", menuName = "CONFIG/Skill/Skill Ranger", order = 400)]
public class SOSkillRanger : ScriptableObject
{

    [Header("Attack")]
    public string skillName = "default";
    public float countdownTime = 10;
    public DataAttackRanger attack;
}
