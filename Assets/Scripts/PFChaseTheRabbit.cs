using UnityEngine;

public class PFChaseTheRabbit : Seek
{

    public Path Path;
    public Agent ChaseTarget;
    public int pathOffset;
    public int currentParam;
    public int currentPos;

    public override SteeringOutput GetSteering(Agent character)
    {
        currentParam = Path.GetParam(character.Position, currentPos);

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
