using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    UnityEngine.AI.NavMeshAgent nav;            // Reference to the NavMeshAgent connected to the enemy.
    Animator anim;                              // Reference to the animator component.
    GameObject fanton;                          // Reference to the player GameObject.
    FantonHealth fantonHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.
    float walk_speed;


    void Awake()
    {
        // Setting up the references.
        fanton = GameObject.FindGameObjectWithTag("Fanton");
        fantonHealth = fanton.GetComponent<FantonHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        walk_speed = nav.speed;
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == fanton)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == fanton)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        // If the player has zero or less health...
        if (fantonHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }

        if(timer > 2.5)
        {
            anim.SetBool("Punching", false);
            nav.speed = walk_speed;
        }

    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (fantonHealth.currentHealth > 0)
        {
            // ... damage the player.
            fantonHealth.TakeDamage(attackDamage);
        }

        anim.SetBool("Punching", true);
        if (this.name == "Boss")
            nav.speed = 0.1f;
    }
}