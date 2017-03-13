using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMenuScene : MonoBehaviour {

    Image img;
    Text txt;
    bool change;
    bool startChange;
    bool startBlink;
    bool fadeIn;

    float timerChange;
    float timerAnyKey;

    public void GoToScene(string newGameLevel)         
    {
        SceneManager.LoadScene(newGameLevel);
    }

    // Use this for initialization
    void Start () {
        startChange = false;
        startBlink = false;
        fadeIn = true;
        timerChange = 0;
        timerAnyKey = 0;
        img = this.transform.GetChild(1).GetComponent<Image>();
        txt = this.transform.GetChild(2).GetComponent<Text>();
        txt.canvasRenderer.SetAlpha(0.0f);
    }
	
	// Update is called once per frame
	void Update () {

        // Start fading in and out the "press any key to contintue"-text.
        if(Time.timeSinceLevelLoad > 1)
        {
            timerAnyKey += Time.deltaTime;
            if(timerAnyKey > 1.5f)
            {
                if(fadeIn)
                {
                    txt.CrossFadeAlpha(1, 0.75f, true);
                    fadeIn = false;
                }
                else
                {
                    txt.CrossFadeAlpha(0, 0.75f, true);
                    fadeIn = true;
                }
                timerAnyKey = 0;
            }
        }

        if (Input.anyKey)
        {
            startChange = true;
        }

        // Fade out the image and change scene.
        if (startChange)
        {
            timerAnyKey = 0;
            timerChange += Time.deltaTime;
            img.CrossFadeAlpha(0, 0.05f, true);
            txt.CrossFadeAlpha(0, 0.05f, true);
            if (timerChange > 0.7f)
                GoToScene("Menu");
        }

    }
}
