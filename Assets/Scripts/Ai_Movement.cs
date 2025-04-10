using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Movement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 3.5f;
    [SerializeField] float stoppingDistance = 2f;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] float chaseDistance = 10f;
    [SerializeField] float attackDistance = 1f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float attackDuration = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackDamage = 10f;

    float lastAttackTime = 0f;

    private void Start()
    {
        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= chaseDistance && distanceToPlayer > attackDistance)
        {

            // Look at the player
            LookAtPlayer();
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }

        if (distanceToPlayer <= attackDistance)
        {
            // Stop the agent when within attack distance
            animator.SetBool("isWalking", false);
            agent.isStopped = true;
            Attack();
        }
    }

    private void LookAtPlayer()
    {
        //Look At the player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        lastAttackTime = Time.time;
        Debug.Log("Attacking player!");
        LookAtPlayer();
        animator.SetTrigger("Attack");
    }
}
