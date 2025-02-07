using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemActorIcon : MonoBehaviour //, IActorHealth
{


    [Header("[Setting]")]
    [SerializeField] private Image imgAvatar;
    [SerializeField] private TMP_Text txtHealth;



    public void InitHealth(string key)
    {
        print("init key: " + key);
        imgAvatar.sprite = GameController.Instance.LoadSpriteCharacter(key);
    }


    public void RenderHealth(float currentHealth, float totalHealth)
    {
        txtHealth.SetText($"{(int)currentHealth}/{(int)totalHealth}");
    }


}
