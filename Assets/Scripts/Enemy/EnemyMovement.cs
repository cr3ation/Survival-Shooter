using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    FantonHealth fantonHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;                              // Reference to the animator.
    bool goToTequila = false;
    public static float tequilaRange = 10;


    void Awake ()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag ("Fanton").transform;
        fantonHealth = player.GetComponent <FantonHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        GameObject tequila = GameObject.FindGameObjectWithTag("Tequila");
        if(tequila != null)
        {
            if(Vector3.Distance(tequila.transform.position, transform.position) < tequilaRange)
            {
                goToTequila = true;
                if(nav.enabled)
                    nav.SetDestination(tequila.transform.position);
            }
        }
        else
        {
            goToTequila = false;
        }

        if(enemyHealth.currentHealth > 0 && fantonHealth.currentHealth > 0 && nav.enabled && !goToTequila)
        {
            nav.SetDestination (player.position);
        }
        else if(enemyHealth.currentHealth <= 0 || fantonHealth.currentHealth <= 0)
        {
            nav.enabled = false;
        }

        if(nav.velocity == Vector3.zero)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
    }


}
