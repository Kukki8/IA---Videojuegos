using UnityEngine;

public class Face : Align
{
    public Agent FaceTarget;
    public Agent ExplicitTarget;

    public override SteeringOutput GetSteering(Agent character)
    {
        Vector3 direction = FaceTarget.Position - character.Position;

        if(direction.magnitude == 0)
        {
            return new SteeringOutput();
        }

        Project(FaceTarget);
        base.Target = ExplicitTarget;
        base.Target.Orientation = Mathf.Atan2(direction.x, direction.z);
        
        return base.GetSteering(character);
    }

    private void Project(Agent target)
    {
        ExplicitTarget.Position = target.Position;
        ExplicitTarget.Velocity = target.Velocity;
        ExplicitTarget.Orientation = target.Orientation;
        ExplicitTarget.Rotation = target.Rotation;
    }
}
