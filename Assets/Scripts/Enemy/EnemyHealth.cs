using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    public AudioClip[] damageTakenClips;        // The audio clips played when the player takes damage.
    bool isDead;                                // Whether the enemy is dead.


    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.
    float timer;


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        timer = 100;

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        timer += Time.deltaTime;

        // Change the opacity of the animation layer that contains the hit-animation
        if(!this.name.Contains("Boss"))
        {
            if (timer < 0.3)
                anim.SetLayerWeight(2, Mathf.Lerp(0, 1, timer * 3));
            else
                anim.SetLayerWeight(2, Mathf.Lerp(1, 0, (timer - 1.3f) / 2));
        }
    }


    public void TakeDamage(int amount, float hitHeight)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        if(timer > 1)
            timer = 0;

        // Play the hurt sound effect.
        enemyAudio.Stop();
        var i = Random.Range(0, damageTakenClips.Length);
        if (damageTakenClips != null && damageTakenClips.Length > 0)
        {
            enemyAudio.clip = damageTakenClips[i];
            enemyAudio.Play();
        }

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        Vector3 hitPoint = transform.position;
        hitPoint.y = hitHeight;
        hitParticles.transform.position = hitPoint;
        hitParticles.transform.rotation = Random.rotation;//new Vector3(0, -5, 0);//playerPos;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Remove the "Enemy" tag so that Lovisa does not keep on punching the dead body.
        this.tag = "Untagged";

        GameObject.Find("Lovisa").GetComponent<LovisaPunching>().currentRage += 10;

        timer = 10;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // Increase the score by the enemy's score value.
        ScoreManager.score += scoreValue;

        // Saves the score
        PlayerPrefs.SetInt("SavedScore", ScoreManager.score);

        // The enemy should no sink.
        isSinking = true;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}