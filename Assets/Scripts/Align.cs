using UnityEngine;


public class Align : Movement
{
    public float MaxAngularAcceleration;
    public float MaxRotation;
    public float targetRadius;
    public float slowRadius;
    private float timeToTarget = 0.1f;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        float rotation = m_target.Orientation - character.Orientation;

        rotation = MapToRange(character.Orientation, m_target.Orientation);
        float rotationSize = Mathf.Abs(rotation);

        if(rotationSize < targetRadius)
        {

            return new SteeringOutput();

        }

        float targetRotation = 0;

        if(rotationSize > slowRadius)
        {
            targetRotation = MaxRotation;
        }else
        {
            targetRotation = MaxRotation*rotationSize/slowRadius;
        }

        targetRotation *= rotation / rotationSize;

        result.Angular = targetRotation - character.Rotation;
        result.Angular /= timeToTarget;

        float angularAcceleration = Mathf.Abs(result.Angular);

        if(angularAcceleration > MaxAngularAcceleration)
        {
            result.Angular /= angularAcceleration;
            result.Angular *= MaxAngularAcceleration;
        }

        result.Linear = Vector3.zero;

        return result;

    }

    
}
