using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{
    public string abilityButtonAxisName = "Fire2";
    public Image darkMask;
    public Text cooldownTextDisplay;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject AbilityHandler;

    private Image myButtonImage;
    private AudioSource abilitySource;

    private float coolDowDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Ability selectedAbility) {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.UiSprite;
        darkMask.sprite = ability.UiSprite;
        coolDowDuration = ability.baseCooldown;

        ability.Initialize(AbilityHandler);
        AbilityReady();
    }

    // Update is called once per frame
    void Update() {

        bool cooldownComplete = (Time.time > nextReadyTime);
        if (cooldownComplete) {
            AbilityReady();
            if (Input.GetButtonDown(abilityButtonAxisName)) {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady() { // ui toggle wenn bereit
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown() { // was passiert jeden Frame wenn auf cooldown
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Round(coolDownTimeLeft);
        cooldownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDowDuration);
    }

    private void ButtonTriggered() { // wenn ability aktiviert wird
        nextReadyTime = coolDowDuration + Time.time;
        coolDownTimeLeft = coolDowDuration;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;

        //abilitySource.clip = ability.soundclip;
        //abilitySource.Play();
        ability.triggerAbility();
    }
}
