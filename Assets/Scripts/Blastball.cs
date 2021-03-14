using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastball : MonoBehaviour
{
    public const int MaxIncrement = 5; //how many times the blastball must be passed before it blasts
    public Color minColor; //initial blastball color
    public Color maxColor; //the penultimate color of the blastball before it blasts
    public int currentTeam; //the team that is currently in possession of the blastball

    private BlastballState currentState;
    public BlastballStateIdle stateIdle = new BlastballStateIdle();
    public BlastballStateThrown stateThrown = new BlastballStateThrown();
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
        ChangeState(stateIdle);
    }

    void Update()
    {
        currentState.Update(this);
    }
}
