using UnityEngine;

[CreateAssetMenu(fileName = "Skill Hero", menuName = "CONFIG/Skill/Skill Hero", order = 100)]
public class SOSkillHero : ScriptableObject
{

    [Header("Attack")]
    public string skillName = "default";
    public float countdownTime = 10;
    public DataAttackRanger attack;

}
