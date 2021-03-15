using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateHasBall : NpcState
{
    private SphereCollider ballCollider;
    private Transform cameraTransform;
    private float heldTime;
    private string team;
    private GameObject closestTeammate;
    private float turnTime;

    public override void Enter(Npc npc)
    {
        ballCollider = npc.npcBall.GetComponent<SphereCollider>();
        ballCollider.enabled = false;//TODO: only ignore collisions with other npcs as well as other held burstballs, but not thrown burstballs
        cameraTransform = Camera.main.transform;
        heldTime = 0f;
        team = npc.team;
        turnTime = 0f;
    }
    public override void Update(Npc npc)
    {
        heldTime += Time.deltaTime;
        npc.npcBall.transform.position = npc.transform.position + npc.transform.right * npc.heldBallLocation.x + npc.transform.up * npc.heldBallLocation.y + npc.transform.forward * npc.heldBallLocation.z; //move the ball to the npc's hand

        if (heldTime >= Random.Range(Npc.minHeldTime, Npc.maxHeldTime)) //throw the ball
        {
            closestTeammate = npc.FindClosestTeammate(npc.transform.position, team);
            npc.GetComponent<NpcController>().FaceTarget(closestTeammate.transform.position);
            turnTime++;
            if (turnTime >= NpcController.TurnTime)
            {
                npc.npcBall.GetComponent<Ball>().ThrowBall(npc.transform.forward); //throw ball in direction of the camera
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
