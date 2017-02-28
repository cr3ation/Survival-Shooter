using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public float timeBetweenPunches = 0.2f;        // The time between each shot.
    public float range = 3f;                        // The distance the gun can fire.
    public float rotationRange = 4f;                // The distance to an enemy when player starts to rotate


    float punchTimer;                                    // A punchTimer to determine when to fire.
    float animationTimer;                               // A punchTimer to control the punch animations.
    //bool is_punching = false;                       // True during the punch
    float distanceToEnemy;                          // Distance to the closest enemy
    //float rotationSpeed = 10f;                      // Speen in witch to rotate
    Animator animator;                                  // Reference to the anomator controller object
    LovisaMovement lovisaMovement;                  // Reference to the LovsaMovement object
    Rigidbody rigidBody;                            // Reference to the rigidBody object
    GameObject closestEnemy;                        // Refrencee to the closest enemy
    

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        animator = GetComponent<Animator>();
        lovisaMovement = GetComponent<LovisaMovement>();
        rigidBody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        animationTimer = 1000;
        punchTimer = 1000;
    }

    void Update()
    {
        // Add the time since Update was last called to the punchTimer.
        punchTimer += Time.deltaTime;
        animationTimer += Time.deltaTime;
        
        // Find closest enemy
        closestEnemy = FindClosestEnemy();

        // Distance to closest enemy
        if (closestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(closestEnemy.transform.position, transform.position);
        }
        else
        {
            distanceToEnemy = 10000000f;
        }

        // If Lovisa is not punching at the moment, start punching.
        if(Input.GetButton("Fire1") && !animator.GetBool("IsPunching"))
        {
            Shoot();
            animator.SetBool("IsPunching", true);
            lovisaMovement.speed = 2f;
            punchTimer = 0;
            animationTimer = 0;
        }
        // If the punching-animation is running, only cause damage.
        else if(Input.GetButton("Fire1") && punchTimer >= timeBetweenPunches)
        {
            Shoot();
            // Reset the timer managing the damage.
            punchTimer = 0;
        }

        // If the animationTimer has been set to zero, fade in the punching animation.
        if (animationTimer < 0.25f)
        {
            animator.SetLayerWeight(1, Mathf.Lerp(0, 1, animationTimer * 5));
        }
        // If Lovisa is still punching, keep the opacity of the punching animation to 1 by resetting the animationTimer.
        else if(punchTimer < 0.25f)
        {
            animationTimer = 0.25f;
        }
        // If Lovisa is no longer punching, fade out the animation and reset her moving speed.
        else
        {
            animator.SetLayerWeight(1, Mathf.Lerp(1, 0, (animationTimer - 0.25f) * 5));
            animator.SetBool("IsPunching", false);
            lovisaMovement.speed = 6f;
        }

    }

    void Shoot()
    {

        if (distanceToEnemy <= range)
        {
            // Only do damage if the enemy is in front of the player. 
            Vector3 enemy_direction = Vector3.Normalize(closestEnemy.transform.position - rigidBody.transform.position);
            Vector3 player_direction = Vector3.Normalize(rigidBody.transform.forward);
            float angle = Mathf.Acos(Vector3.Dot(enemy_direction, player_direction));
            if (angle < 3.14 / 3.0)
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    Vector3 hitPoint = closestEnemy.transform.position;
                    if (closestEnemy.name == "Boss")
                        hitPoint.y = 2.5f;
                    else
                        hitPoint.y = 1.2f;
                    enemyHealth.TakeDamage(damagePerPunch, hitPoint);
                }
            }
        }
    }

    /// <summary>
    /// Rotates player towards the closest enemy if needed.
    /// </summary>
    /*void Rotate()
    {
        // If punching animation is activated. Rotate toward cloases enemy
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && distanceToEnemy <= rotationRange)
        {
            var lookPos = closestEnemy.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            rigidBody.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }*/

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