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
    [SerializeField] Transform waypointsParent;

    [Header("Attacking parameters")]
    [SerializeField] float attackRange = 5.0f;

    // Cached refs
    private Health health;  // actually unsure I will need this, tbd
    private Shooter shooter;
    private List<Transform> waypointTransforms;
    private int currentWaypointIndex;
    private Transform target;

    private void Start()
    {
        shooter = GetComponent<Shooter>();
        if (shooter == null) Debug.LogWarning($"{name} (enemy) has no shooter script!");

        if(waypointsParent != null)
        {
            GenerateWaypointList();
            currentWaypointIndex = 0;
            //transform.position = waypointTransforms[currentWaypointIndex].position;   // I think there is a race condition here (maybe it gets moved when the list is not yet generated!)
        }
    }

    private void GenerateWaypointList()
    {
        waypointTransforms = new List<Transform>();
        Transform[] allChildren = waypointsParent.GetComponentsInChildren<Transform>();
        foreach (var child in allChildren)
        {
            waypointTransforms.Add(child);
        }
    }

    private void Update()
    {
        // Logic:
        // - If player is in range, attack. 
        // - If suspicious, suspicion behaviour (leave for later, optional). & return?
        // - Else, if there is a waypoint path, patrol behaviour
        if (CheckIfPlayerInRange())
        {
            AttackBehaviour();
            return;
        }
        if (waypointsParent != null)
        {
            PatrolBehaviour();
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
                target = collider.transform;
                return true;
            }
        }
        return false;
    }

    private void AttackBehaviour()
    {
        // Rotate to look at player
        LookAt2D();

        // Shoot at intervals (rate is set in Shooter)
        shooter.Shoot();
    }

    private void PatrolBehaviour()
    {
        // Think of a way to avoid waypoint's index becoming out of range  
        int targetIndex;
        if (currentWaypointIndex >= waypointTransforms.Count - 1)
        {
            targetIndex = 0;
        }
        else
        {
            targetIndex = currentWaypointIndex + 1;
        }

        if (Vector2.Distance(transform.position, waypointTransforms[currentWaypointIndex + 1].position) > waypointTolerance)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointTransforms[targetIndex].position, waypointTolerance * Time.deltaTime);
        }
        else
        {
            currentWaypointIndex++;
        }
    }

    private void MoveToNextWaypoint()
    {

    }

    private void LookAt2D()
    {
        float angle = 0;
        Vector3 relative = transform.InverseTransformPoint(target.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
