using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int enemyDamage; // zombie damage
    public float attackInterval = 1f;
    public float moveSpeed = 3f; // zombie move speed
    public float detectionRadius = 50f; // zombie detection radius 
    public float attackRange = 3f;

    public Collider damageZone;

    private float lastAttackTime = 0f;
    private Rigidbody enemyRigidbody;
    private Transform playerTransform;


    public EnemyStatus currentEnemyStatus;

    private HealthController healthController;

    void Start()
    {
        // initializtion Rigidbody
        enemyRigidbody = GetComponent<Rigidbody>();

        healthController = GetComponent<HealthController>();

        // find tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("player with tag 'Player' not found");
        }

        currentEnemyStatus = EnemyStatus.Wait;

        healthController.OnDeath += HandleDeath;

        damageZone.enabled = false;
    }

    private void OnDestroy()
    {
        if (healthController != null)
            healthController.OnDeath -= HandleDeath;
    }

    private void FixedUpdate()
    {
        if (playerTransform == null || enemyRigidbody == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentEnemyStatus)
        {
            case EnemyStatus.Wait:
                HandleWaitState(distanceToPlayer);
                break;

            case EnemyStatus.Movement:
                HandleMovementState(distanceToPlayer);
                break;

            case EnemyStatus.Attack:
                HandleAttackState(distanceToPlayer);
                break;
        }
    }

    private void HandleWaitState(float distanceToPlayer)
    {
        if (distanceToPlayer <= detectionRadius)
        {
            currentEnemyStatus = EnemyStatus.Movement;
        }

        StopMovement();
    }

    private void HandleMovementState(float distanceToPlayer)
    {
        if (distanceToPlayer > detectionRadius)
        {
            currentEnemyStatus = EnemyStatus.Wait;
        }
        else
        {
            MoveTowardsPlayer();
        }

        if (distanceToPlayer <= attackRange)
        {
            currentEnemyStatus = EnemyStatus.Attack;
            StopMovement();
        }
    }


    private void HandleAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            currentEnemyStatus = EnemyStatus.Movement;
        }
        else
        {
            TryAttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        enemyRigidbody.velocity = directionToPlayer * moveSpeed;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    private void TryAttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackInterval)
        {
            lastAttackTime = Time.time;
            Debug.Log("Attack player");
            damageZone.enabled = true;
        }
        else
            damageZone.enabled = false;
    }
    private void DisableDamageZone()
    {
        if (damageZone != null)
        {
            damageZone.enabled = false;
        }
    }

    private void StopMovement()
    {
        enemyRigidbody.velocity = Vector3.zero;
    }

    private void HandleDeath()
    {
        SpawnManager.Instance.RemoveEnemy(gameObject);
    }
}

public enum EnemyStatus
{
    Wait,
    Movement,
    Attack
}
