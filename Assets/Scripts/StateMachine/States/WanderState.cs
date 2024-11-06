using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{

    public Wander Wander;
    public LookWheUGoin LookWYG;
    public ObstacleAvoidance ObstacleAvoidance;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;

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

        SteeringOutput steeringOutput = Wander.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * Wander.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * Wander.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;

        steeringOutput = ObstacleAvoidance.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * ObstacleAvoidance.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * ObstacleAvoidance.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }
}
