using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType { Blastball, Burstball };

public class Player : MonoBehaviour
{
    [HideInInspector] public GameObject playerBall;
    public Vector3 heldBallLocation; //location of held ball relative to player

    #region FSM
    private PlayerState currentState;
    public PlayerStateEmptyHanded stateEmptyHanded = new PlayerStateEmptyHanded();
    public PlayerStateHasBall stateHasBall = new PlayerStateHasBall();
    #endregion

    public void ChangeState(PlayerState newState)
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
        ChangeState(stateEmptyHanded);
    }

    void Update()
    {
        currentState.Update(this);
    }
}
