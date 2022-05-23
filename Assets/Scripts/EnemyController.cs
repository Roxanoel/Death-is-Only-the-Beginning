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
    [SerializeField] Transform waypointsParent;

    [Header("Attacking parameters")]
    [SerializeField] float attackRange = 5.0f;

    // Cached refs
    private Health health;  // actually unsure I will need this, tbd
    private Shooter shooter;
    private List<Transform> waypointTransforms;
    private int currentWaypointIndex;

    private void Start()
    {
        shooter = GetComponent<Shooter>();
        if (shooter == null) Debug.LogWarning($"{name} (enemy) has no shooter script!");

        if(waypointsParent != null)
        {
            GenerateWaypointList();
            currentWaypointIndex = 0;
            transform.position = waypointTransforms[currentWaypointIndex].position;
        }
    }

    private void GenerateWaypointList()
    {
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
            // Attack behaviour
            return;
        }
        if (waypointsParent != null)
        {
            // Patrol behaviour
        }
    }


    private bool CheckIfPlayerInRange()
    {
        Collider2D[] allHits = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.NameToLayer("Characters"));
        foreach (Collider2D collider in allHits)
        {
            if (collider.CompareTag("Player")) return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
