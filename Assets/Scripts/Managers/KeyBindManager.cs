﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour {

    public Text up, down, left, right, punch, specialPunch, rageKick, tequila, zoomIn, zoomOut;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject currentKey;


	// Use this for initialization
	void Start () {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Punch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Punch", "Space")));
        keys.Add("SpecialPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialPunch", "LeftAlt")));
        keys.Add("RageKick", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RageKick", "LeftControl")));
        keys.Add("Tequila", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Tequila", "T")));
        keys.Add("ZoomIn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomIn", "Alpha9")));
        keys.Add("ZoomOut", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomOut", "Alpha0")));

        RefreshKeyBindingText();
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
    }

    void OnGUI() {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        currentKey = clicked;
    }

    public void RefreshKeyBindingText() {
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        punch.text = keys["Punch"].ToString();
        specialPunch.text = keys["SpecialPunch"].ToString();
        rageKick.text = keys["RageKick"].ToString();
        tequila.text = keys["Tequila"].ToString();
        zoomIn.text = keys["ZoomIn"].ToString();
        zoomOut.text = keys["ZoomOut"].ToString();
    }

    public void SaveKeys() {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public void ResetKeys() {
        keys["Up"] = KeyCode.W;
        keys["Down"] = KeyCode.S;
        keys["Left"] = KeyCode.A;
        keys["Right"] = KeyCode.D;
        keys["Punch"] = KeyCode.Space;
        keys["SpecialPunch"] = KeyCode.LeftAlt;
        keys["RageKick"] = KeyCode.LeftControl;
        keys["Tequila"] = KeyCode.T;
        keys["ZoomIn"] = KeyCode.Alpha9;
        keys["ZoomOut"] = KeyCode.Alpha0;

        RefreshKeyBindingText();
    }

    public void ReloadStoredKeys() {
        keys.Clear();

        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Punch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Punch", "Space")));
        keys.Add("SpecialPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialPunch", "LeftAlt")));
        keys.Add("RageKick", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RageKick", "LeftControl")));
        keys.Add("Tequila", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Tequila", "T")));
        keys.Add("ZoomIn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomIn", "Alpha9")));
        keys.Add("ZoomOut", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomOut", "Alpha0")));

        RefreshKeyBindingText();
    }
}
