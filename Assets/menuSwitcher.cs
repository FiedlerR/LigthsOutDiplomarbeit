using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * MenuSwitcher
 * Der MenuSwitcher hat die Aufgabe zwischen verschiedenen Pausenmenu-Seiten zu wechseln
 */
public class menuSwitcher : MonoBehaviour
{
    public GameObject enableThis;
    public GameObject disableThis;

    public void SwitchMenu() {
        disableThis.SetActive(false);
        enableThis.SetActive(true);
    }

    public void closeMenu() {
        disableThis.SetActive(false);
    }
}
