using UnityEngine;

public class KinFlee : Movement
{
    public float MaxSpeed;
    public float radius;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = character.Position - m_target.Position;

        // Radio exterior para dejar de huir
        if(result.Linear.magnitude > radius){
            return new SteeringOutput();
        }

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        character.Orientation = NewOrientation(result.Linear, character.Orientation);
        result.Angular = 0;
        return result;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Target.transform.position,radius);
    }
}
