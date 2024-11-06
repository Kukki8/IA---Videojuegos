using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public State TargetState;

    public abstract bool IsTriggered();
    public State GetTargetState()
    {
        return TargetState;
    }

    public abstract void OnTransition();
}
