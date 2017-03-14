using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoFrameManager : MonoBehaviour {

    public Image videoFrame;
    public static bool startVideo;
    public Image background;
    public AudioSource backroundMusic;

    // The number of frames in each video, 1 to 10.
    int[] frameCount = { 382, 353, 280, 409, 332, 535, 440, 447, 298, 310 };
    float frameRate = 30;
    float timer = 0;
    int choosenVideo = 0;
    float musicVolume;

    bool firstRun;
    bool firstStartVideo;
    bool startMutingMusic;

    // The array of frames.
    Sprite[] frames;

	// Use this for initialization
	void Start () {

        // Set the opacity for the canvas displaying video to 0 initially.
        videoFrame.canvasRenderer.SetAlpha(0.0f);
        background.canvasRenderer.SetAlpha(0.0f);

        startVideo = false;
        firstStartVideo = true;
        startMutingMusic = true;
        firstRun = true;

    }
	
	// Update is called once per frame
	void Update () {

        // Start loading the frames for the video at level start.
        if(firstRun)
        {
            LoadVideo();
            firstRun = false;
        }

        // Start playing the video.
        if(startVideo)
        {
            // Mute background music.
            if(startMutingMusic)
            {
                musicVolume = backroundMusic.volume;
                startMutingMusic = false;
            }
            backroundMusic.volume = Mathf.Lerp(musicVolume, 0, timer);
            
            // Make sure the canvas for displaying the video is visible and active.
            if (firstStartVideo)
            {
                videoFrame.canvasRenderer.SetAlpha(1.0f);
                background.canvasRenderer.SetAlpha(1.0f);
                transform.GetChild(1).GetComponent<AudioSource>().enabled = true;
                firstStartVideo = false;
            }


            timer += Time.deltaTime;

            // Increment the frame.
            int currentFrame = (int)(timer * frameRate);
            if (currentFrame >= frames.Length)
                currentFrame = frames.Length - 1;

            // Change the texture to the next frame.
            videoFrame.sprite = frames[currentFrame];
        }

        // When all frames has been played, change back to Menu scene.
        if(timer * frameRate > frameCount[choosenVideo])
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }

    }

    void LoadVideo()
    {
        // Randomize which video to play.
        choosenVideo = (((int)Random.Range(0, 100)) % 10);
        // Select the number of frames for the chosen video.
        string videoNumber = System.Convert.ToString(choosenVideo + 1);

        // Set the corresponding audio-clip.
        AudioClip audio = (AudioClip)Resources.Load(string.Format("endVideo" + videoNumber));
        transform.GetChild(1).GetComponent<AudioSource>().clip = audio;

        // Save all frames to an array with sprites.
        Texture2D temp;
        frames = new Sprite[frameCount[choosenVideo]];
        for (int i = 0; i < frameCount[choosenVideo]; ++i)
        {
            temp = (Texture2D)Resources.Load(string.Format(videoNumber + "_video{0:d4}", i + 1));
            frames[i] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));
        }
    }
}
