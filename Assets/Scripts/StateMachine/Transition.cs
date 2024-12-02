using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public State TargetState;
    protected StateMachine m_stateMachine;
    public abstract bool IsTriggered();
    public State GetTargetState()
    {
        return TargetState;
    }

    public abstract void OnTransition();

    public void SetStateMachine(StateMachine machine)
    {
        m_stateMachine = machine;
    }
}
