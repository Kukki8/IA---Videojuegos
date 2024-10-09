using UnityEngine;

public class Wander : Face
{
    public float WanderOffset;
    public float WanderRadius;
    public float WanderRate;
    public float WanderOrientation;
    public float MaxAcceleration;

    public override SteeringOutput GetSteering(Agent character)
    {
        WanderOrientation += RandomBinomial()*WanderRate;

        float targetOrientation = WanderOrientation + character.Orientation;

        Vector3 center = character.Position + WanderOffset*FacingDirection(character.Orientation);

        center += WanderRadius*FacingDirection(targetOrientation);

        SteeringOutput result = base.GetSteering(character);

        result.Linear = MaxAcceleration * FacingDirection(character.Orientation);

        return result;
    }
}
