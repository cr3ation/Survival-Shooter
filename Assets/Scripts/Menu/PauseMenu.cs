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
        Cursor.visible = true;
        audioSlider = transform.GetComponentInChildren<Slider>();
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string newGameLevel)
    {
        if (string.IsNullOrEmpty(newGameLevel))
        {
            return;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameLevel);
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
