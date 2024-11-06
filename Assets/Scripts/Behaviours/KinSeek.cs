using UnityEngine;

public class KinSeek : Movement
{
    public float MaxSpeed;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = m_target.Position - character.Position;

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        character.Orientation = NewOrientation(result.Linear, character.Orientation);
        result.Angular = 0;
        
        return result;
    }
}
