using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TequilaHandler : MonoBehaviour {

    float particleTimer = 0.0f;
    public GameObject particles;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        particleTimer += Time.deltaTime;

        if (particleTimer >= 15)
        {
            particles.SetActive(true);
        }
        else if(particleTimer >= 18)
        {
            particles.SetActive(false);
            Destroy(gameObject, 0.1f);
        }

    }
}
