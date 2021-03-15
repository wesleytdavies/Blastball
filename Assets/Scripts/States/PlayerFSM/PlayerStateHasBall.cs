using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHasBall : PlayerState
{
    private SphereCollider ballCollider;
    private Transform cameraTransform;

    public override void Enter(Player player) {
        ballCollider = player.playerBall.GetComponent<SphereCollider>();
        ballCollider.enabled = false;//TODO: only ignore collisions with other players as well as other held burstballs, but not thrown burstballs
        cameraTransform = Camera.main.transform;
    }
    public override void Update(Player player) {
        player.playerBall.transform.position = player.transform.position + player.transform.right * player.heldBallLocation.x + player.transform.up * player.heldBallLocation.y + player.transform.forward * player.heldBallLocation.z; //move the ball to the player's hand

        if (Input.GetButtonDown("Fire1")) //throw the ball
        {
            Vector3 viewDirection = cameraTransform.forward; //get the direction of the camera
            viewDirection.Normalize();
            player.playerBall.GetComponent<Ball>().ThrowBall(viewDirection); //throw ball in direction of the camera
            player.ChangeState(player.stateEmptyHanded); //change state
        }
    }
    public override void Leave(Player player) {
        ballCollider.enabled = true; //TODO: see above
        player.playerBall = null;
    }
}
