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

    protected void Awake()
    {
        m_agent = GetComponentInParent<Agent>();
    }


    public override void OnEntry()
    {
        m_goalIndex = CalculateEndGoal();
        CalculatePath();
        m_agent.animator.SetBool("Walk",true);
    }

    public override void OnExit()
    {
        PredictivePF.ResetPath();
        m_agent.animator.SetBool("Walk",false);
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

    private int CalculateEndGoal()
    {
        float minDistance = int.MaxValue;
        int index = 0;

        for(int i = 0; i < EndGoals.Length; i++)
        {
            Vector3 distanceToTarget = EndGoals[i].position - m_agent.transform.position;
            
            if(distanceToTarget.magnitude < minDistance)
            {
                minDistance = distanceToTarget.magnitude;
                index = i;
            }
        }

        return index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        List<Transform> segments = PredictivePF.Path.Segments;

        for(int i = 0; i < segments.Count; i++)
        {
            if(i!=segments.Count - 1)
            {
                Gizmos.DrawLine(segments[i].position,segments[i+1].position);
            }
            Gizmos.DrawWireSphere(segments[i].position, 0.5f);
        }
    }
}
