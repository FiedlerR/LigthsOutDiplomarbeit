using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour {

    public List<Weapon> inventory;
    public GameObject Parent;
    public GameObject UIPrefab;

    // Start is called before the first frame update
    public void Start(){
        foreach (Weapon weapon in inventory) {
            AddInstance(weapon);
        }
    }

    private void AddInstance(Weapon weapon) {
        GameObject instance = Instantiate(UIPrefab) as GameObject;

        instance.gameObject.GetComponent<Image>().sprite = weapon.invIcon;
        instance.gameObject.GetComponentInChildren<Text>().text = weapon.name;

    }

}
