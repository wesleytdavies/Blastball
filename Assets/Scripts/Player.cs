using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerBall;
    public Vector3 heldBallLocation; //location of held ball relative to player

    private PlayerState currentState;
    public PlayerStateEmptyHanded stateEmptyHanded = new PlayerStateEmptyHanded();
    public PlayerStateHasBall stateHasBall = new PlayerStateHasBall();
    public PlayerStateInactive stateInactive = new PlayerStateInactive();

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
