using UnityEngine;

public abstract class ActorState : MonoBehaviour
{
    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void EndState();
}
