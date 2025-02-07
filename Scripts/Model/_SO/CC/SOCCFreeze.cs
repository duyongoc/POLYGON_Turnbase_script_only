using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CC Freeze", menuName = "CONFIG/CC/CC Freeze", order = 201)]
public class SOCCFreeze : SOCC
{

    [ContextMenu("Reload Type")]
    public void ReloadType()
    {
        // Debug.Log("ReloadType");
        type = CC.Freeze;
    }

}
