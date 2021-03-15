using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateEmptyHanded : NpcState
{
    private CapsuleCollider npcCollider;
    private readonly Collider[] overlapBall = new Collider[1];
    private bool oldCheckCapsule;
    private GameObject closestBall;

    private LayerMask ballMask;

    public override void Enter(Npc npc)
    {
        Debug.Log("penis");
        npcCollider = npc.GetComponent<CapsuleCollider>();
        ballMask = LayerMask.GetMask("Ball"); //all balls must be on the Ball layer
        closestBall = npc.FindClosestBall(npc.transform.position);
        npc.GetComponent<NpcController>().GoToBall(closestBall);
        //npc.GetComponent<NpcController>().closestBall = closestBall;
    }
    public override void Update(Npc npc)
    {
        //OverlapCapsule calculations based on this: https://roundwide.com/physics-overlap-capsule/
        var center = npc.transform.TransformPoint(npcCollider.center);
        var size = new Vector3(npcCollider.radius, npcCollider.height, npcCollider.radius); //the code given in the above link for this line is WRONG! Don't convert to world position!
        var radius = size.x; //the CharacterController radius + skinWidth MUST be smaller than the CapsuleCollider radius or else it won't detect collisions!
        var height = size.y;
        var bottom = new Vector3(center.x, center.y - height / 2 + radius, center.z);
        var top = new Vector3(center.x, center.y + height / 2 - radius, center.z);
        //the following code works similarly to BlastballStateThrown - refer to that for explanation
        bool checkCapsule = Physics.CheckCapsule(top, bottom, radius, ballMask);
        Physics.OverlapCapsuleNonAlloc(top, bottom, radius, overlapBall, ballMask);
        if (!oldCheckCapsule && checkCapsule)
        {
            npc.npcBall = overlapBall[0].gameObject;
        }
        oldCheckCapsule = checkCapsule;

        //change state
        if (npc.npcBall != null)
        {
            npc.ChangeState(npc.stateHasBall);
        }
    }
    public override void Leave(Npc npc) { }
}
