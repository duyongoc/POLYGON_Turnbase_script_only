using UnityEngine;

public class ActorHeroAnimation : MonoBehaviour
{


    [SerializeField] private ActorHeroAttack attack;



    public void OnTrigger_AttackNormal()
    {
        if (attack == null)
            return;

        // print($"name: {attack.name} - OnTrigger_AttackNormal: ");
        attack.AttackNormal.Attack();
    }

    public void OnTrigger_AttackSkill_1()
    {
        if (attack == null)
            return;

        // print($"name: {attack.name} - OnTrigger_AttackSkill_1: ");
        attack.AttackSkill_1.Attack();
    }

    public void OnTrigger_AttackSkill_2()
    {
        if (attack == null)
            return;

        // print($"name: {attack.name} - OnTrigger_AttackSkill_2: ");
        attack.AttackSkill_2.Attack();
    }

    public void OnTrigger_AttackSkill_3()
    {
        if (attack == null)
            return;

        // print($"name: {attack.name} - OnTrigger_AttackSkill_3: ");
        attack.AttackSkill_3.Attack();
    }


    public void OnFinishAttack()
    {
        if (attack == null)
            return;

        attack.OnFinishAttack();
    }


}
