using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KinematicMovement : MonoBehaviour
{
    public abstract SteeringOutput GetSteering(Agent character);
    
    public float NewOrientation(Vector3 velocity)
    {
        Debug.Log(velocity.magnitude);

        if(velocity.magnitude > 0){
            return Mathf.Atan2(velocity.z, - velocity.x);
        }

        return 0;
    }

}
