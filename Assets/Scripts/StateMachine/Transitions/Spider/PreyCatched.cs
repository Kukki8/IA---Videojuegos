using UnityEngine;

public class PreyCatched : Transition
{
    public LayerMask LayerM;
    public float Range;
    public GameObject FollowingTarget;

    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {

        int copied = m_stateMachine.QueryData("targetCopied");
        m_stateMachine.UpdateData("targetCopied", copied + 1);

        GameObject targetObject = hitColliders[0].gameObject;
        StateMachine targetMachine = targetObject.GetComponent<StateMachine>();
        targetMachine.UpdateData("FollowingEnemy",1);
        targetMachine.SetTarget(FollowingTarget);

    }

}
