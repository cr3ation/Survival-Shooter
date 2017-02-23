using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public float timeBetweenPunches = 0.2f;        // The time between each shot.
    public float range = 3f;                        // The distance the gun can fire.
    public float rotationRange = 4f;                // The distance to an enemy when player starts to rotate


    float timer;                                    // A timer to determine when to fire.
    float anim_timer;                               // A timer to control the punch animations.
    bool is_punching = false;                       // True during the punch
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
        Cursor.visible = false;
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        anim_timer += Time.deltaTime;

        // Find closest enemy
        closestEnemy = FindClosestEnemy();

        // Distance to closest enemy
        if (closestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(closestEnemy.transform.position, transform.position);
            Debug.Log("Distance: " + distanceToEnemy);
        }
        else
        {
            distanceToEnemy = 10000000f;
        }

        /*
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
        }*/

        // If the punch-button is pressed and a punch is not performed at the moment
        if (Input.GetKey("space") && !is_punching)
        {
            Shoot(false);
            is_punching = true;
            lovisaMovement.speed = 2f;
            anim.SetBool("IsPunching", true);
        }
        // If a punching animation is already performed
        else if (Input.GetKey("space") && timer >= timeBetweenPunches)
        {
            Shoot(true);
        }

        // Start the punching animation
        if (anim_timer < 0.2f && is_punching)
        {
            anim.SetLayerWeight(1, Mathf.Lerp(0, 1, anim_timer * 5));
        }
        // End the punching animation
        else if (anim_timer > 0.6)
        {
            anim.SetLayerWeight(1, Mathf.Lerp(1, 0, (anim_timer - 0.6f) * 5));

            is_punching = false;
            lovisaMovement.speed = 6;
            anim.SetBool("IsPunching", false);
        }
    }

    void Shoot(bool only_damage)
    {
        // Play the gun shot audioclip.
        // gunAudio.Play();
        // Reset the animation timer.
        if (!only_damage)
        {
            if (is_punching)
                anim_timer = 0.2f;
            else
                anim_timer = 0f;
        }

        // Reset the punching (damage) timer.
        timer = 0f;

        //anim.SetBool("IsPunching", true);

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
                    // TODO: change hitpoint to be where the actual hit was (hight and place)
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && distanceToEnemy <= rotationRange)
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