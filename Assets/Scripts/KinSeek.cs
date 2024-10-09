using UnityEngine;

public class KinSeek : Movement
{
    public GameObject Target;
    public float MaxSpeed;

    private void Start()
    {
        IsKinematic = true; 
    }
    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear = Target.transform.position - character.transform.position;

        result.Linear.Normalize();
        result.Linear*= MaxSpeed;

        character.Orientation = NewOrientation(result.Linear,character.Orientation);
        result.Angular = 0;
        
        return result;
    }
}
