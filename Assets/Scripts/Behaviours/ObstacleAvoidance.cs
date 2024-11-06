using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    public float avoidDistance;
    public float lookahead;
    public LayerMask layerMask;

    protected Kinematic m_explicitTarget;

    protected override void Start()
    {
        m_explicitTarget = new Kinematic(Vector3.zero, 0, Vector3.zero, 0);
        base.Start();
    }

    public override SteeringOutput GetSteering(Kinematic character){

        Vector3 ray = character.Velocity.normalized;
        RaycastHit hit;
        // RaycastHit hit2;
        // RaycastHit hit3;

        if (Physics.Raycast(transform.position, ray, out hit, lookahead, layerMask)){ 

            Project(m_target);
            m_target = m_explicitTarget;
            m_target.Position = hit.point + hit.normal*avoidDistance;
            Debug.DrawRay(transform.position, ray * hit.distance, Color.yellow); 
        
            return base.GetSteering(character);
        }

        return new SteeringOutput();
    
    }

    private void Project(Kinematic target)
    {
        m_explicitTarget.Position = target.Position;
        m_explicitTarget.Orientation = target.Orientation;
        m_explicitTarget.Velocity = target.Velocity;
        m_explicitTarget.Rotation = target.Rotation;
    }


}
