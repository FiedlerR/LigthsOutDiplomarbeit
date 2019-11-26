using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * This is pretty simple
 * It connects the Ammo count from shootRaycastTriggerable and the UI Text/Sprites
 * its here because it didnt make sense to put it anywhere else
 */
public class AmmoUIConnector : MonoBehaviour
{
    public Text inClip;
    public Text currAmmo;
    public Image weaponIcon;
    private shootRaycastTriggerable triggerable;
    
    // Start is called before the first frame update
    void Start()
    {
        triggerable = GetComponentInChildren<shootRaycastTriggerable>();
        Debug.Log("inClip:" + triggerable.getAmmoinClip());
        Debug.Log("Current Ammo:" + triggerable.getCurrentAmmo());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        inClip.text = ""+ triggerable.getAmmoinClip();
        currAmmo.text = ""+ triggerable.getCurrentAmmo();
        weaponIcon.sprite = triggerable.GetWeapon().invIcon;
    }
}
