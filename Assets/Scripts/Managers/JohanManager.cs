using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohanManager : MonoBehaviour {

    public GameObject spawnPoints;
    private bool hasShirt = true;
    private Animator anim;


    // Use this for initialization
    void Start () {
        // Randomize the starting position.
        int spawnAt = (int)Mathf.Round(Random.Range(0, spawnPoints.transform.childCount));
        print("Johan spawnpos: " + spawnAt);
        transform.position = spawnPoints.transform.GetChild(spawnAt).position;
        transform.rotation = spawnPoints.transform.GetChild(spawnAt).rotation;

        anim = transform.GetChild(0).GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // If Lovisa enters the collider, give the player a shirt.
        if (other.name == "Lovisa" && hasShirt)
        {
            FantonHealth.haveShirt = true;
            hasShirt = false;

            // Start the sad animations.
            anim.SetTrigger("LostShirt");

            // Inactivate the object containing the shirt.
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
