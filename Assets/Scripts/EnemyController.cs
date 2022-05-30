using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages enemy behaviours (AI) such as patrolling, attacking, etc.
/// </summary>
public class EnemyController : MonoBehaviour
{
    // Config params
    [Header("Patrolling parameters")]
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] float waypointDwellTimeInSeconds = 1.0f;
    [SerializeField] float waypointTolerance = 0.25f;
    [SerializeField] Transform[] waypoints;

    [Header("Attacking parameters")]
    [SerializeField] float attackRange = 5.0f;
    [SerializeField] float aggroDurationInSeconds = 5.0f;

    // Cached refs
    private Health health;  // actually unsure I will need this, tbd
    private Shooter shooter;
    private int currentWaypointIndex;
    private Transform shootingTarget;
    private float dwellTimer = 0;
    private float aggroCountdownTimer = 0;
    private bool isAggro = false;

    private void Start()
    {
        shooter = GetComponent<Shooter>();
        if (shooter == null) Debug.LogWarning($"{name} (enemy) has no shooter script!");

        if(waypoints != null && waypoints.Length > 0)
        {
            currentWaypointIndex = 0;
            transform.position = waypoints[currentWaypointIndex].position;  
        }
    }

    private void Update()
    {
        // Logic:
        // - If player is in range, attack. 
        // - Else, if there is a waypoint path, patrol behaviour
        if (CheckIfPlayerInRange())
        {
            Aggravate();
        }
        if (isAggro == true)
        {
            AttackBehaviour();
        }
        if (waypoints.Length > 0 && !isAggro)
        {
            PatrolBehaviour();
        }
        UpdateAggroTimer();
    }

    private void UpdateAggroTimer()
    {
        if (aggroCountdownTimer > 0)
        {
            aggroCountdownTimer -= Time.deltaTime;
        }
        else
        {
            aggroCountdownTimer = 0;
            isAggro = false;
        }
    }

    private bool CheckIfPlayerInRange()
    {
        // TO DO: Add code to check if the player is in the line of sight! Might need to do another raycast in a line, order the hits, check if I hit a wall first.

        Collider2D[] allHits = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Characters"));
        foreach (Collider2D collider in allHits)
        {
            if (collider.CompareTag("Player"))
            {
                shootingTarget = collider.transform;
                return true;
            }
        }
        return false;
    }

    private void AttackBehaviour()
    {
        // Rotate to look at player
        LookAt2D(shootingTarget);

        // Shoot at intervals (rate is set in Shooter)
        shooter.Shoot();
    }

    private void PatrolBehaviour()
    {
        // Set target waypoint (by its index)
        int targetIndex;
        if (currentWaypointIndex >= waypoints.Length - 1)
        {
            targetIndex = 0;
        }
        else
        {
            targetIndex = currentWaypointIndex + 1;
        }

        // Make the enemy face the direction they are headed
        LookAt2D(waypoints[targetIndex]);

        if (Vector2.Distance(transform.position, waypoints[targetIndex].position) > waypointTolerance)
        {
            MoveToNextWaypoint(targetIndex);
        }
        else
        {
            // Increment dwell timer
            dwellTimer += Time.deltaTime;

            // If timer reaches configured dwelling duration, reset it to 0 and register the current index has changed
            if (dwellTimer >= waypointDwellTimeInSeconds)
            {
                dwellTimer = 0;
                currentWaypointIndex = targetIndex;
            }
        }
    }

    private void MoveToNextWaypoint(int targetIndex)
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[targetIndex].position, waypointTolerance * Time.deltaTime * movementSpeed);
    }

    private void Aggravate()
    {
        isAggro = true;
        aggroCountdownTimer = aggroDurationInSeconds;
    }

    private void LookAt2D(Transform target)
    {
        Vector3 relative = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
