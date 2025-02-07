using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CC Slow", menuName = "CONFIG/CC/CC Slow", order = 204)]
public class SOCCSlow : SOCC
{


    [ContextMenu("Reload Type")]
    public void ReloadType()
    {
        // Debug.Log("ReloadType");
        type = CC.Slow;
    }


}
