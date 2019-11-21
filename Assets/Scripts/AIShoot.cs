using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour {

    public float reactionTime = 0.45f; // Delay for the Guard to react when the player enters/leaves his FOV for him to shoot
    public Transform shotOrigin;

    private shootRaycastTriggerable shootTriggerable;
    private Guard guard; // To get last seen transform
    private Vector3 p_lastLoc;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        shootTriggerable = GetComponent<shootRaycastTriggerable>();
    }

    // Update is called once per frame
    void Update() {
        float guardSearch = guard.getSearchCounter() - reactionTime;

        if (guard.getWasSeen() && guardSearch < 0 && guardSearch != 0 - reactionTime) { //getSearchCounter ist Standardmäßg 0, desswegen != 0 - shootdelay damit der Guard nicht immer shießt
            //Spieler wurde von Guard gesehen
            p_lastLoc = guard.getLastPlayerV3(); // lastLocation setzen
            StartCoroutine("TriggerShoot");
        } else if (guardSearch > 0 && isShooting) {
            StopCoroutine("TriggerShoot");
        }
    }

    private IEnumerator TriggerShoot() {
        isShooting = true;
        shootTriggerable.AIShoot(shotOrigin.position, p_lastLoc); //AIshoot at last player Location from you Position
        yield return null;                      // Needs to return something 
    }
}
