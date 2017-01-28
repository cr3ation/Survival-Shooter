using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public FantonHealth fantonHealth;


    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (fantonHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}
