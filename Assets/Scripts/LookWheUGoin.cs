using UnityEngine;

public class LookWheUGoin : Align
{
    Kinematic m_explicitTarget;

    protected override void Start()
    {
        m_explicitTarget = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        base.Start();
    }
    
    public override SteeringOutput GetSteering(Kinematic character)
    {
        Vector3 velocity = character.Velocity;
        if(velocity.magnitude == 0)
        {
            return new SteeringOutput();
        }

        Project(m_target);
        m_target = m_explicitTarget;

        m_target.Orientation = Mathf.Atan2(velocity.x, velocity.z);

        return base.GetSteering(character);
    }

        private void Project(Kinematic target)
    {
        m_explicitTarget.Position = target.Position;
        m_explicitTarget.Orientation = target.Orientation;
        m_explicitTarget.Velocity = target.Velocity;
        m_explicitTarget.Rotation = target.Rotation;
    }
}
