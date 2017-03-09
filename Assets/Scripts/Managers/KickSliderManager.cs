using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickSliderManager : MonoBehaviour {

    public Sprite kickActive;
    public Sprite kickNormal;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.GetChild(1).GetComponent<Slider>().value > 99.5f)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = kickActive;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = kickNormal;
        }

    }
}
