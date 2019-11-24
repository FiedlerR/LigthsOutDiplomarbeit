using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public InventoryItem[] items;
    private int EquippedID = 0;

    // Start is called before the first frame update
    void Start()
    {
        items[EquippedID].setActive();
    }

    public void switchWeaponUP() {
        Equip(EquippedID++);
    }

    public void switchWeaponDown()
    {
        Equip(EquippedID--);
    }

    private void Equip(int id) {
        items[EquippedID].setInactive();
        items[id].setActive();

        EquippedID = id;
    }
}
