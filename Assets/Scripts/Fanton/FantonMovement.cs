using UnityEngine;
using System.Collections;

public class FantonMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    FantonHealth fantonHealth;
    Animator anim;                      // Reference to the animator component.
    UnityEngine.AI.NavMeshAgent nav;

    float timer;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Fanton").transform;
        fantonHealth = player.GetComponent<FantonHealth>();

        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = 0;
    }

    void Start()
    {
        target = Waypoints.points[0];
    }


    void Update()
    {
        timer += Time.deltaTime;

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

        if(timer % 10 < 0.1)
            anim.SetInteger("walk_animation", (int)(Random.value * 6));
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

    // Move Fanton with the animation
    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
    }
}
