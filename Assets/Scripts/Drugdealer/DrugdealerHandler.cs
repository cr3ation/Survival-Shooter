using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugdealerHandler : MonoBehaviour {

    public GameObject spawnPoints;
    private bool hasDrugs = true;

    AudioSource audioSource;                                    // Reference to the AudioSource component.
    public AudioClip weedClip;                                 

    // Use this for initialization
    void Start () {
        // Randomize the starting position.
        int spawnAt = (int)Mathf.Round(Random.Range(0, spawnPoints.transform.childCount));
        //print("Drugdealer spawnpos: " + spawnAt);
        transform.position = spawnPoints.transform.GetChild(spawnAt).position;
        transform.rotation = spawnPoints.transform.GetChild(spawnAt).rotation;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        // If Lovisa enters the collider, give the player some drugs.
        if(other.name == "Lovisa" && hasDrugs)
        {
            FantonHealth.haveDrugs = true;
            hasDrugs = false;

            // Set the audiosource to play the death clip and play it
            audioSource.clip = weedClip;
            audioSource.Play();
        }
    }
}
