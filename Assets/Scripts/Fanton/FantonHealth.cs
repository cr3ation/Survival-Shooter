﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FantonHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip[] damageTakenClips;                        // The audio clips played when the player takes damage.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    AudioSource audioSource;                                    // Reference to the AudioSource component.
    FantonMovement fantonMovement;                              // Reference to the fanton's movement.
    UnityEngine.AI.NavMeshAgent nav;
    bool isDead;                                                // Whether the player is dead.
    bool damaged = false;                                       // True when the player gets damaged.


    void Awake()
    {
        // Setting up the references.
        audioSource = GetComponent<AudioSource>();
        fantonMovement = GetComponent<FantonMovement>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effects
        if (!audioSource.isPlaying)
        {
            var i = Random.Range(0, damageTakenClips.Length);
            audioSource.clip = damageTakenClips[i];
            audioSource.Play();
        }

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        // anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        audioSource.clip = deathClip;
        audioSource.Play();

        // Turn off the movement
        fantonMovement.enabled = false;
        nav.Stop();
    }
}