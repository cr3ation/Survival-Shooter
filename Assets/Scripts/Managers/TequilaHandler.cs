using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TequilaHandler : MonoBehaviour {

    float particleTimer = 0.0f;
    bool destroy = false;
    bool enemiesKilled = false;
    public GameObject particles;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        particleTimer += Time.deltaTime;
        float activateTime = 5.0f;
        
        // Activate the particle effect
        if (particleTimer >= activateTime && !destroy)
        {
            particles.SetActive(true);
            destroy = true;
            //Destroy object after 1 second.
            Destroy(gameObject, 1);
        }
        // Cause damage to enemies.
        else if(destroy && !enemiesKilled)
        {
            if(particleTimer >= (activateTime + 0.25f))
            {
                GameObject[] tequilaEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in tequilaEnemies)
                {
                    var curDistance = Vector3.Distance(enemy.transform.position, transform.position);

                    if (curDistance < 4)
                    {
                        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                        enemyHealth.TakeDamage(120, 1.2f);
                    }
                }
                enemiesKilled = true;
            }
        }

    }
}
