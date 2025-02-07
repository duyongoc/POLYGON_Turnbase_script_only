using UnityEngine;

public class ActorMageAnimation : MonoBehaviour
{


    [SerializeField] private ActorMageAttack attack;



    public void OnTrigger_AttackNormal()
    {
        if (attack == null)
            return;

        attack.AttackNormal.Attack();
    }

    public void OnTrigger_AttackSkill_1()
    {
        if (attack == null)
            return;

        attack.AttackSkill_1.Attack();
    }

    public void OnTrigger_AttackSkill_2()
    {
        if (attack == null)
            return;

        attack.AttackSkill_2.Attack();
    }

    public void OnTrigger_AttackSkill_3()
    {
        if (attack == null)
            return;

        attack.AttackSkill_3.Attack();
    }


    public void OnFinishAttack()
    {
        if (attack == null)
            return;

        attack.OnFinishAttack();
    }


}
