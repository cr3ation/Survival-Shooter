using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FantonHealth : MonoBehaviour
{
    public int startingHealth = 300;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    //public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Text money;
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip[] damageTakenClips;                        // The audio clips played when the player takes damage.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public static bool haveDrugs = false;
    public static bool haveShirt = false;


    AudioSource audioSource;                                    // Reference to the AudioSource component.
    FantonMovement fantonMovement;                              // Reference to the fanton's movement.
    UnityEngine.AI.NavMeshAgent nav;
    public static bool isDead;                                                // Whether the player is dead.
    Animator animator;
    bool damaged = false;                                       // True when the player gets damaged.
    bool noShield = false;
    float noShieldBlinkTimer = 0f;
    float prevFadeColor = 255;
    int itemsLeft = 5;
    float lifePerItem;
    float drugsLife;
    float shirtLife;

    float moneyBlinkTimer = 9999.9f;


    void Awake()
    {
        // Setting up the references.
        audioSource = GetComponent<AudioSource>();
        fantonMovement = GetComponent<FantonMovement>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
        lifePerItem = (float)startingHealth / 5.0f;
        drugsLife = lifePerItem;
        shirtLife = lifePerItem;
        isDead = false;
    }


    void Update()
    {
        noShieldBlinkTimer += Time.deltaTime;
        moneyBlinkTimer += Time.deltaTime;

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
            BlinkItems(false);
        }

        if (System.Convert.ToInt32(money.text) > 0)
        {
            noShield = false;
            BlinkItems(true);
        }

        // Blink the color of the money-text in red.
        if(moneyBlinkTimer < 0.5f)
        {
            float greenBlueValue = Mathf.Lerp(0.0f, 1.0f, moneyBlinkTimer*2);
            money.color = new Color(1.0f, greenBlueValue, greenBlueValue);
        }
        
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Start the red blink-animation of money-text.
        moneyBlinkTimer = 0;

        // If the shield is down start taking damage to inventory
        if (noShield)
        {
            // If there is no drugs or shirt in the inventory, start dealing damage to other items.
            if(!haveDrugs && !haveShirt)
            {
                currentHealth -= amount;
                itemsLeft = (int)Mathf.Ceil((float)currentHealth / lifePerItem);
            }
            // If the player has shirt, deal damage to that first.
            else if(haveShirt)
            {
                shirtLife -= amount;
                if (shirtLife < 1)
                    haveShirt = false;
            }
            // If the player has drugs, deal damage to those first.
            else
            {
                drugsLife -= amount;
                if (drugsLife < 1)
                    haveDrugs = false;
            }

            BlinkItems(true);
        }

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
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void BlinkItems(bool resetColors)
    {
        // Variables for changing color of items and euro-sign.
        float fadeFrom = 255;
        float fadeTo = 50;
        float result = fadeFrom;

        if (!resetColors)
        {
            // The length of one cycle in seconds.
            float blinkInterval = 0.75f;

            // Fade in the color.
            if (noShieldBlinkTimer < blinkInterval)
            {
                result = Mathf.Lerp(fadeFrom, fadeTo, noShieldBlinkTimer);
                prevFadeColor = result;

            }
            // Fade back to original color.
            else if (noShieldBlinkTimer < blinkInterval * 2f)
            {
                result = Mathf.Lerp(prevFadeColor, fadeFrom, noShieldBlinkTimer - blinkInterval);
            }
            // Restart the cycle.
            else
            {
                noShieldBlinkTimer = 0;
            }
        }

        // Create the two new colors.
        Color newColor = new Color(fadeFrom / 255, result / 255, result / 255, 200.0f / 255);
        Color disabledColor = new Color(1, 1, 1, 10.0f / 255);

        // Change colors of items.
        GameObject items = GameObject.Find("Items");
        for (int i = 0; i < items.transform.GetChild(1).childCount; i++)
          items.transform.GetChild(1).GetChild(i).GetComponent<Image>().color = newColor;

        // Reset color for disabled items.
        for (int i = items.transform.GetChild(1).childCount - 1; i >= itemsLeft; i--)
          items.transform.GetChild(1).GetChild(i).GetComponent<Image>().color = disabledColor;

        // Set the drugs color.
        if(haveDrugs && drugsLife > 1)
        {
            items.transform.GetChild(1).GetChild(5).GetComponent<Image>().color = newColor;
        }
        // Set the shirt color.
        if(haveShirt && shirtLife > 1)
        {
            items.transform.GetChild(1).GetChild(6).GetComponent<Image>().color = newColor;
        }


        // Change color of euro-sign.
        GameObject euro = GameObject.Find("Money");
        euro.transform.GetChild(0).GetComponent<Image>().color = newColor;
        euro.transform.GetChild(1).GetComponent<Text>().color = newColor;
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