using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VelocityMatching : Movement
{
    public float MaxAcceleration;
    float timeToTarget = 0.1f;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = m_target.Velocity - character.Velocity;
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
