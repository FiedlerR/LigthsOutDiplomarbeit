using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *ShootableCollider
 * 
 * Ein Shootable Collider ist ein Shootable, dass KEIN Gegner ist
 * Das also mit Phsysics auf diesen Treffer reagieren soll.
 * Z.B. eine Box, die Bricht oder sich bewegt wenn sie angeschossen wird
 * 
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

public class ShootableCollider : ShootableObj
{

    public float maximumHealth = 10f; // eine Variable die einen Editor das Maximumleben für jeden Gegner spezifisch setzen lässt.

    private void Start() { // Zuweisungen um das Startelben und das maximale Leben gleich zu setzen
        MaxHealth = maximumHealth;
        currentHealth = MaxHealth;
    }

    public override void Damage(float damageAmount) { //Der Schaden, den dieser Collider nimmt wird vom Leben abezogen und wenn <= 0 wird der Tod hervorgerufen
        currentHealth -= damageAmount;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public override void Die() { //Tote Collider aktivieren noch sachen nach ihrem Tod Bsp:(Zerspringen, Particles, etc. )
        //death stuff
        gameObject.SetActive(false);
    }

    public override void CriticalDamage(float damageAmount){} //Option to give a critical spot 
}
