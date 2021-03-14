using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHasBall : PlayerState
{
    public override void Enter(Player player) { }
    public override void Update(Player player) {
        if (Input.GetButtonDown("Fire1"))
        {
            //throw the ball
            Vector3 viewDir = Camera.main.transform.forward;
            viewDir.Normalize();
        }
    }
    public override void Leave(Player player) { }
}
