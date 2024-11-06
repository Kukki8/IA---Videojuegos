using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] Segments;
    public int GetParam(Vector3 position, int lastParam)
    {
        int index = -1;
        float distance = int.MaxValue;

        for(int i = 0; i < Segments.Length; i++)
        {
            Vector3 direction = Segments[i].position - position;
            if(direction.magnitude < distance)
            {
                index = i;
                distance = direction.magnitude;
            }
        }
        return index;
    }
    public Vector3 GetPosition(int param)
    {
        if(param >= Segments.Length){
            return Vector3.zero;
        }
        return Segments[param].position;
    }

}
