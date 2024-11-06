using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{

    public List<Movement> Behaviours;
    protected Agent m_agent;
    protected SteeringOutput m_steeringOutput;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }

    public override void OnEntry()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        for(int i = 0; i < Behaviours.Count; i++)
        {
            SteeringOutput steeringOutput = Behaviours[i].GetSteering(m_agent.KinematicData);
            m_steeringOutput.Linear += steeringOutput.Linear;
            m_steeringOutput.Angular += steeringOutput.Angular;


        m_agent.SetSteeringOutput(m_steeringOutput);
        }
    }
}
