using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchSliderManager : MonoBehaviour {

    public Sprite punchActive;
    public Sprite punchNormal;

    Animator animator;
    Slider slider;
    float lastSliderValue;

	// Use this for initialization
	void Start () {
        animator = transform.GetChild(0).GetComponent<Animator>();
        slider = transform.GetChild(2).GetComponent<Slider>();

    }
	
	// Update is called once per frame
	void Update () {

        lastSliderValue = slider.value;

        // Prevent the animation trigger from constantly getting triggered.
        if(slider.value > 99.5f && slider.value != lastSliderValue)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = punchActive;
            animator.SetTrigger("AttackReady");
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().sprite = punchNormal;
        }


    }
}
