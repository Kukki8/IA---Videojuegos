using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Path : MonoBehaviour
{
    public Transform[] Segments;
    public int GetParam(Vector3 position, int lastParam)
    {
        int index = 0;
        float distance = int.MaxValue;

        int current = lastParam%Segments.Length;
        Debug.Log("Inicial" + current);
        for(int i = 0; i < Segments.Length; i++)
        {
         
            if(current == lastParam){
                current = (current+1)%Segments.Length;
                continue;
            }
            Vector3 direction = Segments[current].position - position;
            if(direction.magnitude < distance)
            {
                index = current;
                distance = direction.magnitude;
            }
            current = (current+1)%Segments.Length;
            Debug.Log("Current" + current);
        }
        return index;
    }
    public Vector3 GetPosition(int param)
    {
        Debug.Log("Param" + param);
        if(param >= Segments.Length){
            return Segments[0].position;
        }
        return Segments[param].position;
    }

}
