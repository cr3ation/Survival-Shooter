using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    FantonHealth fantonHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Fanton").transform;
        fantonHealth = player.GetComponent <FantonHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && fantonHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
