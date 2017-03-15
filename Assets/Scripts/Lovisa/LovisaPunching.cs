using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public int damagePerKick = 150;
    public int damageSuperPunch = 200;
    public float timeBetweenPunches = 0.2f;        // The time between each shot.
    public float range = 3f;                        // The distance the gun can fire.
    public float kickRange = 4f;
    public float rotationRange = 4f;                // The distance to an enemy when player starts to rotate
    public Slider rageSlider;                                 // Reference to the UI's rage bar.
    public Slider superPunchSlider;
    public Text rageText;
    public int currentRage;
    public float cooldown;
    public GameObject tequila;


    float punchTimer;                                    // A punchTimer to determine when to fire.
    float animationTimer;                               // A punchTimer to control the punch animations.
    float slowMoTimer;
    float tequilaTimer;
    //bool is_punching = false;                       // True during the punch
    float distanceToEnemy;                          // Distance to the closest enemy
    //float rotationSpeed = 10f;                      // Speen in witch to rotate
    bool isTequila;
    bool isKicking;
    bool superPunch;
    bool hasSuperPunched;
    int kickNumber;
    List<GameObject> enemiesInKickRange;
    Animator animator;                                  // Reference to the anomator controller object
    LovisaMovement lovisaMovement;                  // Reference to the LovsaMovement object
    Rigidbody rigidBody;                            // Reference to the rigidBody object
    GameObject closestEnemy;                        // Refrencee to the closest enemy
    GameObject inventoryInspector;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();       //Contains stored input keys

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        animator = GetComponent<Animator>();
        lovisaMovement = GetComponent<LovisaMovement>();
        rigidBody = GetComponent<Rigidbody>();
        inventoryInspector = GameObject.FindGameObjectWithTag("InventoryInspector");
        Cursor.visible = false;
        isKicking = false;
        superPunch = false;
        isTequila = false;
        animationTimer = 1000;
        punchTimer = 1000;
        currentRage = 100;
        cooldown = 0;

        // Don't have the energy to explain why... just belive me...
        slowMoTimer = 5f;

        // Key kontroller
        keys.Add("Punch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Punch", "H")));
        keys.Add("SpecialPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialPunch", "J")));
        keys.Add("RageKick", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RageKick", "K")));
    }

    void Update()
    {
        // Add the time since Update was last called to the punchTimer.
        punchTimer += Time.deltaTime;
        animationTimer += Time.deltaTime;
        slowMoTimer += Time.unscaledDeltaTime;
        tequilaTimer += Time.deltaTime;
        if(cooldown > 0)
            cooldown -= Time.deltaTime * 10;

        // Update UI sliders
        rageSlider.value = currentRage;
        superPunchSlider.value = 100 - cooldown;

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

        // Change to slow-motion if a kick-session is active.
        if (slowMoTimer < 5)
        {
            RunSlowMotion();
        }

        // Stop the tequila-phase for Lovisa.
        if (isTequila && tequilaTimer > 1.31f)
        {
            isTequila = false;
            lovisaMovement.speed = 6.0f;
        }
        // Place the tequila object.
        else if(isTequila && tequilaTimer > 0.7f)
        {
            tequila.SetActive(true);
            tequila.transform.position = transform.position;
        }
        
        // Initiate the tequila-attack.
        if(Input.GetButton("Submit") && tequilaTimer > 1)
        {
            tequilaTimer = 0;
            animator.SetTrigger("PutDown");
            lovisaMovement.speed = 0.0f;
            Instantiate(tequila, transform.position, transform.rotation);
            isTequila = true;
        }
        // Initiate a kick if the player has enough rage.
        else if (!isKicking && (Input.GetButton("Fire2") || Input.GetKey(keys["RageKick"])) && currentRage >= 100)
        {
            animator.SetTrigger("Kick");
            currentRage = 0;
            isKicking = true;
            kickNumber = 0;
            punchTimer = 0;
            slowMoTimer = 0;
        }
        // Initiate a superpunch if the cooldown allows it.
        else if (!superPunch && (Input.GetButton("Fire3") || Input.GetKey(keys["SpecialPunch"])) && cooldown < 0.1)
        {
            animator.SetTrigger("Slash");
            cooldown = 100;
            superPunch = true;
            hasSuperPunched = false;
            punchTimer = 0;
        }
        // Blend in/out the superpunch animation layer and cause some damage.
        else if(superPunch)
        {
            ExecuteSuperPunch();
        }
        else if (!isKicking && !superPunch)
        {
            // If Lovisa is not punching at the moment, start punching.
            if ((Input.GetButton("Fire1") || Input.GetKey(keys["Punch"])) && !animator.GetBool("IsPunching"))
            {
                animator.SetBool("IsPunching", true);
                lovisaMovement.speed = 2f;
                animationTimer = 0;
                punchTimer = 0;
                Shoot(damagePerPunch);
            }
            // If the punching-animation is running, only cause damage.
            else if ((Input.GetButton("Fire1") || Input.GetKey(keys["Punch"])) && punchTimer >= timeBetweenPunches)
            {
                Shoot(damagePerPunch);
                // Reset the timer managing the damage.
                punchTimer = 0;
            }

            // If the animationTimer has been set to zero, fade in the punching animation.
            if (animationTimer < 0.25f)
            {
                animator.SetLayerWeight(1, Mathf.Lerp(0, 1, animationTimer * 5));
            }
            // If Lovisa is still punching, keep the opacity of the punching animation to 1 by resetting the animationTimer.
            else if (punchTimer < 0.25f)
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
        else if(!isTequila)
        {
            ExecuteKicks();
        }
    }

    void Shoot(int damage)
    {
        if (distanceToEnemy <= range)
        {
            // Only do damage if the enemy is in front of the player. 
            Vector3 enemy_direction = Vector3.Normalize(closestEnemy.transform.position - rigidBody.transform.position);
            Vector3 player_direction = Vector3.Normalize(rigidBody.transform.forward);
            float angle = Mathf.Acos(Vector3.Dot(enemy_direction, player_direction));
            if (angle < 3.14 / 3.0 || (damage == damageSuperPunch && angle < 3.14 * 0.6))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    float hitHeight;
                    if (closestEnemy.name == "Boss")
                        hitHeight = 2.5f;
                    else
                        hitHeight = 1.2f;
                    enemyHealth.TakeDamage(damage, hitHeight);
                }
            }
        }
    }

    // Slow down the running-time of the game and change music and fanton sound pitches.
    void RunSlowMotion()
    {
        if (inventoryInspector == null)
        {
            if (isKicking)
            {
                float pitch = Mathf.Lerp(1, 0.5f, slowMoTimer);
                GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().pitch = pitch;
                GameObject.Find("Fanton").GetComponent<AudioSource>().pitch = pitch;
                Time.timeScale = Mathf.Lerp(1, 0.25f, slowMoTimer * 4);
            }
            else
            {
                float pitch = Mathf.Lerp(0.5f, 1, slowMoTimer);
                GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().pitch = pitch;
                GameObject.Find("Fanton").GetComponent<AudioSource>().pitch = pitch;
                Time.timeScale = Mathf.Lerp(0.25f, 1, slowMoTimer);
            }
        }
    }

    void ExecuteSuperPunch()
    {

        if (punchTimer < 0.5 / 1.2)
            animator.SetLayerWeight(2, Mathf.Lerp(0, 1, punchTimer * 2 * 1.2f));
        if (punchTimer > 1 / 1.2)
            animator.SetLayerWeight(2, Mathf.Lerp(1, 0, (punchTimer - 1 / 1.2f) * 2 * 1.2f));
        if (punchTimer > 1.5 / 1.2)
            superPunch = false;
        if (punchTimer > 0.75 / 1.2 && !hasSuperPunched)
        {
            Shoot(damageSuperPunch);
            hasSuperPunched = true;
        }
    }

    void ExecuteKicks()
    {
        float animationSpeed = 3f;

        // During the kick, rotate Lovisa so that the kicks hit in every direction.
        transform.Rotate(new Vector3(0, -4.0f, 0));

        // This is when the first kick lands. Deal damage to 1/3 of the enemies in range.
        if (punchTimer > 1 / animationSpeed && kickNumber == 0)
        {
            enemiesInKickRange = findEnemiesInKickRange();
            for (int i = 2 * enemiesInKickRange.Count / 3; i < enemiesInKickRange.Count; i++)
            {
                enemiesInKickRange[i].GetComponent<EnemyHealth>().TakeDamage(damagePerKick, 1.2f);
            }
            kickNumber++;
        }
        // This is when the second kick lands. Deal damage to the second 1/3 of the enemies in range.
        else if (punchTimer > 0.9 * 2.0 / animationSpeed && kickNumber == 1)
        {
            for (int i = enemiesInKickRange.Count / 3; i < 2 * enemiesInKickRange.Count / 3; i++)
            {
                enemiesInKickRange[i].GetComponent<EnemyHealth>().TakeDamage(damagePerKick, 1.2f);
            }
            kickNumber++;
        }
        // This is when the third kick lands. Deal damage to the rest of the enemies in range.
        else if (punchTimer > 0.9 * 3 / animationSpeed && kickNumber == 2)
        {
            for (int i = 0; i < enemiesInKickRange.Count; i++)
            {
                enemiesInKickRange[i].GetComponent<EnemyHealth>().TakeDamage(damagePerKick, 1.2f);
            }
            kickNumber++;
        }
    }


    // Returns a list with all enemies within the kick-range of Lovisa.
    List<GameObject> findEnemiesInKickRange()
    {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();

        Vector3 position = transform.position;

        foreach (GameObject enemy in allEnemies)
        {
            var curDistance = Vector3.Distance(enemy.transform.position, position);

            if (curDistance < kickRange)
                enemiesInRange.Add(enemy);
        }

        return enemiesInRange;
    }


    // Triggered by the last kick-animation.
    void EndOfKick()
    {
        isKicking = false;
        slowMoTimer = 0;

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