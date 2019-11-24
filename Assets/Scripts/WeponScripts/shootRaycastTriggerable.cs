using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRaycastTriggerable : MonoBehaviour {

    [SerializeField] private Transform Weaponholder;
    //Damage/Range/Reload
    private float gDamage = 1;
    private float gFireRate = .25f;
    private float gReloadSpeed = 1.5f;
    private float gWeaponRange = 50f;
    //Recoil
    private float gRecoilSpeed = .5f;
    private float gRecoilMax = -20f;
    private float gRecoil = 0f;

    private float hitForce = 10f;
    private float nextFire;

    // Ammo Variablen
    private float gCurrentAmmo;
    private float gAmmoInClip;
    private float gClipSize;
    private float gMaxAmmo;

    private Camera fpsCam;
    private ShootableObj shootable;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextFire = Time.time - gFireRate;                                                       // Verhindert einen Fehler, der den Spieler daran hindert anzufangen zu schießen
    }

    public void Shoot() {
        if (Time.time >= nextFire) {                                                            // nextFire überprüfen
            if (gAmmoInClip <= 0)
            {
                Reload();
            }
            else {
                gAmmoInClip--;
                nextFire = Time.time + gFireRate;                                                   // Next Fire setzen um zu verhindern, dass jede Waffe so schnell schießen kann wie man will
                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));        // raycast Ursprung zum Zentrum des Bildschirms setzen
                ReactToRaycastHit(rayOrigin, fpsCam.transform.forward);
                HandleRecoil();
            }
        }
    }

    /*
     * AIs dont have a Camera so they cant use Shoot()
     * AIShoot() lets then comunicate thier own origin and direction
     */
    public void AIShoot(Vector3 rayOrigin, Vector3 shotDirection) {                             // Shoot but for AIs
        if (Time.time >= nextFire) {
            if (gAmmoInClip <= 0)
            {
                Reload();
            }
            else {
                gAmmoInClip--;
                nextFire = Time.time + gFireRate;
                ReactToRaycastHit(rayOrigin, shotDirection);
                HandleRecoil();
            }
        }
    }

    private void HandleRecoil() {
        if (gRecoil > 0)
        {
            var maxRecoil = Quaternion.Euler(gRecoilMax, 0, 0);                                // Dampen towards the target rotation
            Weaponholder.rotation = Quaternion.Slerp(Weaponholder.rotation, maxRecoil, Time.deltaTime * gRecoilSpeed);
            gRecoil -= Time.deltaTime;
        }
        else
        {
            gRecoil = 0;
            var minRecoil = Quaternion.Euler(0, 0, 0);                                          // Dampen towards the target rotation
            Weaponholder.rotation = Quaternion.Slerp(Weaponholder.rotation, minRecoil, Time.deltaTime * gRecoilSpeed / 2);
        }
    }

    private void ReactToRaycastHit(Vector3 rayOrigin, Vector3 shotDirection) {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, shotDirection, out hit, gWeaponRange)) {                 // Wenn der Raycast in Richtung fpsCam.transform.forward etwas trifft -> true
            Debug.Log("Hit detected");
            //hit


            if (hit.collider.GetComponent<ShootableEnemy>() != null) {                          // Wenn das getroffene Object eine Componente von ShootableEnemy hat -> true
                shootable = hit.collider.GetComponent<ShootableEnemy>();
            }
            else if (hit.collider.GetComponent<ShootableCollider>() != null) {                  // Wenn das getroffene Object eine Componente von ShootableCollider hat -> true
                shootable = hit.collider.GetComponent<ShootableCollider>();
            }
            else {                                                                              // Es wurde nichts Shootable getroffen also wird Shottable ein notShootable (Siehe notShottable für Erklärung)
                shootable = new NotShootable();
            }

            if (shootable != null) {                                                            // Check nach einem Shootable
                if (shootable.critHitbox == hit.collider)                                       // Check nach einem Headshot
                {
                    shootable.CriticalDamage(gDamage);
                }
                else {                                                                          // nur ein normaler Treffer
                    shootable.Damage(gDamage);
                }
            }
            if (hit.rigidbody != null) {                                                        // Check nach einem rigidbody für hitForce
                Debug.Log("rigidbody not null");
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else {                                                                                  //  kein Hit vorhanden
            Debug.Log("No hit detected");
        }
    }

    /*
     * Reload:
     * The Player does NOT loose any Bullets by reloading with a non-Empty mag (Quality of Life)
     */

    private void Reload() {                                                                     // Manages Reload Math of the Ammo values
        if (gClipSize > gAmmoInClip) {
            if (gCurrentAmmo < gClipSize - gAmmoInClip) {
                ReloadToRemaining();
            }
            else {
                ReloadToFullFlexible();
            }
        }
    }

    private void ReloadToRemaining() {                                                          // Manages Reload from any Point to Maximum Possible with remaining Bullets
        gAmmoInClip = gCurrentAmmo + gAmmoInClip;
        gCurrentAmmo = 0;
    }

    private void ReloadToFullFlexible() {                                                       // Manages Reloads from anyPoint to Full
        if (gCurrentAmmo > gClipSize - gAmmoInClip) {
            gCurrentAmmo -= gClipSize - gAmmoInClip;
            gAmmoInClip = gClipSize;
        }
    }

    public void SetWeapon(Weapon weapon) {                                                      // Werte der angegebenen Waffe setzen
        gRecoil = weapon.recoil;
        gRecoilMax = weapon.XrecoilMax;
        gDamage = weapon.damagePerShot;
        gFireRate = weapon.shotsPerSecond;
        gReloadSpeed = weapon.reloadtime;
        gWeaponRange = weapon.damagePerShot;
        gRecoilSpeed = weapon.recoilSpeed;

        gMaxAmmo = weapon.maxAmmo;
        gClipSize = weapon.clipSize;
    }
}
