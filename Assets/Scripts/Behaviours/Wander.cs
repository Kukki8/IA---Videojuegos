using UnityEngine;

public class Wander : Face
{
    public float WanderOffset;
    public float WanderRadius;
    public float WanderRate;
    public float WanderOrientation;
    public float MaxAcceleration;

    protected override void Start()
    {
        m_explicitTarget = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        base.Start();
    }
    
    public override SteeringOutput GetSteering(Kinematic character)
    {
        WanderOrientation += RandomBinomial()*WanderRate;

        float targetOrientation = WanderOrientation + character.Orientation;

        Vector3 center = character.Position + WanderOffset*FacingDirection(character.Orientation);

        Project(m_target);
        m_target = m_explicitTarget;
        m_target.Position = center + WanderRadius*FacingDirection(targetOrientation);

        SteeringOutput result = base.GetSteering(character);

        result.Linear = MaxAcceleration * FacingDirection(character.Orientation);

        return result;
    }

    private void Project(Kinematic target)
    {
        m_explicitTarget.Position = target.Position;
        m_explicitTarget.Orientation = target.Orientation;
        m_explicitTarget.Velocity = target.Velocity;
        m_explicitTarget.Rotation = target.Rotation;
    }
}
