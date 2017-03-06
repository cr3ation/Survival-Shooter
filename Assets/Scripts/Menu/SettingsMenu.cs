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

        // Resolution dropdown
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            if (res.width > 1024)
            {
                resolution.options.Add(new Dropdown.OptionData() { text = res.width + "x" + res.height });
            }
        }
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < resolution.options.Count; i++)
        {
            var res = resolution.options[i].text.Split('x');
            if (currentResolution.width == int.Parse(res[0]) && currentResolution.height == int.Parse(res[1]))
            {
                resolution.value = i;
            }
        }


        // Graphics quality drop down
        string[] qualities = QualitySettings.names;
        foreach (var qual in qualities)
        {
            quality.options.Add(new Dropdown.OptionData() { text = qual });
        }
        quality.value = QualitySettings.GetQualityLevel();

        // Window mode
        windowed.isOn = !Screen.fullScreen;

        quality.RefreshShownValue();
        resolution.RefreshShownValue();
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void SetResolution() {
        var res = resolution.options[resolution.value].text.Split('x');
        int width = int.Parse(res[0]);
        int height = int.Parse(res[1]);
        Screen.SetResolution(width, height, !windowed.isOn);
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(quality.value);
    }

    public void Save()
    {
        SetQuality();
        SetResolution();
    }
}