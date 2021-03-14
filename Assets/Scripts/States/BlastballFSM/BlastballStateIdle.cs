using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastballStateIdle : BlastballState
{
    public override void Enter(Blastball blastball) {
        Debug.Log("idle");
    }
    public override void Update(Blastball blastball) {

    }
    public override void Leave(Blastball blastball) { }
}
