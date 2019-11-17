using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    //Properties every Ability has to have
    public string aName = "Ablilityname";
    public Sprite UiSprite;
    public AudioClip soundclip;
    public float baseCooldown = 1f;


    //Abstract functions
    public abstract void Initialize(GameObject obj);
    public abstract void triggerAbility();
}
