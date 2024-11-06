using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State InitialState;
    private State m_currentState;
    private Transition m_triggered;

    private void Start(){
        
        if(InitialState != null){
            m_currentState = InitialState;
        }
    }

    private void Update()
    {
        m_triggered = null;

        foreach(Transition t in m_currentState.GetTransition()){
            if(t.IsTriggered()){
                m_triggered = t;
                break;
            }
        }
        if(m_triggered != null){
            State targetedState = m_triggered.GetTargetState();

            m_currentState.OnExit();
            m_triggered.OnTransition();
            targetedState.OnEntry();

            m_currentState = targetedState;

        }
    
        m_currentState.OnUpdate();

    }
}
