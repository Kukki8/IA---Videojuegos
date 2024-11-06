using UnityEngine;

public class TargetNear : Transition
{
    public LayerMask LayerM;
    public float Range;

    public override bool IsTriggered()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range, LayerM);

        if(hitColliders.Length > 0)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        
    }

}
