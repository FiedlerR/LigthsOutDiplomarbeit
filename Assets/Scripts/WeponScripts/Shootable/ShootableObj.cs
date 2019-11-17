using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ShootableObj
 * 
 * Ein Shottable Object ist ein Object in der Spielwelt, dass auf einen Spielershuss reagieren soll(in der Regel -> Ausnahme notShottable)
 * Es teilt sich ein in: 
 * - Shootable Collider
 * - Shootable Enemy
 * - Not Shootable
 * 
 * Spezifische Beschreibungen in den einzelnen Klassen
 */

public abstract class ShootableObj : MonoBehaviour
{
    [HideInInspector] public float MaxHealth; // Eine MaxHealth Variable die von den einzelen Teilklassen gesetz wird
    [HideInInspector] public float currentHealth;

    public abstract void Damage(float damageAmount); // Eine abstrakte Klasse die in den einzelnen Kindern gehandelt werden soll

    public abstract void Die(); // Eine abstrakte Klasse die in den einzelnen Kindern gehandelt werden soll
}
