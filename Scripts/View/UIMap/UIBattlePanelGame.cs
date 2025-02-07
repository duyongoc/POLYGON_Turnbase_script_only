using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattlePanelGame : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private List<UIItemActorIcon> ui_itemActor;


    // [properties]
    public List<UIItemActorIcon> UI_Actor { get => ui_itemActor; set => ui_itemActor = value; }


}
