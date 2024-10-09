using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Movement
{
    public Agent Target;
    public float MaxAcceleration;

    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = Target.Position - character.Position;

        result.Linear.Normalize();

        result.Linear*= MaxAcceleration;
        
        result.Angular = 0;

        return result;
    }
}
