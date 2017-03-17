using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    Text text;                      // Reference to the Text component.

    float seconds;
    int minutes;

    // Use this for initialization
    void Start () {
        // Set up the reference.
        text = GetComponent<Text>();

        seconds = 0;
        minutes = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(!FantonHealth.isDead)
        {
            seconds += Time.deltaTime;
        }
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        text.text = "Time: ";
        if (minutes == 0)
        {
            text.text += "00";
        }
        else if(minutes < 10)
        {
            text.text += "0" + System.Convert.ToString(minutes);
        }
        else
        {
            text.text += System.Convert.ToString(minutes);
        }
        text.text += ":";
        if(seconds < 10)
        {
            text.text += "0";
        }
        text.text += System.Convert.ToString((int)seconds);

    }
}
