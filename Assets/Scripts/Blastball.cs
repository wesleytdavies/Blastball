using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastball : MonoBehaviour
{
    public const float MaxIncrement = 5f; //how many times the blastball must be passed before it blasts
    public Color minColor; //initial blastball color
    public Color maxColor; //final blastball color
    [SerializeField] private float minSize; //initial blastball size
    [SerializeField] private float maxSize;  //final blastball size
    public Vector3 MinSizeVector
    {
        get
        {
            return _minSizeVector;
        }
        private set
        {
            _minSizeVector = value;
        }
    }
    private Vector3 _minSizeVector = Vector3.one;
    public Vector3 MaxSizeVector
    {
        get
        {
            return _maxSizeVector;
        }
        private set
        {
            _maxSizeVector = value;
        }
    }
    private Vector3 _maxSizeVector = Vector3.one;
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
        MinSizeVector *= minSize;
        MaxSizeVector *= maxSize;
        ChangeState(stateIdle);
    }

    void Update()
    {
        currentState.Update(this);
    }
}
