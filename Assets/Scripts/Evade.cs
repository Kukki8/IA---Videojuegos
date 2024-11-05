using UnityEngine;

public class Evade : Flee
{
    public float MaxPrediction;
    Kinematic m_explicitTarget;

    protected override void Start()
    {
        m_explicitTarget = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        base.Start();
    }

    public override SteeringOutput GetSteering(Kinematic character)
    {
        Vector3 direction = m_target.Position - character.Position;
        float distance = direction.magnitude;   

        float speed = character.Velocity.magnitude;   

        float prediction;
    
        if(speed <= distance / MaxPrediction)
        {
            prediction = MaxPrediction;
        }else
        {
            prediction = distance/speed;
        }

        Project(m_target);
        m_target = m_explicitTarget;
        m_target.Position += m_target.Velocity * prediction;

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
