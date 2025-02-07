using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentState : MonoBehaviour
{
    public virtual void StartState() { }
    public virtual void UpdateState() { }
    public virtual void EndState() { }
}
