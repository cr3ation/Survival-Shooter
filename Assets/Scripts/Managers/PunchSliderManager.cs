using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchSliderManager : MonoBehaviour {

    public Sprite punchActive;
    public Sprite punchNormal;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(transform.GetChild(1).GetComponent<Slider>().value > 99.5f)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = punchActive;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = punchNormal;
        }

    }
}
