using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastballStateBlastOnImpact : BlastballState
{
    private float blastRadius;
    private float blastMagnitude;
    private float blastLift; //how much the blast should lift objects

    private LayerMask goalMask;

    public override void Enter(Blastball blastball) {
        blastRadius = 10f;
        blastMagnitude = 10f;
        blastLift = 0f;
        goalMask = LayerMask.GetMask("Goal");
    }
    public override void Update(Blastball blastball) {
        if (Physics.CheckSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2, goalMask))
        {
            if (blastball.currentTeam == "Blue Team")
            {
                ScoreManager.blueScore++;
            } else if (blastball.currentTeam == "Blue Team")
            {
                ScoreManager.yellowScore++;
            }
            blastball.GetComponent<Ball>().Blast(blastball.transform.position, blastRadius, blastMagnitude, blastLift);
        }
    }
    public override void Leave(Blastball blastball) { }
}
