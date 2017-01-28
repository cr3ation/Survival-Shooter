using UnityEngine;
using System.Collections;

public class FantonMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    FantonHealth fantonHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Fanton").transform;
        fantonHealth = player.GetComponent<FantonHealth>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start()
    {
        target = Waypoints.points[0];
    }


    void Update()
    {
        if (fantonHealth.currentHealth > 0 && waypointIndex < Waypoints.points.Length)
        {
            nav.SetDestination(target.position);
            if (Vector3.Distance(transform.position, target.position) <= 2f)
            {
                GetNextWaypoint();
            }
        }
        else
        { 
            nav.enabled = false;
        }
    }

    void GetNextWaypoint()
    {
       if (waypointIndex < Waypoints.points.Length - 1)
       {
            waypointIndex++;
       }
        else
        {
            waypointIndex = 0;
        }
        target = Waypoints.points[waypointIndex];
    }
}
