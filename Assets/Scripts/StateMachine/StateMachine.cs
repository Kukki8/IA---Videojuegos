using System;
using System.Collections.Generic;
using UnityEngine;

public struct WorldData
{
    public GameObject target;
    public Dictionary<string,int> data;

    public WorldData(GameObject ntarget = null)
    {
        target = ntarget;
        data = new Dictionary<string, int>();
    }
}
public class StateMachine : MonoBehaviour
{
    public State InitialState;
    public List<State> States; 
    private State m_currentState;
    private Transition m_triggered;
    private WorldData m_worldData;

    private void Start(){

        Initialize();

        if(InitialState != null){
            m_currentState = InitialState;
            m_currentState.OnEntry();
        }
    }

    private void Update()
    {
        m_triggered = null;

        foreach(Transition t in m_currentState.GetTransition()){
            if(t.IsTriggered()){
                m_triggered = t;
                break;
            }
        }
        if(m_triggered != null){
            State targetedState = m_triggered.GetTargetState();
            
            m_currentState.OnExit();
            m_triggered.OnTransition();
            targetedState.OnEntry();

            m_currentState = targetedState;

        }
    
        m_currentState.OnUpdate();

    }

    public int QueryData(string key){

        if(m_worldData.data.ContainsKey(key)){
            return m_worldData.data[key];
        }

        return -1;
    }

    public void UpdateData(string key, int value)
    {
        m_worldData.data[key] = value;
    }

    private void Initialize()
    {
        m_worldData = new WorldData(null);

        for(int i = 0; i < States.Count;i++)
        {
            States[i].Initialize(this);
        }
    }

    public void SetTarget(GameObject target)
    {
        m_worldData.target = target;
    }

    public GameObject GetTarget()
    {
        return m_worldData.target;
    }
}
