using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SteeringOutput
{
    public Vector3 Linear;
    public float Angular;
}
public class Agent : MonoBehaviour
{
    public KinematicMovement Behaviour;

    Vector3 m_velocity;
    float m_rotation;
    SteeringOutput m_steeringOutput;

    // Update is called once per frame
    void Update()
    {
        transform.position += m_velocity*Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(m_rotation*Mathf.Rad2Deg*Time.deltaTime,Vector3.up);

        m_velocity += m_steeringOutput.Linear*Time.deltaTime;
        m_rotation += m_steeringOutput.Angular*Time.deltaTime;

        m_steeringOutput = Behaviour.GetSteering(this);

    }

    //Solo por temas de visualizacion :3
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.forward * 5 + transform.position);
    }
}

