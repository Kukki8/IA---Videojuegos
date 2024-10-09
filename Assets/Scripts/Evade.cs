using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee
{
    public float MaxPrediction;
    public Agent PursueTarget;
    public Agent ExplicitTarget;

    public override SteeringOutput GetSteering(Agent character)
    {
        Vector3 direction = PursueTarget.Position - character.Position;
        float distance = direction.magnitude;   

        float speed = character.Velocity.magnitude;   

        float prediction;
    
        if(speed <= distance / MaxPrediction)
        {
            prediction = MaxPrediction;
        }else
        {
            prediction = distance/speed;
        }

        Project(PursueTarget);
        base.Target = ExplicitTarget;
        base.Target.Position += PursueTarget.Velocity*prediction;

        return base.GetSteering(character);

    }

    private void Project(Agent target)
    {
        ExplicitTarget.Position = target.Position;
        ExplicitTarget.Velocity = target.Velocity;
        ExplicitTarget.Orientation = target.Orientation;
        ExplicitTarget.Rotation = target.Rotation;

    }
}
