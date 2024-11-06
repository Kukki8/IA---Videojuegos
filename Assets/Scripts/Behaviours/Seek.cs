
public class Seek : Movement
{
    public float MaxAcceleration;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear = m_target.Position - character.Position;

        result.Linear.Normalize();

        result.Linear*= MaxAcceleration;
        
        result.Angular = 0;

        return result;
    }
}
