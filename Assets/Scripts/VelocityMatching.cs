using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VelocityMatching : Movement
{
    public Agent Target;
    public float MaxAcceleration;
    float timeToTarget = 0.1f;

    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = Target.Velocity - character.Velocity;
        result.Linear /= timeToTarget;

        if(result.Linear.magnitude> MaxAcceleration)
        {
            result.Linear.Normalize();
            result.Linear *= MaxAcceleration;
        }

        result.Angular = 0;
        return result;

    }

}
