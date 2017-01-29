using System.Collections.Generic;
using UnityEngine;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public float timeBetweenPunches = 0.15f;        // The time between each shot.
    public float range = 50f;                       // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    Animator anim;
    LovisaMovement lovisaMovement;
    GameObject closestEnemy;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        anim = GetComponent<Animator>();
        lovisaMovement = GetComponent<LovisaMovement>();
    }

    void Update()
    {
        // Find closest enemy
        closestEnemy = FindClosestEnemy();
        // Distance to closest enemy
        var distanceToEnemy = Vector3.Distance(closestEnemy.transform.position, transform.position);
        Debug.Log("Distance: " + distanceToEnemy);

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && distanceToEnemy <= range && timer >= timeBetweenPunches)
        {
            // ... punch the enemy.
            Shoot();
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