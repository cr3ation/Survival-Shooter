using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class VideoManager : MonoBehaviour {

    public MovieTexture[] movies;

    private MovieTexture movie;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        if (movies.Length > 0)
        {
            movie = movies[Random.Range(0, movies.Length)];

            GetComponent<RawImage>().texture = movie as MovieTexture;
            audio = GetComponent<AudioSource>();
            audio.clip = movie.audioClip;
            movie.Play();
            audio.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
