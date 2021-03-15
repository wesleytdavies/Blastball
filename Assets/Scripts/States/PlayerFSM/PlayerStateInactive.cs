using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInactive : PlayerState
{
    private float inactiveTime;

    private Rigidbody rb;

    public override void Enter(Player player) {
        player.GetComponent<CharacterController>().enabled = false;
        rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        inactiveTime = 0f;
    }
    public override void Update(Player player) {
        inactiveTime += Time.deltaTime;
        if (inactiveTime >= Player.maxInactiveTime)
        {
            player.ChangeState(player.stateEmptyHanded);
        }
    }
    public override void Leave(Player player) {
        rb.isKinematic = true;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
