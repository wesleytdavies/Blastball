using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstballStateThrown : BurstballState
{
    private float blastRadius;
    private float blastMagnitude;
    private float blastLift; //how much the blast should lift objects

    public override void Enter(Burstball burstball) {
        blastRadius = 10f;
        blastMagnitude = 10f;
        blastLift = 0f;
    }
    public override void Update(Burstball burstball) {
        if (Physics.CheckSphere(burstball.transform.position, burstball.transform.lossyScale.x / 2))
        {
            burstball.GetComponent<Ball>().Blast(burstball.transform.position, blastRadius, blastMagnitude, blastLift);
        }
    }
    public override void Leave(Burstball burstball) { }
}
