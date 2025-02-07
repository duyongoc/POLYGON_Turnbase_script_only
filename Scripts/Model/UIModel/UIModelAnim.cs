using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelAnim : MonoBehaviour
{


    [Header("[Animation]")]
    [SerializeField] private Animator animator;



    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion


    public void TriggerAnim(string anim)
    {
        if (anim.Contains("normal"))
            animator.SetTrigger(ActorBase.ANIM_ATTACK_NORMAL);

        if (anim.Contains("skill_1"))
            animator.SetTrigger(ActorBase.ANIM_ATTACK_SKILL_1);

        if (anim.Contains("skill_2"))
            animator.SetTrigger(ActorBase.ANIM_ATTACK_SKILL_2);

        if (anim.Contains("skill_3"))
            animator.SetTrigger(ActorBase.ANIM_ATTACK_SKILL_3);
    }


}
