using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [HideInInspector] public GameObject npcBall;
    public Vector3 heldBallLocation; //location of held ball relative to npc
    public const float minHeldTime = 5f; //least amount of time enemy can hold onto their ball. Change this value to change difficulty
    public const float maxHeldTime = 10f; //least amount of time enemy can hold onto their ball. Change this value to change difficulty

    #region FSM
    private NpcState currentState;
    public NpcStateEmptyHanded stateEmptyHanded = new NpcStateEmptyHanded();
    public NpcStateHasBall stateHasBall = new NpcStateHasBall();
    #endregion

    public void ChangeState(NpcState newState)
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

    //finding closest gameObject based on this: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestBall(Vector3 currentPos)
    {
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closestBall = null;
        float distance = Mathf.Infinity;
        foreach (GameObject ball in balls)
        {
            Vector3 distanceFromNpc = ball.transform.position - currentPos;
            float ballDistance = distanceFromNpc.sqrMagnitude;
            if (ballDistance < distance)
            {
                closestBall = ball;
                distance = ballDistance;
            }
        }
        return closestBall;
    }
}
