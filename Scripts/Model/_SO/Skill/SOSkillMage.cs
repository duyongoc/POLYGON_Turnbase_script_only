using UnityEngine;

[CreateAssetMenu(fileName = "Skill Mage", menuName = "CONFIG/Skill/Skill Mage", order = 200)]
public class SOSkillMage : ScriptableObject
{

    [Header("Attack")]
    public string skillName = "default";
    public float countdownTime = 10;
    public DataAttackRanger attack;

}
