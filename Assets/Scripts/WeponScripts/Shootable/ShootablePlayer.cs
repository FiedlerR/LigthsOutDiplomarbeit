using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shootable player ist wie shootable enemy aber hat noch eine UI referenz für das Leben

public class ShootablePlayer : ShootableObj
{
    public float maximumHealth = 100f;  // eine Variable die einen Editor das Maximumleben für jeden Gegner spezifisch setzen lässt.
    public Image healthbar;
    public Image healthFade;
    private float reactionTime;
    private float shrinkSpeed = 1f;

    private void Start()
    { // Zuweisungen um das Startelben und das maximale Leben gleich zu setzen
        MaxHealth = maximumHealth;
        currentHealth = MaxHealth;
        healthFade.fillAmount = healthbar.fillAmount;
    }

    public void Update()
    {
        healthbar.fillAmount = MaxHealth / currentHealth;
        reactionTime -= Time.deltaTime;

        if (reactionTime < 0) { // visual effect that makes the taken damage more visible
            if (healthbar.fillAmount < healthFade.fillAmount) {
                healthFade.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    public override void Damage(float damageAmount)
    { //Der Schaden, den dieser Gegner nimmt wird vom Leben abezogen und wenn <= 0 wird der Tod hervorgerufen
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    

    public override void CriticalDamage(float damageAmount)
    { //Wie Damage aber mit headshotMult für Nutzung in Kombination mit headHitbox
        Damage(damageAmount * headshotMult);
    }

    public override void Die()
    { //Tote Gegner aktivieren noch sachen nach ihrem Tod Bsp:(Animation, ragdoll, etc. )
        //Do death stuff here
        //send to restart/last save
        gameObject.SetActive(false);
    }
}
