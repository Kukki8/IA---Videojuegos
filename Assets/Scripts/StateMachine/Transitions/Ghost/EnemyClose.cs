using UnityEngine;

public class EnemyClose : Transition
{
    public LayerMask LayerM;
    public float Range;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int following = m_stateMachine.QueryData("FollowingEnemy");
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0 && following < 1)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        (TargetState as FleeState).SetTarget(hitColliders[0].gameObject) ;
    }

}
