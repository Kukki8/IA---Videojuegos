using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductedState : State
{

    private SteeringOutput m_steeringOutput;
    private Agent m_agent;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
        
    }

    public override void OnEntry()
    {
        m_agent.animator.SetBool("Walk",false);
        m_agent.animator.SetBool("Run",false);
    }

    public override void OnExit()
    {
    
    }

    public override void OnUpdate()
    {
        m_agent.SetSteeringOutput(m_steeringOutput);
    }
}
