using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Inventory;

    public void Start() {
        //Inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    public void EnterInventory() {
        Inventory.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitInventory() {
        Inventory.SetActive(false);
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
