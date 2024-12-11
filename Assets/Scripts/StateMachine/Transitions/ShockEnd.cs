using UnityEngine;

public class ShockEnd : Transition
{

    public float ShockedTime = 1.0f;
    private float m_timer;

    public override bool IsTriggered()
    {
        m_timer += Time.deltaTime;
        if(m_timer>= ShockedTime)
        {
            return true;
        }
        return false;
    }

    public override void OnTransition()
    {
        m_timer = 0;
    }


}
