using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls character movement. Based on MVC Demo & Unity Documentation: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
public class CharacterControllerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    private bool isJumping = false;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private float gravity = 9.81f;

    //inputs
    private float xMove;
    private float yMove;
    private bool jump;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //player inputs
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(xMove, 0.0f, yMove);
            moveDirection *= speed;

            // Face in direction of movement.
            if (moveDirection.magnitude > float.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }

            if (jump)
            {
                moveDirection.y = jumpSpeed;
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
