using UnityEngine;

public class LookWheUGoin : Align
{
    public override SteeringOutput GetSteering(Agent character)
    {
        Vector3 velocity = character.Velocity;
        if(velocity.magnitude == 0)
        {
            return new SteeringOutput();
        }

        Target.Orientation = Mathf.Atan2(velocity.x, velocity.z);

        return base.GetSteering(character);
    }
}
