using UnityEngine;

public class TargetOutOfRange : Transition
{
    public LayerMask LayerM;
    public float Range;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit < 1)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
    
    }

}
