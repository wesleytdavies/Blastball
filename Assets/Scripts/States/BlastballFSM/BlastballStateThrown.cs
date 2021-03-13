using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for checking collision only on enter based on this: https://answers.unity.com/questions/1315660/is-there-a-way-for-physicschecksphere-to-have-onen.html
public class BlastballStateThrown : BlastballState
{
    private int increment; //how many times the blastball has hit something. Once it reaches MaxIncrement, it explodes after the next hit
    private int oldIncrement; //the increment of the blastball before being passed
    private bool oldCheckSphere; //previous blastball collision
    private Collider oldPlayer; //previous player who had the blastball

    private LayerMask playerMask;
    private Renderer renderer;
    private Color minColor; //initial blastball color
    private Color maxColor;

    public override void Enter(Blastball blastball) {
        oldIncrement = increment;

        playerMask = LayerMask.GetMask("Player");
        renderer = blastball.GetComponent<Renderer>();
        minColor = renderer.material.color;
        maxColor = blastball.maxColor;
    }
    public override void Update(Blastball blastball) {


        bool checkSphere = Physics.CheckSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2, playerMask);
        Collider[] overlapPlayer = Physics.OverlapSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2, playerMask); //store the last player to have the blastball to prevent them from passing it to themselves
        //TODO: theoretically, there could be a *rare* bug if the ball collides with two players at the exact same time, so prioritize players of the same team as the thrower and then randomly pick between them, but doubtful this will happen often if at all
        if (oldCheckSphere == false && checkSphere == true && overlapPlayer[0] != oldPlayer) //check collision OnEnter and make sure the collision is with a new player
        {
            increment++; //upon being passed, the blastball increments
            renderer.material.color = Color.Lerp(minColor, maxColor, increment / Blastball.MaxIncrement); //lerp blastball color as it increments
            oldPlayer = overlapPlayer[0]; //set the player who has the ball as the old player
        }
        oldCheckSphere = checkSphere;

        if (increment >= Blastball.MaxIncrement)
        {
            blastball.ChangeState(blastball.stateBlastOnImpact);
        }else if (increment > oldIncrement)
        {
            blastball.ChangeState(blastball.stateIdle);
        }
    }
    public override void Leave(Blastball blastball) { }
}
