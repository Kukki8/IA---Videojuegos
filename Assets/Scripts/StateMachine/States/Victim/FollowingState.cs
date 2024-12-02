using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingState : State
{
    public Seek Seek;
    public LookWheUGoin LookWYG;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;
    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }

    public override void OnEntry()
    {
        m_agent.animator.SetBool("Walk",true);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        SteeringOutput steeringOutput = Seek.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * Seek.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * Seek.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    public void SetTarget(GameObject target)
    {
        Seek.Target = target.GetComponent<Agent>();
    }

}
