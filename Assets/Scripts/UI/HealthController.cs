using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    public Image healthBar;
    public Image upperCap;
    public Color32 startColor;
    public Color32 endColor;
    public float maxHealth = 100f;
    private float currHealth;

	// Use this for initialization
	void Start () {
        healthBar.color = startColor;
        currHealth = maxHealth;
        
        //InvokeRepeating("DecreaseHealth", 1f, 1f);
	}

    void DecreaseHealth() {
        if (currHealth <= 0)
        {
            currHealth = maxHealth;
        }
        //currHealth -= 10f;
        calcUIFill();
    }

    void calcUIFill() {
        float calcHealth = currHealth / maxHealth;
        healthBar.fillAmount = calcHealth;
        healthBar.color = Color.Lerp(endColor, startColor, calcHealth);
    }
}
