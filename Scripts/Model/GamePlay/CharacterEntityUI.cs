using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntityUI : MonoBehaviour
{

    [SerializeField] private CharacterEntity character;
    [SerializeField] private GameObject selectableObject;
    [SerializeField] private GameObject activatingObject;


    private void FixedUpdate()
    {
        if (character == null || character.IsDead || character.IsShowModel)
            return;

        if (selectableObject != null)
            selectableObject.SetActive(character.selectable);

        if (activatingObject != null)
            activatingObject.SetActive(character.IsActiveCharacter);
    }


}
