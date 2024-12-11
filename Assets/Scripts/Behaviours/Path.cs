using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> Segments;
    public float DistanceToNode = 0.8f;
    public int GetParam(Vector3 position, int lastParam)
    {
        
        if((Segments[lastParam].position - position).magnitude < DistanceToNode){
            return lastParam + 1;
        }

        return lastParam;
    }
    public Vector3 GetPosition(int param)
    {

        if(param >= Segments.Count){
            return Segments[0].position;
        }
        return Segments[param].position;
    }

    public void SetSegments(List<Transform> segments)
    {
        Segments = segments;
    }

    public void ResetSegments()
    {
        Segments.Clear();
    }  

}
