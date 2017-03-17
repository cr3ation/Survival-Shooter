using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSliderManager : MonoBehaviour {

    public Sprite attackActive;
    public Sprite attackNormal;
    public Image image;
    public Slider slider;

    Animator animator;
    bool hasChangedImage = false;

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        // Prevent the animation trigger from constantly getting triggered.
        if (slider.value > 99.5f && !hasChangedImage)
        {
            hasChangedImage = true;
            image.sprite = attackActive;
            animator.SetTrigger("AttackReady");
            print("HEJJ");
        }
        else if(slider.value < 90)
        {
            hasChangedImage = false;
            image.sprite = attackNormal;
        }
    }
}
