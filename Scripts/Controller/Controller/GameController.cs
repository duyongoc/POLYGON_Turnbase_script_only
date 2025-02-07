using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : Singleton<GameController>
{


    [Header("[Setting]")]
    public Color ColorBot = Color.red;
    public Color colorPlayer = Color.blue;



    // public GameObject GetModelByKey(string key)
    // {
    //     if (string.IsNullOrEmpty(key))
    //     {
    //         Debug.LogError($"GetModelByKey is null");
    //         return null;
    //     }

    //     var path = Path.Combine("Model", key);
    //     var character = Resources.Load<GameObject>(path);
    //     // print($"value: {value} | path: {path} | character: {character}");
    //     return character;
    // }


    // public GameObject GetActorByKey(string key)
    // {
    //     if (string.IsNullOrEmpty(key))
    //     {
    //         Debug.LogError($"GetActorByKey is null");
    //         return null;
    //     }

    //     var path = Path.Combine("Actor", key);
    //     var character = Resources.Load<GameObject>(path);
    //     if (character == null)
    //     {
    //         Debug.LogError($"GetActorByKey: {key} is null.");
    //         return null;
    //     }

    //     // print($"value: {key} | path: {path} | character: {character}");
    //     return character;
    // }



    public Sprite LoadSpriteCharacter(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError($"LoadSpriteCharacter is null: " + value);
            return null;
        }

        var path = Path.Combine("Hero", value.ToLower());
        var sprite = Resources.Load<Sprite>(path);

        // print($"value: {value} | path: {path} | sprite: {sprite}");
        return sprite;
    }


    public Sprite LoadSpriteActorSkill(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError($"LoadSpriteCharacterSkill is null: " + value);
            return null;
        }

        var path = Path.Combine("ActorSkill", value);
        var sprite = Resources.Load<Sprite>(path);

        // print($"value: {value} | path: {path} | sprite: {sprite}");
        return sprite;
    }

    
    // public Sprite LoadSpriteActorType(string value)
    // {
    //     if (string.IsNullOrEmpty(value))
    //     {
    //         Debug.LogError($"LoadSpriteActorType is null: " + value);
    //         return null;
    //     }
    //     var path = Path.Combine("ActorType", value);
    //     var sprite = Resources.Load<Sprite>(path);
    //     // print($"value: {value} | path: {path} | sprite: {sprite}");
    //     return sprite;
    // }



}
