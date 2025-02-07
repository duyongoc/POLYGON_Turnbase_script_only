using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGamePlayFormation : MonoBehaviour
{


    public Transform[] containers;
    public List<BaseCharacterEntity> Characters = new();
    public Transform helperContainer;
    // public Dictionary<int, BaseCharacterEntity> Characters = new Dictionary<int, BaseCharacterEntity>();



    public void SetFormationCharacters_Char()
    {
        // print("SetFormationCharacters_My");
        ClearCharacters();
        var formations = GameManager.Instance.Data.GetCharacterFormation();

        for (var i = 0; i < formations.Count; ++i)
        {
            // var character = PlayerItem.CreateActorItemWithLevel(formations[i], 1);
            // print("character: " + character);
            SetCharacter(i, formations[i]);
        }
    }


    public virtual void SetFormationCharacters()
    {
        var formationName = "STAGE_FORMATION_A";
        // var formationName = Player.CurrentPlayer.SelectedFormation;
        ClearCharacters();
        for (var i = 0; i < containers.Length; ++i)
        {
            PlayerFormation playerFormation = null;
            if (PlayerFormation.TryGetData(formationName, i, out playerFormation))
            {
                var itemId = playerFormation.ItemId;
                PlayerItem item = null;
                if (!string.IsNullOrEmpty(itemId) && PlayerItem.DataMap.TryGetValue(itemId, out item))
                    SetCharacter(i, item);
            }
        }
        
        if (BaseGamePlayManager.Helper != null &&
            !string.IsNullOrEmpty(BaseGamePlayManager.Helper.MainCharacter) &&
            GameInstance.GameDatabase.Items.ContainsKey(BaseGamePlayManager.Helper.MainCharacter) &&
            helperContainer != null)
        {
            var item = new PlayerItem();
            item.Id = "_Helper";
            item.DataId = BaseGamePlayManager.Helper.MainCharacter;
            item.Exp = BaseGamePlayManager.Helper.MainCharacterExp;
            SetHelperCharacter(item);
        }
    }

    // public virtual void SetCharacters(PlayerItem[] items)
    // {
    //     ClearCharacters();
    //     for (var i = 0; i < containers.Length; ++i)
    //     {
    //         if (items.Length <= i)
    //             break;

    //         var item = items[i];
    //         SetCharacter(i, item);
    //     }
    // }

    public virtual void SetCharacters_Char(CharacterItem[] array)
    {
        ClearCharacters();
        for (var i = 0; i < containers.Length; ++i)
        {
            if (array.Length <= i)
                break;

            var item = array[i];
            SetCharacter(i, item);
        }
    }


    public virtual BaseCharacterEntity SetCharacter(int position, CharacterItem data)
    {
        if (position < 0 || position >= containers.Length || data == null)
            return null;

        if (data.model == null)
        {
            Debug.LogWarning("Character's model is empty, this MUST be set");
            return null;
        }

        var container = containers[position];
        container.RemoveAllChildren();

        var character = Instantiate(data.model);
        character.SetFormation(this, position, container);
        character.Init(data);
        // print("item: " + item);

        // render ui
        var uiStats = Instantiate(GamePlayManager.Singleton.uiCharacterStatsPrefab, GamePlayManager.Singleton.uiCharacterStatsContainer);
        uiStats.transform.localScale = Vector3.one;
        uiStats.character = character;
        character.GetComponent<CharacterEntity>().uiCharacterStats = uiStats;

        Characters.Add(character);
        return character;
    }


    public virtual BaseCharacterEntity SetCharacter(int position, PlayerItem item)
    {
        // print("setcharacter: " + item);
        if (position < 0 || position >= containers.Length || item == null || item.CharacterData == null)
            return null;

        if (item.CharacterData.model == null)
        {
            Debug.LogWarning("Character's model is empty, this MUST be set");
            return null;
        }

        var container = containers[position];
        container.RemoveAllChildren();

        var character = Instantiate(item.CharacterData.model);
        character.SetFormation(this, position, container);
        character.Init(item.CharacterData);

        Characters.Add(character);
        return character;
    }


    public virtual BaseCharacterEntity SetHelperCharacter(PlayerItem item)
    {
        if (helperContainer == null)
            return null;

        var position = containers.Length;
        if (item.CharacterData.model == null)
        {
            Debug.LogWarning("Character's model is empty, this MUST be set");
            return null;
        }

        var container = helperContainer;
        container.RemoveAllChildren();

        var character = Instantiate(item.CharacterData.model);
        character.SetFormation(this, position, container);
        character.Init(item.CharacterData);

        Characters[position] = character;
        return character;
    }


    public virtual void ClearCharacters()
    {
        foreach (var container in containers)
        {
            container.RemoveAllChildren();
        }

        if (helperContainer != null)
            helperContainer.RemoveAllChildren();

        Characters.Clear();
    }


}
