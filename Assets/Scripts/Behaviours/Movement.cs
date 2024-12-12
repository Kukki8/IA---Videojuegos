using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public Agent Target;
    public bool IsKinematic;
    public float weight;

    protected Kinematic m_target;

    protected virtual void Start()
    {
        if(Target != null){
            m_target = Target.KinematicData;
        }else{
            m_target = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        }
        
    }

    protected void Update()
    {
        if(Target != null){
            m_target = Target.KinematicData;
        }
    }

    public abstract SteeringOutput GetSteering(Kinematic character);

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

    public void CreateTarget(Transform targetPos)
    {
        if(m_target == null)
        {
            m_target = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        }

        m_target.Position = targetPos.position;
        m_target.Orientation = targetPos.eulerAngles.y * Mathf.Deg2Rad;
    }
}
