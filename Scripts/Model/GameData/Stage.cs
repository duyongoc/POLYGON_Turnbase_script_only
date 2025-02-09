﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageFoe
{
    public CharacterItem character;
    public int level;
}

[System.Serializable]
public class StageRandomFoe
{
    public StageFoe[] foes;
    public int randomWeight;
}

[System.Serializable]
public class StageWave
{
    public bool useRandomFoes;
    public StageFoe[] foes;
}

public class Stage : BaseStage
{

    public EnvironmentData environment;
    
    [Header("Battle")]
    public StageWave[] waves;
    public StageRandomFoe[] randomFoes;


    public StageRandomFoe RandomFoes()
    {
        var weight = new Dictionary<StageRandomFoe, int>();
        foreach (var randomFoe in randomFoes)
        {
            weight.Add(randomFoe, randomFoe.randomWeight);
        }
        return WeightedRandomizer.From(weight).TakeOne();
    }

    public override List<PlayerItem> GetCharacters()
    {
        var dict = new Dictionary<string, PlayerItem>();
        foreach (var randomFoe in randomFoes)
        {
            foreach (var foe in randomFoe.foes)
            {
                if (foe.character != null)
                {
                    var newEntry = PlayerItem.CreateActorItemWithLevel(foe.character, foe.level);
                    newEntry.Id = foe.character.Id + "_" + foe.level;
                    dict[foe.character.Id + "_" + foe.level] = newEntry;
                }
            }
        }
        foreach (var wave in waves)
        {
            if (wave.useRandomFoes)
                continue;

            var foes = wave.foes;
            foreach (var foe in foes)
            {
                var item = foe.character;
                if (item != null)
                {
                    var newEntry = PlayerItem.CreateActorItemWithLevel(item, foe.level);
                    newEntry.Id = foe.character.Id + "_" + foe.level;
                    dict[foe.character.Id + "_" + foe.level] = newEntry;
                }
            }
        }
        return new List<PlayerItem>(dict.Values);
    }
}
