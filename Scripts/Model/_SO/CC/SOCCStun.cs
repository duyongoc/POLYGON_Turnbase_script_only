using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CC Stun", menuName = "CONFIG/CC/CC Stun", order = 205)]
public class SOCCStun : SOCC
{
    

    [ContextMenu("Reload Type")]
    public void ReloadType()
    {
        // Debug.Log("ReloadType");
        type = CC.Stun;
    }


}
