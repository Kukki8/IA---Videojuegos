using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public struct SteeringOutput
{
    public Vector3 Linear;
    public float Angular;
}
public class Agent : MonoBehaviour
{
    public Animator animator = null;
    public Movement Behaviour;
    public float MaxSpeed;
    public bool Stop;
    private Vector3 m_position;
    private float m_orientation;
    private Vector3 m_velocity;
    private float m_rotation;

    SteeringOutput m_steeringOutput;

    // Campos/propiedades especiales (intermediario)
    public Vector3 Position {
        get { return m_position; }
        set {m_position = value;}
    }

    public Vector3 Velocity {
        get { return m_velocity; }
        set {m_velocity = value;}
    }

    public float Rotation{
        get { return m_rotation; }
        set {m_rotation = value;}
    }
    
    public float Orientation {
        get { return m_orientation; }
        set {m_orientation = value;}
    }

    void Start()
    {
        m_position = transform.position; 
        m_orientation = transform.eulerAngles.y*Mathf.Deg2Rad;
    }

    void Update()
    {
        m_position += m_velocity*Time.deltaTime; 

        //Se debe cancelar la Y, ya que solo estamos trabajando en 2 dimensiones :3
        m_position.y = 0;
        m_orientation += m_rotation*Time.deltaTime;
        transform.position = m_position;
        transform.eulerAngles = new Vector3(0,m_orientation*Mathf.Rad2Deg,0);

        if(Behaviour == null){

            return;
        }

        m_steeringOutput = Behaviour.GetSteering(this);

        m_velocity += m_steeringOutput.Linear*Time.deltaTime;
        m_rotation += m_steeringOutput.Angular*Time.deltaTime;

        if(m_velocity != Vector3.zero)
        {
            animator.SetBool("Walk",true);
        }

        if(Stop){

            if(m_steeringOutput.Linear == Vector3.zero){
                m_velocity = Vector3.zero;
                animator.SetBool("Walk",false);
            }
            if(m_steeringOutput.Angular == 0){
                m_rotation = 0;
            }
            
        }

        if(m_velocity.magnitude > MaxSpeed && !Behaviour.IsKinematic)
        {
            m_velocity.Normalize();
            m_velocity *= MaxSpeed;
        }
    }

    //Solo por temas de visualizacion :3
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.forward * 5 + transform.position);
    }
}

