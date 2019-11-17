using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability/PassiveAbility")]
/*
 Eine passiv-Fähigkeit ist eine aktivierbare Fähigkeit, die dem Spieler verschiedene Boni über einen gewissen zeitraum gibt. (Geschwindigkeit, leiseres bewegen , ...)
*/
public class PassiveAbility : Ability
{
    public GameObject player;
    public Weapon gun;

    //speedfaktoren
    public float pWalkSpeedMult = 1f; // leitern zu klettern Skaliet mit walkspeed
    public float pRunSpeedMult = 1f;
    public float pSneakSpeedMult = 1f;

    //jump
    public float pJumpMultiplier = 1f;

    // --- gunscript ---
    public float gDamageMult = 1f;
    public float gFireRateMult = 1f;
    public float gRecoilMult = 1f;
    public float gReloadTimeMult = 1f;

    public float aDuration = 1f;

    //Buff
    private BuffPlayerTriggerable buff = new BuffPlayerTriggerable();

    public override void Initialize(GameObject obj)
    {
        buff = obj.GetComponent<BuffPlayerTriggerable>();

        buff.pMovement = player.GetComponent<PlayerMovement>();
        buff.pRunSpeedMult = pRunSpeedMult;
        buff.pWalkSpeedMult = pWalkSpeedMult;
        buff.pSneakSpeedMult = pSneakSpeedMult;

        buff.pJumpMultiplier = pJumpMultiplier;

        buff.gun = gun;

        buff.gDamageMult = gDamageMult;
        buff.gFireRateMult = gFireRateMult;
        buff.gRecoilMult = gRecoilMult;
        buff.gReloadTimeMult = gReloadTimeMult;
        buff.aDuration = aDuration;
        
        buff.setBackup();
    }

    public override void triggerAbility()
    {
        buff.activateAbility();
    }
}
