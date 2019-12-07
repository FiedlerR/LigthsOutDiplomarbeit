using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        LoadKeyBindings();
    }

    public bool GetKeyDown(string Action) {
        return Input.GetKeyDown(keys[Action]);
    }

    public bool GetKeyUp(string Action) {
        return Input.GetKeyUp(keys[Action]);
    }

    public bool GetKey(string Action) {
        return Input.GetKey(keys[Action]);
    }

    public void LoadKeyBindings() { // Gets keybinds from Playerprefs and has defaults 
        //Movement
        keys.Add("Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
        keys.Add("Back", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Sneak", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sneak", "C")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("Use", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Use", "E")));

        //Weaponhandling
        keys.Add("Shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Shoot", "Mouse0")));
        keys.Add("Reload", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Reload", "R")));
    }
}
