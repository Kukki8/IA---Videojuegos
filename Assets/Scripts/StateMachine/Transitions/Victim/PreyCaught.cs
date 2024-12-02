using UnityEngine;

public class PreyCaught : Transition
{


    public override bool IsTriggered()
    {
        int following = m_stateMachine.QueryData("AbductedByEnemy");

        if(following == 1)
        {
            return true;
        }

        return false;
    }

    public override void OnTransition()
    {
        (TargetState as FollowingState).SetTarget(m_stateMachine.GetTarget()) ;
    }

}
