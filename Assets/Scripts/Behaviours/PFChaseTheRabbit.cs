using UnityEngine;

public class PFChaseTheRabbit : Seek
{

    public Path Path;
    public int pathOffset;
    public int currentParam;
    public int currentPos;
    Kinematic m_explicitTarget;

    protected override void Start()
    {
        m_explicitTarget = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        base.Start();
    }

    public override SteeringOutput GetSteering(Kinematic character)
    {
        currentParam = Path.GetParam(character.Position, currentPos);

        int targetParam = (currentParam + pathOffset)%Path.Segments.Length;
        currentPos = targetParam;

        Vector3 position = Path.GetPosition(targetParam);

        if(position == Vector3.zero)
        {
            position = m_target.Position;
        }
        
        Project(m_target);
        m_target = m_explicitTarget;
        m_target.Position = position;
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
