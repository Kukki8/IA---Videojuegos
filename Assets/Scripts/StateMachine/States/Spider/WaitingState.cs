
using UnityEngine;

public class WaitingState : State
{

    public Seek Seek;
    public LookWheUGoin LWYG;
    public Transform WaitPoint;
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;


    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
        
    }

    public override void OnEntry()
    {
        Seek.CreateTarget(WaitPoint);
        m_agent.animator.SetBool("Walk",false);
    }

    public override void OnExit()
    {
        m_agent.animator.SetBool("Walk",true);
    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        if(DistanceToGoal() > 0.3f)
        {

            SteeringOutput steeringOutput = Seek.GetSteering(m_agent.KinematicData);
            m_steeringOutput.Linear += steeringOutput.Linear * Seek.weight;
            m_steeringOutput.Angular += steeringOutput.Angular * Seek.weight;

            steeringOutput = LWYG.GetSteering(m_agent.KinematicData);
            m_steeringOutput.Linear += steeringOutput.Linear * LWYG.weight;
            m_steeringOutput.Angular += steeringOutput.Angular * LWYG.weight;
            
        }

        m_agent.SetSteeringOutput(m_steeringOutput);
    }

    private float DistanceToGoal()
    {
        return (m_agent.KinematicData.Position - WaitPoint.position).magnitude;
    }
}
