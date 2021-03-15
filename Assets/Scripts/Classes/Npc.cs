using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [HideInInspector] public string team; //TODO: strengthen this encapsulation

    [HideInInspector] public GameObject npcBall;
    public Vector3 heldBallLocation; //location of held ball relative to npc
    public const float minHeldTime = 5f; //least amount of time enemy can hold onto their ball. Change this value to change difficulty
    public const float maxHeldTime = 10f; //least amount of time enemy can hold onto their ball. Change this value to change difficulty
    //[SerializeField] private Color blueTeamColor;
    //[SerializeField] private Color yellowTeamColor;

    #region FSM
    private NpcState currentState;
    public NpcStateEmptyHanded stateEmptyHanded = new NpcStateEmptyHanded();
    public NpcStateHasBall stateHasBall = new NpcStateHasBall();
    #endregion

    private Renderer npcRenderer;

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

    void Awake()
    {
        team = tag;
        /*
        npcRenderer = GetComponent<Renderer>();
        if (team == "Blue Team") //TODO: use a dictionary to define team colors
        {
            npcRenderer.material.SetColor("_Color", blueTeamColor);
        }else if (team == "Yellow Team"){
            npcRenderer.material.SetColor("_Color", yellowTeamColor);
        }
        */
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

    public GameObject FindClosestTeammate(Vector3 currentPos, string team)
    {
        GameObject[] teammates;
        teammates = GameObject.FindGameObjectsWithTag(team);
        GameObject closestTeammate = null;
        float distance = Mathf.Infinity;
        foreach (GameObject teammate in teammates)
        {
            Vector3 distanceFromNpc = teammate.transform.position - currentPos;
            float ballDistance = distanceFromNpc.sqrMagnitude;
            if (ballDistance < distance)
            {
                closestTeammate = teammate;
                distance = ballDistance;
            }
        }
        return closestTeammate;
    }
}
