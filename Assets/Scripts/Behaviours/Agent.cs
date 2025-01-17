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
    public Animator animator = null;
    public Movement[] Behaviours;
    public float MaxSpeed;
    public bool Stop;
    public bool Input;
    public Kinematic KinematicData;
    public bool UsingStateMachine = false;

    SteeringOutput m_steeringOutput;

    bool m_isKinematic = false;
    
    void Start()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;
        KinematicData = new Kinematic(transform.position, transform.eulerAngles.y*Mathf.Deg2Rad, Vector3.zero, 0);
        Behaviours = GetComponents<Movement>();
    }

    public void Update()
    {
        
        if(!UsingStateMachine){
            UpdateSteering();
        }

        KinematicData.Update(Time.deltaTime, m_steeringOutput, MaxSpeed, m_isKinematic);
        transform.position = KinematicData.Position;
        transform.eulerAngles = new Vector3(0,KinematicData.Orientation*Mathf.Rad2Deg,0);

        if(Stop){

            if(m_steeringOutput.Linear == Vector3.zero){
                KinematicData.Velocity = Vector3.zero;
            }
            if(m_steeringOutput.Angular == 0){
                KinematicData.Rotation = 0;
            }
            
        }

    }

    private void UpdateSteering()
    {
        m_steeringOutput.Linear = Vector3.zero;
        m_steeringOutput.Angular = 0;

        for(int i = 0; i < Behaviours.Length; i++)
        {
            SteeringOutput steeringOutput = Behaviours[i].GetSteering(KinematicData);
            m_steeringOutput.Linear += steeringOutput.Linear * Behaviours[i].weight;
            m_steeringOutput.Angular += steeringOutput.Angular* Behaviours[i].weight;

            if(Behaviours[i].IsKinematic)
            {
                m_isKinematic = true;
            }
        }
    }

    public void SetSteeringOutput(SteeringOutput steeringOutput)
    {
        m_steeringOutput = steeringOutput;
    }






    //Solo por temas de visualizacion :3
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.forward * 5 + transform.position);
    }

}

