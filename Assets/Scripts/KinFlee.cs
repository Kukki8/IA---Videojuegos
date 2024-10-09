using UnityEngine;

public class KinFlee : Movement
{
    public GameObject Target;
    public float MaxSpeed;
    public float radius;

    private void Start()
    {
        IsKinematic = true; 
    }
    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = character.transform.position - Target.transform.position;

        // Radio exterior para dejar de huir
        if(result.Linear.magnitude > radius){
            return new SteeringOutput();
        }

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        character.Orientation = NewOrientation(result.Linear,character.Orientation);
        result.Angular = 0;
        return result;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Target.transform.position,radius);
    }
}
