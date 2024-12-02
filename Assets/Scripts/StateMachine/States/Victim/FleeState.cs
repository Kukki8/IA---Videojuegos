using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    public Flee Flee;
    public LookWheUGoin LookWYG;
    public float FleeRange;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;
    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }

    public override void OnEntry()
    {
        m_agent.animator.SetBool("Run",true);
    }

    public override void OnExit()
    {
        m_agent.animator.SetBool("Run",false);
    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        SteeringOutput steeringOutput = Flee.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * Flee.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * Flee.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    public void SetTarget(GameObject target)
    {
        Flee.Target = target.GetComponent<Agent>();
    }

}
