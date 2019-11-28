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
    public Button[] keyButton;
    private Text[] keyText;

    private GameObject currentKey;

    public Color32 buttonNormal;
    public Color32 buttonEditing;

    private void Awake()
    {
        keys = GameObject.FindObjectOfType<InputManager>().keys;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < keyButton.Length; i++) {
            keyText[i] = keyButton[i].transform.GetComponentInChildren<Text>();
        }

        // set Button text acording to set Buttons
        int j = 0;
        foreach (var key in keys) {
            keyText[j].text = key.Key.ToString();
            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnGUI()
    {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey) {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = buttonNormal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) { //Trigger for OnGUI to run
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = buttonNormal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = buttonEditing;
    }

    public void SaveKeys() {        // Save keys to PlayerPrefs
        foreach (var key in keys) {
            PlayerPrefs.SetString(key.Key,key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}