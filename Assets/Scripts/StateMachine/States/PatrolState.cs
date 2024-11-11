using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public PFChaseTheRabbit PredictivePF;
    public LookWheUGoin LookWYG;
    public Transform[] EndGoals;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;
    private int m_goalIndex;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }


    public override void OnEntry()
    {
        // To do: Calcular el end goal que este mas cerca para ir a ese...
        m_goalIndex = 0;
        CalculatePath();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if(DistanceToGoal() <= 1f)
        {
            m_goalIndex = (m_goalIndex + 1)%EndGoals.Length;
            CalculatePath();
        }
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

    private float DistanceToGoal()
    {
        return (m_agent.KinematicData.Position - PredictivePF.Path.Segments[^1].position).magnitude;
    }

    private void CalculatePath()
    {
        Node current = Graph.Instance.MapToNode(transform.position);
        Node end = Graph.Instance.MapToNode(EndGoals[m_goalIndex].position);

        List<Transform> path = Graph.Instance.AStar(current,end);
        PredictivePF.SetPath(path);
    }
}
