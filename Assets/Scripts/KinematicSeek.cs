using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : KinematicMovement
{
    public GameObject Target;
    public float MaxSpeed;


    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = Target.transform.position - character.transform.position;

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        result.Angular = NewOrientation(result.Linear);

        return result;
    }
}
