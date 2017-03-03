using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RageManager : MonoBehaviour {

    public static int rage;        // Lovisas rage

    //Text text;                      // Reference to the Text component.

    void Awake()
    {
        // Set up the reference.
        //text = GetComponent<Text>();

        // Reset the score.
        rage = 0;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
