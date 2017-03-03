using UnityEngine;

public class LovisaMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    public float rotationSpeed = 1f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    UnityEngine.AI.NavMeshAgent nav;    // Reference to the NavMeshAgent connected to the player.
    float timer;


    void Awake()
    {
        // Set up references.
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") ||
            anim.GetCurrentAnimatorStateInfo(0).IsTag("Kick"))
        {
            return;
        }

        // Update the punchTimer
        timer += Time.deltaTime;

        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning(h, v);
        
        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        nav.Move(movement);
        nav.SetDestination(transform.position + movement);
        
    }

    void Turning(float h, float v)
    {
        // If no key is pressed, do not rotate.
        if (h == 0 & v == 0) { return; }

        // Create a quaternion (rotation) based on looking down the vector
        Quaternion newRotation = Quaternion.LookRotation(movement);

        // Set the player's rotation to this new rotation.
        transform.rotation= Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        if (walking)
        {
            timer = 0f;
            anim.SetBool("IsWalking", walking);
        }
        else if (timer > 0.08f)
        {
            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }
    }
}