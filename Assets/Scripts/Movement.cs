using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public abstract SteeringOutput GetSteering(Agent character);
    public bool IsKinematic;
    public float NewOrientation(Vector3 velocity, float current)
    {

        if(velocity.magnitude > 0){
            return Mathf.Atan2(velocity.x, velocity.z);
        }

        return current;
    }

    public float RandomBinomial()
    {
        return Random.Range(0f,1f) - Random.Range(0f,1f);
    }

    public float MapToRange(float current, float target)
    {
        return Mathf.DeltaAngle(current*Mathf.Rad2Deg,target*Mathf.Rad2Deg)*Mathf.Deg2Rad;
    }

    public Vector3 FacingDirection(float orientation)
    {
        Vector3 direction = new Vector3(Mathf.Sin(orientation),0,Mathf.Cos(orientation));

        return direction;
    }

}
