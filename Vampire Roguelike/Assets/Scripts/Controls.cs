using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{

    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    // Start is called before the first frame update

    public Controls()
    {

    }

    public Dictionary<string, KeyCode> playerControls()
    {
        controls.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        controls.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        controls.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        controls.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        controls.Add("Dodge", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dodge", "Space")));
        controls.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
        controls.Add("Bloodsuck", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BloodSuck", "F")));
        controls.Add("Skill1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill1", "1")));
        controls.Add("Skill2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill2", "2")));
        controls.Add("Skill3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill3", "3")));
        return this.controls;
    }


}
