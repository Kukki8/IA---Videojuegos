using UnityEngine;

public class Caught : Transition
{


    public override bool IsTriggered()
    {
        int following = m_stateMachine.QueryData("FollowingEnemy");

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
