using UnityEngine;

public class KinWander : Movement
{
    public float maxSpeed;
    public float maxRotation;

    private void Start()
    {
        IsKinematic = true; 
    }
    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = maxSpeed*FacingDirection(character.Orientation);
        result.Linear.y = 0;

        result.Angular = RandomBinomial()*maxRotation;

        return result;

    }

}
