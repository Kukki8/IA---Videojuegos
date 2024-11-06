using System.Collections.Generic;
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

}
