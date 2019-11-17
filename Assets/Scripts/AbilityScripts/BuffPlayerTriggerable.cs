using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPlayerTriggerable : MonoBehaviour {

    // --- player script ---
    [HideInInspector] public PlayerMovement pMovement;
    private PlayerMovement playerBackup;

    //speedfaktoren
    [HideInInspector] public float pWalkSpeedMult = 1; // leitern zu klettern Skaliet mit walkspeed
    [HideInInspector] public float pRunSpeedMult = 1;
    [HideInInspector] public float pSneakSpeedMult = 1;

    //jump
    [HideInInspector] public float pJumpMultiplier = 1;

    // --- gunscript ---
    [HideInInspector] public float gDamageMult = 1;
    [HideInInspector] public float gFireRateMult = 1;
    [HideInInspector] public float gRecoilMult = 1;
    [HideInInspector] public float gReloadTimeMult = 1;

    [HideInInspector] public float aDuration = 1;

    //weapon referance
    [HideInInspector] public Weapon gun;
    private Weapon gunBackup;

    public void activateAbility() {
        StartCoroutine(useBuff());
    }

    IEnumerator useBuff() {
        startBuff();
        yield return new WaitForSecondsRealtime(aDuration);
        endBuff();
    }

    private void startBuff() {
        //Movement buffs
        pMovement.walkSpeed *= pWalkSpeedMult;
        pMovement.runSpeed *= pRunSpeedMult;
        pMovement.sneakSpeed *= pSneakSpeedMult;

        pMovement.jumpMultiplier *= pJumpMultiplier;

        //Gun buffs
        gun.damagePerShot *= gDamageMult;
        gun.shotsPerSecond *= gFireRateMult;
        gun.XrecoilPerShot *= gRecoilMult;
        gun.YrecoilPerShot *= gRecoilMult;
        gun.reloadtime *= gReloadTimeMult;
    }

    private void endBuff() {
        //movement buffs
        pMovement.walkSpeed = playerBackup.walkSpeed;
        pMovement.runSpeed = playerBackup.runSpeed;
        pMovement.sneakSpeed = playerBackup.sneakSpeed;

        pMovement.jumpMultiplier = playerBackup.jumpMultiplier;

        //Gun buffs
        gun.damagePerShot = gunBackup.damagePerShot;
        gun.shotsPerSecond = gunBackup.shotsPerSecond;
        gun.XrecoilPerShot = gunBackup.XrecoilPerShot;
        gun.YrecoilPerShot = gunBackup.YrecoilPerShot;
        gun.reloadtime = gunBackup.reloadtime;
    }

    public void setBackup() {
        playerBackup = pMovement;
        gunBackup = gun;
    }
}