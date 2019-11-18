using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<Weapon> inventory;
    private int EquippedID = 0;

    // Start is called before the first frame update
    void Start()
    {
        inventory[EquippedID].isEquipped = true;
    }

    public void switchWeaponUP() {
        Equip(EquippedID++);
    }

    public void switchWeaponDown()
    {
        Equip(EquippedID--);
    }

    private void Equip(int id) {
        inventory[EquippedID].isEquipped = false;
        inventory[id].isEquipped = true;
        EquippedID = id;
    }
}
