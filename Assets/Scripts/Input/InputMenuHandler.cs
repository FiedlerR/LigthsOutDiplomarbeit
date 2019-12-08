using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Dieser InputMenuHandler verwaltet alle Aktionen im UI Menü, wo man die Inputkeys verändern kann
 */
public class InputMenuHandler : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys;
    private Dictionary<string, KeyCode> backupKeys;
    public Button[] keyButton;
    private Text[] keyText;

    private GameObject currentKey;

    public Color32 buttonNormal;
    public Color32 buttonEditing;

    private void Awake()
    {
        keys = GameObject.FindObjectOfType<InputManager>().keys;
        backupKeys = keys;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < keyButton.Length; i++) {
            keyText[i] = keyButton[i].GetComponentInChildren<Text>();
        }

        // set Button text acording to set Buttons
        int j = 0;
        foreach (var key in keys) {
            keyText[j].text = key.Key.ToString();
            j++;
        }
    }

    private void OnGUI()
    {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                currentKey.GetComponentInChildren<Image>().color = buttonNormal;
                currentKey = null;
            }
            else if (e.shift) {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetComponentInChildren<Text>().text = "Shift";
                currentKey.GetComponentInChildren<Image>().color = buttonNormal;
                currentKey = null;
            }
            else if (e.isMouse)
            {
                string keyString = "MouseCode";
                switch (e.keyCode) {
                    case KeyCode.Mouse0:
                        keyString = "Mouse0";
                        break;
                    case KeyCode.Mouse1:
                        keyString = "Mouse1";
                        break;
                    case KeyCode.Mouse2:
                        keyString = "Mouse2";
                        break;
                    case KeyCode.Mouse3:
                        keyString = "Mouse3";
                        break;
                    case KeyCode.Mouse4:
                        keyString = "Mouse4";
                        break;
                    case KeyCode.Mouse5:
                        keyString = "Mouse5";
                        break;
                    case KeyCode.Mouse6:
                        keyString = "Mouse6";
                        break;
                }
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetComponentInChildren<Text>().text = keyString;
                currentKey.GetComponentInChildren<Image>().color = buttonNormal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) { //Trigger for OnGUI to run
        if (currentKey != null)
        {
            currentKey.GetComponentInChildren<Image>().color = buttonNormal;
        }
        currentKey = clicked;
        currentKey.GetComponentInChildren<Image>().color = buttonEditing;
    }

    public void SaveKeys() {        // Save keys to PlayerPrefs
        foreach (var key in keys) {
            PlayerPrefs.SetString(key.Key,key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public void BackButNoSave()
    {
        Debug.Log("NoSave was triggered");
        keys = backupKeys;
        int i = 0;
        foreach (var key in keys) {
            keyText[i].text = key.Key.ToString();
            i++;
        }
    }
}