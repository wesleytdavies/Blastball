using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burstball : MonoBehaviour
{
    #region FSM
    private BurstballState currentState;
    public BurstballStateIdle stateIdle = new BurstballStateIdle();
    public BurstballStateThrown stateThrown = new BurstballStateThrown();
    #endregion

    public void ChangeState(BurstballState newState)
    {
        if (currentState != null)
        {
            currentState.Leave(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    void Start()
    {
        ChangeState(stateIdle);
    }

    void Update()
    {
        currentState.Update(this);
    }
}
