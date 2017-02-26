using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class VideoManager : MonoBehaviour {

    public float restartDelay = 2f;         // Time to wait before restarting the level
    public AudioSource backgroundMusicAudioSource;
    public MovieTexture[] movies;           // Array containg all the movies

    private MovieTexture movie;
    private AudioSource audio;
    private float restartTimer;

	// Use this for initialization
	void Start () {
        if (movies.Length > 0)
        {
            backgroundMusicAudioSource.Stop();
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
        if (movie.isPlaying) return;

        // .. increment a punchTimer to count up to restarting.
        restartTimer += Time.deltaTime;

        // .. if it reaches the restart delay...
        if (restartTimer >= restartDelay)
        {
            // .. then reload the currently loaded level.
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
