using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for checking collision only on enter from this: https://answers.unity.com/questions/1315660/is-there-a-way-for-physicschecksphere-to-have-onen.html
public class BlastballStateIncrementing : BlastballState
{
    private int increment; //how many times the blastball has hit something. Once it reaches maxIncrement, it explodes after the next hit
    bool oldCheckSphere;

    public override void Enter(Blastball blastball) {
        increment = 0;
    }
    public override void Update(Blastball blastball) {
        bool checkSphere = Physics.CheckSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2);
        if (oldCheckSphere == false && checkSphere == true) //OnEnter
        {
            increment++;
        }
        oldCheckSphere = checkSphere;
        if (increment >= VariableManager.maxIncrement)
        {
            blastball.ChangeState(blastball.stateBlastOnImpact);
        }
    }
    public override void Leave(Blastball blastball) { }
}
