using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public Slider audioSlider;
    public AudioMixer audioMixer;
    private GameObject selectedKey;

    private Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();

    public Text up, left, down, right, dodge, interact, bloodSuck, slot1, slot2, slot3;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();

    }

    public void Start()
    {
        
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume", 0f));
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 0f);

        controls.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        controls.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        controls.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        controls.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        controls.Add("Dodge", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dodge", "Space")));
        controls.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
        controls.Add("Bloodsuck", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BloodSuck", "F")));
        controls.Add("Skill1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill1", "r")));
        controls.Add("Skill2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill2", "t")));
        controls.Add("Skill3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Skill3", "g")));

        up.text = controls["Up"].ToString();
        down.text = controls["Down"].ToString();
        left.text = controls["Left"].ToString();
        right.text = controls["Right"].ToString();
        dodge.text = controls["Dodge"].ToString();
        interact.text = controls["Interact"].ToString();
        bloodSuck.text = controls["Bloodsuck"].ToString();
        slot1.text = controls["Skill1"].ToString();
        slot2.text = controls["Skill2"].ToString();
        slot3.text = controls["Skill3"].ToString();
    }

    public void ChangeKey(GameObject clicked)
    {
        
        selectedKey = clicked;
        selectedKey.GetComponent<Image>().color = Color.green;
    }
    private void OnGUI()
    {
        if (selectedKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {

                if (controls.ContainsValue(e.keyCode))
                {
                    selectedKey.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    selectedKey = null;
                }
                else
                {
                    controls[selectedKey.name] = e.keyCode;
                    PlayerPrefs.SetString(selectedKey.name, e.keyCode.ToString());
                    selectedKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                    PlayerPrefs.Save();
                    selectedKey.GetComponent<Image>().color = new Color(0,0,0,1);
                    selectedKey = null;
                }
            }
        }
    }

    
}
