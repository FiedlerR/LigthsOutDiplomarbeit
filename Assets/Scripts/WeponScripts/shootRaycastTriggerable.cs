using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRaycastTriggerable : MonoBehaviour {

    [HideInInspector] public float gDamage = 1;
    [HideInInspector] public float gFireRate = .25f;
    [HideInInspector] public float gReloadSpeed = 1.5f;
    [HideInInspector] public float gWeaponRange = 50f;
    private float hitForce = 10f;
    private Camera fpsCam;

    private float nextFire;

    private ShootableObj shootable;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextFire = Time.time - gFireRate;                                                       //To fix a bug to be able to start shooting 
    }

    public void Shoot(){
        if (Time.time > nextFire) {
            nextFire = Time.time + gFireRate;                                                   // Next Fire setzen um zu verhindern, dass jede Waffe eine MG ist
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));        //raycast Ursprung zum Zentrum des Bildschirms setzen
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, gWeaponRange)) {  // Wenn der Raycast in Richtung fpsCam.transform.forward etwas trifft -> true
                Debug.Log("Hit detected");
                //hit


                if (hit.collider.GetComponent<ShootableEnemy>() != null) {                      // Wenn das getroffene Object eine Componente von ShootableEnemy hat -> true
                    shootable = hit.collider.GetComponent<ShootableEnemy>();
                }
                else if (hit.collider.GetComponent<ShootableCollider>() != null) {              // Wenn das getroffene Object eine Componente von ShootableCollider hat -> true
                    shootable = hit.collider.GetComponent<ShootableCollider>();
                }
                else {                                                                          // Es wurde nichts Shottable getroffen also wird Shottable ein notShootable (Siehe notShottable für Erklärung)
                    shootable = new notShootable();
                }

                if (shootable != null) {
                    Debug.Log("shootable not null");
                    shootable.Damage(gDamage);
                }
                if (hit.rigidbody != null) {
                    Debug.Log("rigidbody not null");
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else {
                //no hit
                Debug.Log("No hit detected");
            }
        }
    }
}
