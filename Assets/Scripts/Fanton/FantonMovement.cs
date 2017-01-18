using UnityEngine;
using System.Collections;

public class FantonMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
