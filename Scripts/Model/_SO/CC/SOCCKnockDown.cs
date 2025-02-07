using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CC KnockDown", menuName = "CONFIG/CC/CC KnockDown", order = 203)]
public class SOCCKnockDown : SOCC
{
    
    
    [Header("[Knockdown]")]
    public float forceKnockBack = 1;


    [ContextMenu("Reload Type")]
    public void ReloadType()
    {
        // Debug.Log("ReloadType");
        type = CC.KnockDown;
    }


}
