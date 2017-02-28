using UnityEngine;
using System.Collections;

public class FantonMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    FantonHealth fantonHealth;
    Animator anim;                      // Reference to the animator component.
    int walkAnimation;
    int idleAnimation;
    UnityEngine.AI.NavMeshAgent nav;

    float timer;
    float previousTimer;

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

        // Randomize new walk and idle animations.
        if (timer % 4 < 0.1)
        {
            walkAnimation = (int)(Random.value * 4);
            idleAnimation = (int)(Random.value * 7);
        }

        anim.SetInteger("walkAnimation", walkAnimation);
        anim.SetInteger("idleAnimation", idleAnimation);

        // Blend in the Macarena animation layer.
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dizzy"))
        {
            if (timer > 10)
            {
                timer = 0;
                anim.SetTrigger("macarena");
            }
            anim.SetLayerWeight(1, Mathf.Lerp(0, 1, timer / 2));
            previousTimer = timer;
        }
        // Blend out the Macarena animation layer.
        else
        {
            anim.SetLayerWeight(1, Mathf.Lerp(1, 0, timer - previousTimer));
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

    // Move Fanton with the animation
    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
    }
}
