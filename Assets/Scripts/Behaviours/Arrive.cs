using UnityEngine;

public class Arrive : Movement
{
    public float MaxAcceleration;
    public float MaxSpeed;
    public float targetRadius;
    public float slowRadius;
    private float timeToTarget = 0.1f;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        Vector3 direction = m_target.Position - character.Position;
        float distance = direction.magnitude;

        if(distance < targetRadius)
        {
            //Chequear: null check?
            return new SteeringOutput();
        }

        float targetSpeed = 0;

        if(distance > slowRadius)
        {
            targetSpeed = MaxSpeed;
        }else
        {
            targetSpeed = MaxSpeed*distance/slowRadius;
        }

        Vector3 targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        result.Linear = targetVelocity - character.Velocity;
        result.Linear /= timeToTarget;

        if(result.Linear.magnitude > MaxAcceleration)
        {
            result.Linear.Normalize();
            result.Linear *= MaxAcceleration;
        }

        result.Angular = 0;

        return result;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Target.transform.position,targetRadius);
        Gizmos.DrawWireSphere(Target.transform.position,slowRadius);
    }
}
