using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *ShootableEnemy
 * 
 * Ein Shootable Enemy ist ein Objekt, dass ebenfalls ein Gegner ist.
 * Das also mit Animationen/Aktionen auf diesen Treffer reagieren soll.
 * Dieses Script gibt dem Gegner: 
 * -Variablen
 *  +MaximumHealth
 *  +MaxHealth
 *  +currentHealth
 * -Methoden
 *  +Damage (um Treffer zu handeln)
 *  +Die (um den Tod zu handeln)
 * 
*/

public class ShootableEnemy : ShootableObj
{
    public float maximumHealth = 100f;  // eine Variable die einen Editor das Maximumleben für jeden Gegner spezifisch setzen lässt.
    public bool wasKilled = false;

    private void Start() { // Zuweisungen um das Startelben und das maximale Leben gleich zu setzen
        MaxHealth = maximumHealth;
        currentHealth = MaxHealth;
    }

    public override void Damage(float damageAmount) { //Der Schaden, den dieser Gegner nimmt wird vom Leben abezogen und wenn <= 0 wird der Tod hervorgerufen
        currentHealth -= damageAmount;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public override void CriticalDamage(float damageAmount) { //Wie Damage aber mit headshotMult für Nutzung in Kombination mit headHitbox
        Damage(damageAmount * headshotMult);
    }

    public override void Die() { //Tote Gegner aktivieren noch sachen nach ihrem Tod Bsp:(Animation, ragdoll, etc. )
        wasKilled = true;
        //Do death stuff here
        gameObject.SetActive(false);
    }
}
