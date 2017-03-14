using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class VideoManager : MonoBehaviour {

    //public float restartDelay = 2f;         // Time to wait before restarting the level
    //public AudioSource backgroundMusicAudioSource;
    //public MovieTexture[] movies;           // Array containg all the movies
    //public GameObject videoCanvas;

    //private MovieTexture movie;
    //private new AudioSource audio;
    //private float restartTimer;

    //int vsyncprevious;

    // Use this for initialization
    void Start () {

        // Set the boolean in VideoFrameManager to true, starting the video playback.
        VideoFrameManager.startVideo = true;

        //GameObject.FindGameObjectWithTag("VideoCanvas").SetActive(true);
        //print(GameObject.Find("CanvasObject").GetComponent<Canvas>().enabled);
        //print(GameObject.Find("CanvasObject").activeSelf);
        //GameObject.Find("CanvasObject").GetComponent<Canvas>().enabled = true;

        //if (movies.Length > 0)
        //{
            // Stop the background music
            //backgroundMusicAudioSource.Stop();

            // Select a random video
            /*var i = Random.Range(0, movies.Length);
            movie = movies[i];
            GetComponent<RawImage>().texture = movie as MovieTexture;

            vsyncprevious = QualitySettings.vSyncCount;
            QualitySettings.vSyncCount = 0;

            // Get audio from video
            audio = GetComponent<AudioSource>();
            audio.clip = movie.audioClip;

            // Stop playing. Needed if same video is loaded again.
            movie.Stop();
            audio.Stop();

            // Start playing
            movie.Play();
            audio.Play();*/
        //}
    }
	
	// Update is called once per frame
	void Update () {
        //QualitySettings.vSyncCount = vsyncprevious;
        //QualitySettings.vSyncCount = 0;

        /*if (movie.isPlaying) return;

        VideoFrameManager.

        // .. increment a restartTimer to count up to restarting.
        restartTimer += Time.deltaTime;


        // .. if it reaches the restart delay...
        if (restartTimer >= restartDelay)
        {
            // .. then reload the currently loaded level.
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }*/
    }
}
