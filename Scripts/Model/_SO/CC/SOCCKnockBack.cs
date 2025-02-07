using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CC KnockBack", menuName = "CONFIG/CC/CC KnockBack", order = 202)]
public class SOCCKnockBack : SOCC
{


    [ContextMenu("Reload Type")]
    public void ReloadType()
    {
        // Debug.Log("ReloadType");
        type = CC.KnockBack;
    }


}
