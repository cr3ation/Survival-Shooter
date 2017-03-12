using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {


    public AudioClip[] backgroundMusic;

    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (source.isPlaying) { return; }

        var i = Random.Range(0, backgroundMusic.Length - 1);
        source.Stop();
        source.clip = backgroundMusic[i];
        source.Play();
    }
}
