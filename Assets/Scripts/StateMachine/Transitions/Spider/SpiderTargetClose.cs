using UnityEngine;

public class SpiderTargetClose : Transition
{
    public LayerMask LayerM;
    public float Range;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int followed = m_stateMachine.QueryData("targetCopied");
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0 && followed < 2)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        (TargetState as CopyState).SetTarget(hitColliders[0].gameObject);
    }

}
