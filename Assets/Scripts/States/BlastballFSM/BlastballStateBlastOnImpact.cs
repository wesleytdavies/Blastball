using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastballStateBlastOnImpact : BlastballState
{
    public override void Enter(Blastball blastball) {
        Debug.Log("entering impact state");
    }
    public override void Update(Blastball blastball) {
        if (Physics.CheckSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2))
        {
            blastball.ChangeState(blastball.stateBlasting);
        }
    }
    public override void Leave(Blastball blastball) { }
}
