using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     * NotShootables:
     * 
     * DO NOT USE IN INSPECTOR. THIS IS ONLY USED BY SCRIPT.
     * 
     * notShootables sind Objekte, die NICHT im Inspector gesetzt werden sollten!
     * Es handelt sich lediglich um einen Workaround um 100% sicher zu gehen, dass der Spieler nicht die kompletter spielwelt erschießen kann
     * Es sollte ohne diesem Workaround auch nicht funktionieren aber sicher ist sicher
     * 
     * Warum?
     * Bei einem Schuss des Spielers wird ein ShootableObj erwartet. 
     * Wenn er ein Object trifft, dass nicht Shottable (nicht auf den Spielershuss reagieren soll) ist, verhindert diese Klasse das auftreten eines Fehlers.
*/

public class notShootable : ShootableObj
{
    
    public override void Damage(float damageAmount) {
        //Nothing to do here
    }

    public override void Die() {
        //Nothing to do here
    }
}
