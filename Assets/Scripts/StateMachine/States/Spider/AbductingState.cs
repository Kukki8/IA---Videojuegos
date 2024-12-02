using System.Collections.Generic;
using UnityEngine;

public class AbductingState : State
{
    public PFChaseTheRabbit PredictivePF;
    public LookWheUGoin LookWYG;
    public Transform EndGoal;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }


    public override void OnEntry()
    {
    
        CalculatePath();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {

        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        SteeringOutput steeringOutput = PredictivePF.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * PredictivePF.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * PredictivePF.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    private void CalculatePath()
    {
        Node current = Graph.Instance.MapToNode(transform.position);
        Node end = Graph.Instance.MapToNode(EndGoal.position);

        List<Transform> path = Graph.Instance.AStar(current,end);
        PredictivePF.SetPath(path);
    }

}
