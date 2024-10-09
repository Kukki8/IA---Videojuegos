using UnityEngine;

public class KinArrive : Movement
{
    public GameObject Target;
    public float MaxSpeed;

    public float radius;
    float TimeToTarget = 0.25f;

    private void Start()
    {
        IsKinematic = true; 
    }
    
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

        character.Orientation = NewOrientation(result.Linear, character.Orientation);
        result.Angular = 0;

        return result;
    }
}
