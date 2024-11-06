using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : Movement
{
    public float maxAcceleration;
    public Agent[] Targets;
    public float Threshold;
    public float DecayCoefficient;
    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;
        result.Linear = Vector3.zero;

        foreach(Agent target in Targets){

            Vector3 direction = character.Position - target.KinematicData.Position;
            float distance = direction.magnitude;

            if(distance < Threshold)
            {
                float strength = Math.Min(DecayCoefficient / (distance * distance),maxAcceleration);
                
                direction.Normalize();
                result.Linear += strength * direction;
            }
        }

        result.Angular = 0;
        return result;
    }
}
