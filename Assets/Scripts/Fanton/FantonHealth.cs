using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FantonHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    //public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Text money;
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip[] damageTakenClips;                        // The audio clips played when the player takes damage.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    AudioSource audioSource;                                    // Reference to the AudioSource component.
    FantonMovement fantonMovement;                              // Reference to the fanton's movement.
    UnityEngine.AI.NavMeshAgent nav;
    bool isDead;                                                // Whether the player is dead.
    Animator animator;
    bool damaged = false;                                       // True when the player gets damaged.
    bool noShield = false;
    float noShieldBlinkTimer = 0f;
    float prevFadeColorItems = 255;
    float prevFadeColorEuro = 255;
    int itemsLeft = 7;


    void Awake()
    {
        // Setting up the references.
        audioSource = GetComponent<AudioSource>();
        fantonMovement = GetComponent<FantonMovement>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        noShieldBlinkTimer += Time.deltaTime;

        if (damageImage == null) return;
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

        if(noShield)
        {
            BlinkItems();
        }
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current money by the damage amount.
        int moneyLeft = System.Convert.ToInt32(money.text);
        int previousMoney = moneyLeft;
        if(moneyLeft != 0)
            moneyLeft -= amount;
        if (moneyLeft < 0)
            moneyLeft = 0;
        if (previousMoney != moneyLeft && moneyLeft == 0)
        {
            noShield = true;
            noShieldBlinkTimer = 0;
        }
        money.text = System.Convert.ToString(moneyLeft);

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // Play the hurt sound effects
        if (!audioSource.isPlaying)
        {
            var i = Random.Range(0, damageTakenClips.Length);
            audioSource.clip = damageTakenClips[i];
            audioSource.Play();
        }

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead && itemsLeft == 0)
        {
            // ... it should die.
            Death();
        }
    }

    void BlinkItems()
    {
        // Variables for changing color of items.
        float fadeFromItems = 60;
        float fadeToItems = 255;
        float resultItems = fadeFromItems;

        // Variables for changing color of euro-sign.
        float fadeFromEuro = 255;
        float fadeToEuro = 100;
        float resultEuro = fadeFromEuro;

        // The length of one cycle in seconds.
        float blinkInterval = 0.75f;

        // Fade in the color.
        if (noShieldBlinkTimer < blinkInterval)
        {
            resultItems = Mathf.Lerp(fadeFromItems, fadeToItems, noShieldBlinkTimer);
            prevFadeColorItems = resultItems;
            resultEuro = Mathf.Lerp(fadeFromEuro, fadeToEuro, noShieldBlinkTimer);
            prevFadeColorEuro = resultEuro;
        }
        // Fade back to original color.
        else if (noShieldBlinkTimer < blinkInterval * 2f)
        {
            resultItems = Mathf.Lerp(prevFadeColorItems, fadeFromItems, noShieldBlinkTimer - blinkInterval);
            resultEuro = Mathf.Lerp(prevFadeColorEuro, fadeFromEuro, noShieldBlinkTimer - blinkInterval);
        }
        // Restart the cycle.
        else
        {
            noShieldBlinkTimer = 0;
        }

        // Create the two new colors.
        Color newItemColor = new Color(resultItems / 255, fadeFromItems / 255, fadeFromItems / 255, 1);
        Color newEuroColor = new Color(fadeFromEuro / 255, resultEuro / 255, resultEuro / 255, 0.5f);

        // Change colors of items.
        GameObject items = GameObject.Find("Items");
        //for (int i = 0; i < items.transform.GetChild(1).childCount; i++)
        //  items.transform.GetChild(1).GetChild(i).GetComponent<Image>().color = newItemColor;
        items.transform.GetChild(0).GetComponent<Image>().color = newEuroColor;

        // Change color of euro-sign.
        GameObject euro = GameObject.Find("Money");
        euro.transform.GetChild(0).GetComponent<Image>().color = newEuroColor;
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        animator.SetTrigger("Dead");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        audioSource.clip = deathClip;
        audioSource.Play();

        // Turn off the movement
        fantonMovement.enabled = false;
        nav.Stop();
    }
}