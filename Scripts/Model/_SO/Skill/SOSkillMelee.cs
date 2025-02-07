using UnityEngine;

[CreateAssetMenu(fileName = "Skill Melee", menuName = "CONFIG/Skill/Skill Melee", order = 300)]
public class SOSkillMelee : ScriptableObject
{

    [Header("Attack")]
    public string skillName = "default";
    public float countdownTime = 10;
    public DataAttackMelee attack;

}
