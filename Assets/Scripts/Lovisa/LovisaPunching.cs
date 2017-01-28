using UnityEngine;

public class LovisaPunching : MonoBehaviour
{
    public int damagePerPunch = 20;                 // The damage inflicted by each bullet.
    public float timeBetweenPunches = 0.15f;        // The time between each shot.
    public float range = 50f;                       // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    Animator anim;
    LovisaMovement lovisaMovement;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
        lovisaMovement = GetComponent<LovisaMovement>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenPunches)
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
        // Reset the timer.
        timer = 0f;
 
        anim.SetBool("IsPunching", true);

        // Play the gun shot audioclip.
        // gunAudio.Play();


        // Stop the particles from playing if they were, then start the particles.
        //gunParticles.Stop();
        //gunParticles.Play();
    }
}