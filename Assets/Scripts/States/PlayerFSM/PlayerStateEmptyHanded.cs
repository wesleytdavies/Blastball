using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateEmptyHanded : PlayerState
{
    private CapsuleCollider capsuleCollider;
    private readonly Collider[] overlapBall = new Collider[1];
    private bool oldCheckCapsule;

    private LayerMask ballMask;

    public override void Enter(Player player) {
        capsuleCollider = player.GetComponent<CapsuleCollider>();
        ballMask = LayerMask.GetMask("Ball");
    }
    public override void Update(Player player) {
        //OverlapCapsule calculations based on this: https://roundwide.com/physics-overlap-capsule/
        var center = player.transform.TransformPoint(capsuleCollider.center);
        var size = player.transform.TransformVector(capsuleCollider.radius, capsuleCollider.height, capsuleCollider.radius);
        var radius = size.x; //the CharacterController radius MUST be smaller than the CapsuleCollider radius or else it won't detect collisions!
        var height = size.y;
        var bottom = new Vector3(center.x, center.y - height / 2 + radius, center.z);
        var top = new Vector3(center.x, center.y + height / 2 - radius, center.z);
        //the following code works similarly to BlastballStateThrown, refer to that for explanation
        bool checkCapsule = Physics.CheckCapsule(top, bottom, radius, ballMask);
        Physics.OverlapCapsuleNonAlloc(top, bottom, radius, overlapBall, ballMask);
        if (!oldCheckCapsule && checkCapsule)
        {
            player.playerBall = overlapBall[0].gameObject;
            if (overlapBall[0].gameObject.GetComponent<Blastball>() != null)
            {
                //player has the blastball
            }
        }
        oldCheckCapsule = checkCapsule;

        //change state
        if (player.playerBall != null)
        {
            player.ChangeState(player.stateHasBall);
        }
    }
    public override void Leave(Player player) { }
}
