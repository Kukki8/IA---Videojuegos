using UnityEngine;

public class KinArrive : Movement
{
    public float MaxSpeed;

    public float radius;
    float TimeToTarget = 0.25f;
    
    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = m_target.Position - character.Position;

        if(result.Linear.magnitude < radius){
            return new SteeringOutput();
        }

        result.Linear /= TimeToTarget;

        if(result.Linear.magnitude > MaxSpeed){
            result.Linear.Normalize();
            result.Linear*= MaxSpeed;
        }

        character.Orientation = NewOrientation(result.Linear, character.Orientation);
        result.Angular = 0;

        return result;
    }
}
