using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KinematicArrive : KinematicMovement
{
    public GameObject Target;
    public float MaxSpeed;

    public float radius;
    float TimeToTarget = 0.25f;


    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = Target.transform.position - character.transform.position;

        if(result.Linear.magnitude < radius){
            return new SteeringOutput();
        }

        result.Linear /= TimeToTarget;

        if(result.Linear.magnitude > MaxSpeed){
            result.Linear.Normalize();
            result.Linear*= MaxSpeed;
        }

        result.Angular = NewOrientation(result.Linear);

        return result;
    }
}
