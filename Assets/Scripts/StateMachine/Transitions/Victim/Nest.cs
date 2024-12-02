using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Nest : Transition
{
    public LayerMask LayerM;
    public float Range;
    public float DistanceToNest = 0.5f;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0)
        {
            float distance = (hitColliders[0].transform.position - m_stateMachine.transform.position).magnitude;

            if(distance <= DistanceToNest)
            {
                return true;
            }
 
        }

        return false;
    }

    public override void OnTransition()
    {
       
    }

}
