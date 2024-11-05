using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kinematic
{
    public Vector3 Position;
    public float Orientation;
    public Vector3 Velocity;
    public float Rotation;

    public Kinematic(Vector3 position, float orientation,Vector3 velocity, float rotation)
    {
        Position = position;
        Orientation = orientation;
        Velocity = velocity;
        Rotation = rotation;
    }
    
    public void Update(float deltaTime, SteeringOutput steeringOutput, float maxSpeed, bool isKinematic)
    {
        Position += Velocity * deltaTime;
        // Se debe cancelar la Y, ya que solo estamos trabajando en 2 dimensiones :3 uwu
        Position.y = 0;
        Orientation += Rotation * deltaTime;
        Velocity += steeringOutput.Linear * deltaTime; 
        Rotation += steeringOutput.Angular * deltaTime;

        if(Velocity.magnitude > maxSpeed && !isKinematic)
        {
            Velocity.Normalize();
            Velocity *= maxSpeed;
        }

    }
}
