
public class Flee : Movement
{
    public Agent Target;
    public float MaxAcceleration;
    public float radius;

    public override SteeringOutput GetSteering(Agent character)
    {
        SteeringOutput result;

        result.Linear =  character.Position - Target.Position;

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
