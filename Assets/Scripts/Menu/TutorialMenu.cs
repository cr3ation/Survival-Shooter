using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour {

    public Image image;
    public Sprite[] sprites;
    public Text nextText;

    int spriteIndex = 0;
	// Use this for initialization
	void Start () {
        image.sprite = sprites[spriteIndex];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextImage() {
        if (spriteIndex < sprites.Length - 1)
        {
            spriteIndex++;
            nextText.text = "Back";
        }
        else
        {
            spriteIndex = 0;
            nextText.text = "Next";
        }
        image.sprite = sprites[spriteIndex];
    }
}
