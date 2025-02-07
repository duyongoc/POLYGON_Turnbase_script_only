using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Default", menuName = "CONFIG/Stage/Stage Default", order = 200)]
public class SOStageConfig : ScriptableObject
{

    public ActorBase[] bots;
    public string[] rewards;

}
