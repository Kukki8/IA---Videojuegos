using UnityEngine;

public class ShockedState : State
{
    private SteeringOutput m_steeringOutput;
    private Agent m_agent;

    protected void Start()
    {
        m_agent = GetComponentInParent<Agent>();
        
    }
    public override void OnEntry()
    {
        m_stateMachine.UpdateData("Shocked",1);
    }

    public override void OnExit()
    {
        m_stateMachine.UpdateData("Shocked",0);
    }

    public override void OnUpdate()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;


        m_agent.SetSteeringOutput(m_steeringOutput);
    }
}
