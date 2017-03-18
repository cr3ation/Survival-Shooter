using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    // public Slider audioSlider;
    Slider audioSlider;
	
    // Use this for initialization
	void Start () {
        audioSlider = transform.GetComponentInChildren<Slider>();
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="newGameLevel">Name if the scene to load</param>
    public void LoadScene(string newGameLevel)
    {
        if (string.IsNullOrEmpty(newGameLevel))
        {
            return;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameLevel);
    }

    public void ResumeGame() {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Used to set background volume
    /// </summary>
    public void SetBackgroundMusicVolume()
    {
        var audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        audioSource.volume = audioSlider.value;
        PlayerPrefs.SetFloat("Volume", audioSlider.value);
    }
}
