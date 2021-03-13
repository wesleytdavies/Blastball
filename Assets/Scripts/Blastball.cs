using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastball : MonoBehaviour
{
    private BlastballState currentState;
    public BlastballStateIncrementing stateIncrementing = new BlastballStateIncrementing();
    public BlastballStateBlastOnImpact stateBlastOnImpact = new BlastballStateBlastOnImpact();
    public BlastballStateBlasting stateBlasting = new BlastballStateBlasting();

    public void ChangeState(BlastballState newState)
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
        
    }

    void Update()
    {
        currentState.Update(this);
    }
}
