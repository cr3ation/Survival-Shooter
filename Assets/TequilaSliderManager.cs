﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TequilaSliderManager : MonoBehaviour {

    public Sprite tequilaActive;
    public Sprite tequilaNormal;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (transform.GetChild(2).GetComponent<Slider>().value > 99.5f)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = tequilaActive;
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().sprite = tequilaNormal;
        }

    }
}
