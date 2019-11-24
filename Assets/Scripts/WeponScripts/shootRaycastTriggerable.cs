using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRaycastTriggerable : MonoBehaviour {
    
    [SerializeField] private Transform Weaponholder;
    private Weapon gun;

    private float hitForce = 10f;
    private float nextFire;

    // Ammo Variablen
    private float gCurrentAmmo;
    private float gAmmoInClip;

    private Camera fpsCam;
    private ShootableObj shootable;

    // Start is called before the first frame update
    void Start()
    {
        gun.damagePerShot = 1;
        gun.shotsPerSecond = .25f;
        gun.reloadtime = 1.5f;
        gun.range = 50f;

        gun.recoilSpeed = .5f;
        gun.XrecoilMax = -20f;
        gun.recoil = 0f;

        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextFire = Time.time - gun.shotsPerSecond;                                                      // Verhindert einen Fehler, der den Spieler daran hindert anzufangen zu schießen
    }

    public void Shoot() {
        if (Time.time >= nextFire) {                                                                    // nextFire überprüfen
            if (gAmmoInClip <= 0)
            {
                Reload();
            }
            else {
                gAmmoInClip--;
                nextFire = Time.time + gun.shotsPerSecond;                                              // Next Fire setzen um zu verhindern, dass jede Waffe so schnell schießen kann wie man will
                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));            // raycast Ursprung zum Zentrum des Bildschirms setzen
                ReactToRaycastHit(rayOrigin, fpsCam.transform.forward);
                HandleRecoil();
            }
        }
    }

    /*
     * AIs dont have a Camera so they cant use Shoot()
     * AIShoot() lets then comunicate thier own origin and direction
     */
    public void AIShoot(Vector3 rayOrigin, Vector3 shotDirection) {                                     // Shoot but for AIs
        if (Time.time >= nextFire) {
            if (gAmmoInClip <= 0)
            {
                Reload();
            }
            else {
                gAmmoInClip--;
                nextFire = Time.time + gun.shotsPerSecond;
                ReactToRaycastHit(rayOrigin, shotDirection);
                HandleRecoil();
            }
        }
    }

    private void HandleRecoil() {
        if (gun.recoil > 0)
        {
            var maxRecoil = Quaternion.Euler(gun.XrecoilMax, 0, 0);                                     // Dampen towards the target rotation
            Weaponholder.rotation = Quaternion.Slerp(Weaponholder.rotation, maxRecoil, Time.deltaTime * gun.recoilSpeed);
            gun.recoil -= Time.deltaTime;
        }
        else
        {
            gun.recoil = 0;
            var minRecoil = Quaternion.Euler(0, 0, 0);                                                  // Dampen towards the target rotation
            Weaponholder.rotation = Quaternion.Slerp(Weaponholder.rotation, minRecoil, Time.deltaTime * gun.recoilSpeed / 2);
        }
    }

    private void ReactToRaycastHit(Vector3 rayOrigin, Vector3 shotDirection) {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, shotDirection, out hit, gun.range)) {                            // Wenn der Raycast in Richtung fpsCam.transform.forward etwas trifft -> true
            Debug.Log("Hit detected");
            //hit


            if (hit.collider.GetComponent<ShootableEnemy>() != null) {                                 // Wenn das getroffene Object eine Componente von ShootableEnemy hat -> true
                shootable = hit.collider.GetComponent<ShootableEnemy>();
            }
            else if (hit.collider.GetComponent<ShootableCollider>() != null) {                         // Wenn das getroffene Object eine Componente von ShootableCollider hat -> true
                shootable = hit.collider.GetComponent<ShootableCollider>();
            }
            else {                                                                                     // Es wurde nichts Shootable getroffen also wird Shottable ein notShootable (Siehe notShottable für Erklärung)
                shootable = new NotShootable();
            }

            if (shootable != null) {                                                                   // Check nach einem Shootable
                if (shootable.critHitbox == hit.collider)                                              // Check nach einem Headshot
                {
                    shootable.CriticalDamage(gun.damagePerShot);
                }
                else {                                                                                 // nur ein normaler Treffer
                    shootable.Damage(gun.damagePerShot);
                }
            }
            if (hit.rigidbody != null) {                                                               // Check nach einem rigidbody für hitForce
                Debug.Log("rigidbody not null");
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else {                                                                                         //  kein Hit vorhanden
            Debug.Log("No hit detected");
        }
    }

    /*
     * Reload:
     * The Player does NOT loose any Bullets by reloading with a non-Empty mag (Quality of Life)
     */

    private void Reload() {                                                                            // Manages Reload Math of the Ammo values
        if (gun.clipSize > gAmmoInClip) {

            if (gCurrentAmmo < gun.clipSize - gAmmoInClip) {
                ReloadToRemaining();
            }
            else {
                ReloadToFullFlexible();
            }
        }
    }

    private void ReloadToRemaining() {                                                                 // Manages Reload from any Point to Maximum Possible with remaining Bullets
        gAmmoInClip = gCurrentAmmo + gAmmoInClip;
        gCurrentAmmo = 0;
    }

    private void ReloadToFullFlexible() {                                                              // Manages Reloads from anyPoint to Full
        if (gCurrentAmmo > gun.clipSize - gAmmoInClip) {
            gCurrentAmmo -= gun.clipSize - gAmmoInClip;
            gAmmoInClip = gun.clipSize;
        }
    }

    public void SetWeapon(Weapon weapon) {                                                             // Werte der angegebenen Waffe setzen
        gun = weapon;
    }

    public Weapon GetWeapon() {
        return gun;
    }

    public float getAmmoinClip (){
        return gAmmoInClip;
    }

    public float getCurrentAmmo()
    {
        return gCurrentAmmo;
    }
}
