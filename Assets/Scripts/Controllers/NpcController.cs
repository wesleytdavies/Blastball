﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    public const float TurnTime = 5f;

    private Vector3 currentPosition;
    private Vector3 lastPosition;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        lastPosition = currentPosition;
    }

    public void GoToBall(GameObject ball)
    {
        agent.SetDestination(ball.transform.position);
        FaceTarget(ball.transform.position);
    }

    public void FaceTarget(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnTime);
    }
}
