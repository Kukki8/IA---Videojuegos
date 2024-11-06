using UnityEngine;

public class KinWander : Movement
{
    public float maxSpeed;
    public float maxRotation;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = maxSpeed*FacingDirection(character.Orientation);
        result.Linear.y = 0;

        result.Angular = RandomBinomial()*maxRotation;

        return result;

    }

}
