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
        return Input.GetKeyDown(keys[Action.ToLower()]);
    }

    public bool GetKeyUp(string Action) {
        return Input.GetKeyUp(keys[Action.ToLower()]);
    }

    public bool GetKey(string Action) {
        return Input.GetKey(keys[Action.ToLower()]);
    }

    public void LoadKeyBindings() { // Gets keybinds from Playerprefs and has defaults 
        //Movement
        keys.Add("forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forward", "W")));
        keys.Add("back", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("back", "S")));
        keys.Add("left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left", "A")));
        keys.Add("right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right", "D")));
        keys.Add("jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "Space")));
        keys.Add("sneak", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sneak", "C")));
        keys.Add("sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sprint", "LeftShift")));
        keys.Add("use", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("use", "E")));

        //Weaponhandling
        keys.Add("shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shoot", "Mouse0")));
        keys.Add("reload", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("reload", "R")));
    }
}
