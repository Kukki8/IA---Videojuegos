using UnityEngine;

///////////////////////
////////REVISAR!!//////
///////////////////////

public class PredictivePF : Seek
{
    public Path Path;
    public Agent ChaseTarget;
    public int pathOffset;
    public int currentParam;
    public int currentPos;
    public float predictTime = 0.1f;

    public override SteeringOutput GetSteering(Agent character)
    {
        Vector3 futurePos = character.Position + character.Velocity * predictTime;
        
        currentParam = Path.GetParam(futurePos, currentPos);
        
        int targetParam = (currentParam + pathOffset)%Path.Segments.Length;

        Vector3 position = Path.GetPosition(targetParam);

        if(position == Vector3.zero)
        {
            position = ChaseTarget.Position;
        }

        ChaseTarget.Position = position;
        base.Target = ChaseTarget;

        return base.GetSteering(character);
    }
}
