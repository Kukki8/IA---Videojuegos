using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : KinematicMovement
{
    public GameObject Target;
    public float MaxSpeed;


    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = character.transform.position - Target.transform.position;

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        result.Angular = NewOrientation(result.Linear);

        return result;
    }
}
