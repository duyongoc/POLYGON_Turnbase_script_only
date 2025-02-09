﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterActionSkill : UICharacterAction
{


    public UISkill uiSkill;
    public CharacterSkill skill;
    public Text textRemainsTurns;
    public Image imageRemainsTurnsGage;
    public int skillIndex;


    private void Update()
    {
        if (skill == null)
            return;
        
        var rate = 1 - skill.GetCoolDownDurationRate();

        if (uiSkill != null)
            uiSkill.data = skill.Skill as Skill;

        if (textRemainsTurns != null)
            textRemainsTurns.text = skill.GetCoolDownDuration() <= 0 ? "" : skill.GetCoolDownDuration().ToString("N0");

        if (imageRemainsTurnsGage != null)
            imageRemainsTurnsGage.fillAmount = rate;
    }

    protected override void OnActionSelected()
    {
        ActionManager.ActiveCharacter.SetAction(skillIndex);
    }
}
