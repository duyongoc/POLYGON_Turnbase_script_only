using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterItem : BaseActorItem
{
    

    [Header("Character Data")]
    public BaseCharacterEntity model;
    public List<BaseSkill> skills;
    public CharacterItemEvolve evolveInfo;
    public List<BaseAttackAnimationData> attackAnimations;
    public CalculationAttributes calculationAttributes;


    public override SpecificItemEvolve GetSpecificItemEvolve()
    {
        return evolveInfo;
    }


#if UNITY_EDITOR
    public override BaseActorItem CreateEvolveItemAsset(CreateEvolveItemData createEvolveItemData)
    {
        var newItem = ScriptableObjectUtility.CreateAsset<CharacterItem>(name);
        newItem.attackAnimations = new List<BaseAttackAnimationData>(attackAnimations);
        newItem.skills = new List<BaseSkill>(skills);
        newItem.model = model;
        newItem.evolveInfo = (CharacterItemEvolve)evolveInfo.Clone();
        return newItem;
    }
#endif



    public void RandomAttribute()
    {
        calculationAttributes.hp = Random.Range(400, 600);
        calculationAttributes.pAtk = Random.Range(100, 200);
        calculationAttributes.pDef = Random.Range(50, 120);
        calculationAttributes.mAtk = Random.Range(100, 200);
        calculationAttributes.mDef = Random.Range(100, 200);
        calculationAttributes.spd = Random.Range(100, 200);
        calculationAttributes.eva = Random.Range(100, 200);
        calculationAttributes.acc = Random.Range(100, 150);
    }


}



[System.Serializable]
public class CharacterItemAmount : BaseItemAmount<CharacterItem> { }

[System.Serializable]
public class CharacterItemDrop : BaseItemDrop<CharacterItem> { }

[System.Serializable]
public class CharacterItemEvolve : SpecificItemEvolve<CharacterItem>
{
    public override SpecificItemEvolve<CharacterItem> Create()
    {
        return new CharacterItemEvolve();
    }
}
