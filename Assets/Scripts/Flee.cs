
public class Flee : Movement
{
    public float MaxAcceleration;
    public float radius;

    public override SteeringOutput GetSteering(Kinematic character)
    {
        SteeringOutput result;

        result.Linear =  character.Position - m_target.Position;

        // Radio exterior para dejar de huir
        if(result.Linear.magnitude > radius){
            return new SteeringOutput();
        }

        result.Linear.Normalize();

        result.Linear*= MaxAcceleration;
        
        result.Angular = 0;

        return result;
    }
}
