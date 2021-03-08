using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls character movement. Based on MVC Demo & Unity Documentation: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
public class CharacterMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool canJump = false;
    private bool isJumping = false;
    private float jumpStartY; //the y-level of the player when they first start jumping
    private float jumpHeight; //how high the player is relative to jumpStartY

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxJumpHeight;

    private float gravity = 9.81f;

    //inputs
    private float xMove;
    private float yMove;
    private bool jumpStart;
    private bool jumping;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //player inputs
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jumpStart = Input.GetButton("Jump");
        jumping = Input.GetButton("Jump");

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(xMove, 0.0f, yMove);
            moveDirection *= speed;

            canJump = true;
            if (jumpStart)
            {
                isJumping = true;
                jumpStartY = transform.position.y;
            }
        }
        if (isJumping && canJump)
        {
            jumpHeight = transform.position.y - jumpStartY;
            if (jumping && jumpHeight < maxJumpHeight)
            {
                moveDirection = new Vector3(xMove, 0.0f, yMove);
                moveDirection *= speed;
                moveDirection.y = jumpSpeed;
            }
            else
            {
                canJump = false;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
