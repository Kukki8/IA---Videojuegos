using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public PFChaseTheRabbit PathFollowing;
    public LookWheUGoin LookWYG;
    public float GoalDistance;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;
    private List<Transform> m_path;
    private GameObject m_target;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
    }

    public override void OnEntry()
    {
        m_agent.animator.SetBool("Walk",true);
        CalculatePath();
    }

    public override void OnExit()
    {
        PathFollowing.ResetPath();
        m_agent.animator.SetBool("Walk",false);
    }

    public override void OnUpdate()
    {
        if(DistanceToGoal() <= GoalDistance)
        {
            CalculatePath();
        }

        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        SteeringOutput steeringOutput = PathFollowing.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * PathFollowing.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * PathFollowing.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

    private void CalculatePath()
    {
        Node current = Graph.Instance.MapToNode(transform.position);
        Node end = Graph.Instance.MapToNode(m_target.transform.position);

        m_path = Graph.Instance.AStar(current,end, true);
        PathFollowing.SetPath(m_path);
    }

    private float DistanceToGoal()
    {
        return (m_agent.KinematicData.Position - PathFollowing.Path.Segments[^1].position).magnitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        List<Transform> segments = PathFollowing.Path.Segments;

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
