using UnityEngine;

public class Spotted : Transition
{
    public LayerMask LayerM;
    public float Range;
    private Collider[] hitColliders = new Collider[1];


    public override bool IsTriggered()
    {
        int numHit = Physics.OverlapSphereNonAlloc(transform.position, Range, hitColliders, LayerM);

        if(numHit > 0 )
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        if(m_stateMachine.QueryData("targetFollowed") != -1){

            m_stateMachine.UpdateData("targetFollowed",0);
        }

        if(m_stateMachine.QueryData("targetCopied") != -1){

            m_stateMachine.UpdateData("targetCopied",0);
        }
        if(m_stateMachine.QueryData("FollowingEnemy") != -1){

            m_stateMachine.UpdateData("FollowingEnemy",0);
        }
       
        
    }

}
