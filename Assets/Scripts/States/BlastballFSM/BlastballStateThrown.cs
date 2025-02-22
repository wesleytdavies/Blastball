﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for checking collision only on enter based on this: https://answers.unity.com/questions/1315660/is-there-a-way-for-physicschecksphere-to-have-onen.html
public class BlastballStateThrown : BlastballState
{
    private float increment; //how many times the blastball has hit something. Once it reaches MaxIncrement, it explodes after the next hit
    private float oldIncrement; //the increment of the blastball before being passed
    private bool oldCheckSphere; //previous blastball collision
    private readonly Collider[] overlapPlayer = new Collider[1]; //single index array for storing blastball/player collision
    private Collider oldPlayer; //previous player who had the blastball

    private Rigidbody rb;
    private LayerMask playerMask;
    private Renderer renderer;
    private Color minColor;
    private Color maxColor;
    private Vector3 minSize;
    private Vector3 maxSize;
    private float minMass;
    private float maxMass;

    public override void Enter(Blastball blastball) {
        rb = blastball.GetComponent<Rigidbody>();
        playerMask = LayerMask.GetMask("Player");
        renderer = blastball.GetComponent<Renderer>();
        minColor = blastball.minColor;
        maxColor = blastball.maxColor;
        minSize = blastball.MinSizeVector;
        maxSize = blastball.MaxSizeVector;
        minMass = blastball.MinMass;
        maxMass = blastball.MaxMass;
    }
    public override void Update(Blastball blastball) {
        //check collisions
        bool checkSphere = Physics.CheckSphere(blastball.transform.position, blastball.transform.lossyScale.x / 2, playerMask);
        Physics.OverlapSphereNonAlloc(blastball.transform.position, blastball.transform.lossyScale.x / 2, overlapPlayer, playerMask); //store the last player to have the blastball to prevent them from passing it to themselves
        //TODO: theoretically, there could be a *rare* bug if the ball collides with two players at the exact same time, so prioritize players of the same team as the thrower and then randomly pick between them, but doubtful this will happen often, if at all
        if (!oldCheckSphere && checkSphere && overlapPlayer[0] != oldPlayer) //check collision OnEnter and make sure the collision is with a new player
        {
            increment++; //upon being passed, the blastball increments
            Debug.Log("incrementing");
            renderer.material.SetColor("_Color", Color.Lerp(minColor, maxColor, increment / Blastball.MaxIncrement)); //lerp blastball color as it increments
            renderer.material.SetColor("_EmissionColor", Color.Lerp(minColor, maxColor, increment / Blastball.MaxIncrement)); //lerp blastball emission color as it increments
            blastball.transform.localScale = Vector3.Lerp(minSize, maxSize, increment / Blastball.MaxIncrement); //lerp blastball size as it increments
            rb.mass = Mathf.Lerp(minMass, maxMass, increment / Blastball.MaxIncrement);
            oldPlayer = overlapPlayer[0]; //set the player who has the ball as the old player
            Debug.Log(oldPlayer.gameObject);
        }
        oldCheckSphere = checkSphere;

        //change state
        if (increment >= Blastball.MaxIncrement)
        {
            blastball.ChangeState(blastball.stateBlastOnImpact);
        }else if (increment > oldIncrement)
        {
            blastball.ChangeState(blastball.stateIdle);
        }
    }
    public override void Leave(Blastball blastball) {
        oldIncrement = increment;
        Debug.Log(increment);
    }
}
