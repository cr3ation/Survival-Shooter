using System.Collections.Generic;
using UnityEngine;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public float timeBetweenPunches = 0.15f;        // The time between each shot.
    public float range = 3f;                        // The distance the gun can fire.
    public float rotationRange = 4f;                // The distance to an enemy when player starts to rotate


    float timer;                                    // A timer to determine when to fire.
    float distanceToEnemy;                          // Distance to the closest enemy
    float rotationSpeed = 10f;                      // Speen in witch to rotate
    Animator anim;                                  // Reference to the anomator controller object
    LovisaMovement lovisaMovement;                  // Reference to the LovsaMovement object
    Rigidbody rigidBody;                            // Reference to the rigidBody object
    GameObject closestEnemy;                        // Refrencee to the closest enemy
    

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        anim = GetComponent<Animator>();
        lovisaMovement = GetComponent<LovisaMovement>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // Find closest enemy
        closestEnemy = FindClosestEnemy();
        
        // Distance to closest enemy
        distanceToEnemy = Vector3.Distance(closestEnemy.transform.position, transform.position);
        Debug.Log("Distance: " + distanceToEnemy);

        // Rotate player toward enemy if nessecery 
        Rotate();

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && distanceToEnemy <= range && timer >= timeBetweenPunches)
        {
            // ... punch the enemy.
            Shoot();
        }
        else if (Input.GetButton("Fire1"))
        {
            // Just do the move
            anim.SetBool("IsPunching", true);
        }
        if (!Input.GetButton("Fire1"))
        {
            anim.SetBool("IsPunching", false);
        }
    }

    void Shoot()
    {
        // Play the gun shot audioclip.
        // gunAudio.Play();
        // Reset the timer.
        timer = 0f;

        anim.SetBool("IsPunching", true);

        // Try and find an EnemyHealth script on the gameobject hit.
        EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();

        // If the EnemyHealth component exist...
        if (enemyHealth != null)
        {
            // ... the enemy should take damage.
            enemyHealth.TakeDamage(damagePerPunch, closestEnemy.transform.position);
        }
    }

    /// <summary>
    /// Rotates player towards the closest enemy if needed.
    /// </summary>
    void Rotate()
    {
        // If punching animation is activated. Rotate toward cloases enemy
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && distanceToEnemy <= rotationRange)
        {
            var lookPos = closestEnemy.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            rigidBody.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Find the name of the closest enemy
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            var curDistance = Vector3.Distance(go.transform.position, position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}