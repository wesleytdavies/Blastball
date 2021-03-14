using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastball : MonoBehaviour
{
    public const float MaxIncrement = 5f; //how many times the blastball must be passed before it blasts
    public Color minColor; //initial blastball color
    public Color maxColor; //final blastball color
    [SerializeField] private float minSize; //initial blastball size
    [SerializeField] private float maxSize; //final blastball size
    [SerializeField] private float minMass; //initial blastball mass
    [SerializeField] private float maxMass; //final blastball mass

    #region getters/setters
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
    public float MinMass
    {
        get
        {
            return _minMass;
        }
        private set
        {
            _minMass = value;
        }
    }
    private float _minMass;
    public float MaxMass
    {
        get
        {
            return _maxMass;
        }
        private set
        {
            _maxMass = value;
        }
    }
    private float _maxMass;
    #endregion

    public int currentTeam; //the team that is currently in possession of the blastball

    private Rigidbody rb;

    #region FSM
    private BlastballState currentState;
    public BlastballStateIdle stateIdle = new BlastballStateIdle();
    public BlastballStateThrown stateThrown = new BlastballStateThrown();
    public BlastballStateBlastOnImpact stateBlastOnImpact = new BlastballStateBlastOnImpact();
    public BlastballStateBlasting stateBlasting = new BlastballStateBlasting();
    #endregion

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
        MinMass = minMass;
        MaxMass = maxMass;
        this.gameObject.transform.localScale = MinSizeVector;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.mass = MinMass;
        ChangeState(stateIdle);
    }

    void Update()
    {
        currentState.Update(this);
    }
}
