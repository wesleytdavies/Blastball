using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHasBall : PlayerState
{
    public override void Enter(Player player) { }
    public override void Update(Player player) {
        player.playerBall.gameObject.transform.position = player.transform.position + player.transform.right * player.heldBallLocation.x + player.transform.up * player.heldBallLocation.y + player.transform.forward * player.heldBallLocation.z; //move the ball to the player's hand

        if (Input.GetButtonDown("Fire1"))
        {
            //throw the ball
            Vector3 viewDirection = Camera.main.transform.forward;
            viewDirection.Normalize();
        }
    }
    public override void Leave(Player player) {
        player.playerBall = null;
    }
}
