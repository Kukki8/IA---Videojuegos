using UnityEngine;

public abstract class State : MonoBehaviour
{

    public Transition[] Transitions;

    public virtual void OnUpdate()
    {

    }

    public abstract void OnEntry();

    public abstract void OnExit();

    public Transition[] GetTransition()
    {
        return Transitions;
    }

    public void Initialize(StateMachine machine)
    {
        for(int i = 0; i < Transitions.Length ; i++)
        {
            Transitions[i].SetStateMachine(machine);
        }
    }

}
