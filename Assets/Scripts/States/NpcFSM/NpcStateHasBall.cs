using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateHasBall : NpcState
{
    private SphereCollider ballCollider;
    private float heldTime;
    private string team;
    private string opposingTeam;
    private GameObject closestTeammate;
    private float turnTime;

    public override void Enter(Npc npc)
    {
        ballCollider = npc.npcBall.GetComponent<SphereCollider>();
        ballCollider.enabled = false;//TODO: only ignore collisions with other npcs as well as other held burstballs, but not thrown burstballs
        heldTime = 0f;
        team = npc.team;
        opposingTeam = npc.opposingTeam;
        turnTime = 0f;
        if (npc.npcBall.GetComponent<Blastball>())
        {
            npc.npcBall.GetComponent<Blastball>().currentTeam = team;
        }
    }
    public override void Update(Npc npc)
    {
        heldTime += Time.deltaTime;
        npc.npcBall.transform.position = npc.transform.position + npc.transform.right * npc.heldBallLocation.x + npc.transform.up * npc.heldBallLocation.y + npc.transform.forward * npc.heldBallLocation.z; //move the ball to the npc's hand

        if (heldTime >= Random.Range(Npc.minHeldTime, Npc.maxHeldTime)) //throw the ball
        {
            if (npc.npcBall.GetComponent<Ball>().Type == Ball.BallType.Blastball)
            {
                closestTeammate = npc.FindClosestTeammate(npc.transform.position, team);
            } else if (npc.npcBall.GetComponent<Ball>().Type == Ball.BallType.Burstball)
            {
                closestTeammate = npc.FindClosestTeammate(npc.transform.position, opposingTeam);
            }
            npc.GetComponent<NpcController>().FaceTarget(closestTeammate.transform.position);
            turnTime++;
            if (turnTime >= NpcController.TurnTime)
            {
                Vector3 viewDirection = npc.transform.forward; //get the direction of the npc
                viewDirection.Normalize();
                viewDirection.y += 3f; //TODO: get rid of this magic number
                npc.npcBall.GetComponent<Ball>().ThrowBall(viewDirection); //throw ball in direction of the npc
                npc.ChangeState(npc.stateEmptyHanded); //change state
            }
        }
    }
    public override void Leave(Npc npc)
    {
        ballCollider.enabled = true; //TODO: see above
        npc.npcBall = null;
    }
}
