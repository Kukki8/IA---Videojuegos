using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{

    public PFChaseTheRabbit PathFollowing; 
    public LookWheUGoin LookWYG;
    public ObstacleAvoidance ObstacleAvoidance;
    public Transform Goal;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;

    protected void Awake()
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
        m_agent.animator.SetBool("Walk",false);
        PathFollowing.ResetPath();
    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        SteeringOutput steeringOutput = PathFollowing.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * PathFollowing.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * PathFollowing.weight;

        steeringOutput = LookWYG.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * LookWYG.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * LookWYG.weight;

        steeringOutput = ObstacleAvoidance.GetSteering(m_agent.KinematicData);
        m_steeringOutput.Linear += steeringOutput.Linear * ObstacleAvoidance.weight;
        m_steeringOutput.Angular += steeringOutput.Angular * ObstacleAvoidance.weight;
        
        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    private void CalculatePath()
    {
        Node current = Graph.Instance.MapToNode(transform.position);
        Node end = Graph.Instance.MapToNode(Goal.position);

        List<Transform> path = Graph.Instance.AStar(current,end);
        PathFollowing.SetPath(path);
    }
}
