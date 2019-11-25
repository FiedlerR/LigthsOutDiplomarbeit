using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * InventoryItems
 * Verbindet eine Weapon mit ihrem GameObject um eine einfache Verbindung zwischen den beiden Objekten zu schaffen
 */

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Items/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public Weapon weapon;
    public GameObject weaponObject;

    public void setActive() { // Aktiviert alle verbundenen Objekte
        weapon.isEquipped = true;
        weaponObject.SetActive(true);
    }

    public void setInactive() { // Deaktiviert alle verbundenen Objekte
        weapon.isEquipped = false;
        weaponObject.SetActive(false);
    }
}
