using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuScene : MonoBehaviour {

    public void GoToScene(string newGameLevel)         
    {
        SceneManager.LoadScene(newGameLevel);
    }

    // Use this for initialization
    void Start () {
        GoToScene("Menu");		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
