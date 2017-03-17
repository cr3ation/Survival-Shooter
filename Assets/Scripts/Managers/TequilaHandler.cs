using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TequilaHandler : MonoBehaviour {

    float particleTimer = 0.0f;
    bool destroy = false;
    bool enemiesKilled = false;
    public GameObject particles;
    AudioSource audioSource;
    MeshRenderer meshRenderer;
    Light pointLight;
    

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        pointLight = transform.GetChild(1).GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {

        particleTimer += Time.deltaTime;
        float activateTime = 5.0f;

        audioSource.volume = Mathf.Lerp(0, 1, particleTimer * 0.5f);
        
        // Activate the particle effect
        if (particleTimer >= activateTime && !destroy)
        {
            particles.SetActive(true);

            destroy = true;
            //Destroy object after 1 second.
            Destroy(gameObject, 3);
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
        if (particleTimer >= activateTime + 0.2f && meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
            pointLight.enabled = false;
        }

    }
}
