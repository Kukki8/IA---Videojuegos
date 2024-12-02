using UnityEngine;

public class TargetNear : Transition
{
    public LayerMask LayerM;
    public float Range;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int followed = m_stateMachine.QueryData("targetFollowed");
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0 && followed < 1)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        (TargetState as ChaseState).SetTarget(hitColliders[0].gameObject) ;
    }

}
