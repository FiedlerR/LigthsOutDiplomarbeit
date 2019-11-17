using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName ="Items/Weapon")]
public class Weapon : ScriptableObject {

    //UI
    public new string name;
    public Sprite invIcon;

    //DamageParams
    public float damagePerShot;
    public float shotsPerSecond;

    //Weaponcontrol
    public float XrecoilPerShot;
    public float YrecoilPerShot;
    public float reloadtime;

    //Ammomanagement
    public int clipSize;
    public int maxAmmo;

    //sound
    public AudioClip shotSound;
    public float shotVolume;
    public AudioClip reloadSound;
    public float reloadVolume;

    //animations & emitters
    public Animation shotAnim;
    public Animation reloadAnim;

    //Transform
    public Vector3 soundSource;
    public Transform shotSource;

    //functionParams
    private bool shootable = true;


    void reload() {
        shootable = false;
        AudioSource.PlayClipAtPoint(reloadSound, soundSource);
        reloadAnim.Play();
        if (! reloadAnim.IsPlaying(reloadAnim.name)) {
            shootable = true;
        }
    }

    void fire() {
        
    }
}
