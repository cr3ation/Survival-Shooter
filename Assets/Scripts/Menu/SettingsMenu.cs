using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public Dropdown resolution;
    public Dropdown quality;
    public Toggle windowed;


	// Use this for initialization
	void Start () {
        resolution.ClearOptions();
        quality.ClearOptions();

        // Resolution
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            resolution.options.Add(new Dropdown.OptionData() { text = res.width + "x" + res.height });
            //print(res.width + "x" + res.height);
        }

        // Graphics quality
        string[] qualities = QualitySettings.names;
        foreach (var qual in qualities)
        {
            quality.options.Add(new Dropdown.OptionData() { text = qual });
            //print(res.width + "x" + res.height);
        }

        // Window mode
        windowed.isOn = !Screen.fullScreen;

        quality.value = QualitySettings.GetQualityLevel();
        quality.RefreshShownValue();
        resolution.RefreshShownValue();
    }


    // Update is called once per frame
    void Update () {
		
	}



    public void ChangeResolution() {
        var res = resolution.options[resolution.value].text.Split('x');
        print(res[0] + "x" + res[1]);
        //Screen.SetResolution(640, 480, true)
    }
}
