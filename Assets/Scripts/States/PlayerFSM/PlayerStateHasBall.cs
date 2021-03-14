using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHasBall : PlayerState
{
    private CapsuleCollider playerCollider;
    private SphereCollider ballCollider;
    private Rigidbody rbBall;
    private Transform cameraTransform;

    public override void Enter(Player player) {
        playerCollider = player.GetComponent<CapsuleCollider>();
        ballCollider = player.playerBall.GetComponent<SphereCollider>();
        ballCollider.enabled = false;//TODO: only ignore collisions with other players as well as other held burstballs, but not thrown burstballs
        rbBall = player.playerBall.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    public override void Update(Player player) {
        player.playerBall.gameObject.transform.position = player.transform.position + player.transform.right * player.heldBallLocation.x + player.transform.up * player.heldBallLocation.y + player.transform.forward * player.heldBallLocation.z; //move the ball to the player's hand

        if (Input.GetButtonDown("Fire1"))
        {
            //throw the ball
            Vector3 viewDirection = cameraTransform.forward;
            viewDirection.Normalize();
            rbBall.AddForce(viewDirection * 20, ForceMode.Impulse); //TODO: get rid of this magic number
            player.ChangeState(player.stateEmptyHanded);
        }
    }
    public override void Leave(Player player) {
        ballCollider.enabled = true; //TODO: see above
        player.playerBall = null;
    }
}
