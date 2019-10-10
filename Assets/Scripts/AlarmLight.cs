using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public float highIntensity = 20f;
    public float lowIntensity = 0.5f;
    public bool alarmOn;
    public Light alarmLight;

    void start(){
        alarmLight = GetComponent<Light>();
    }

    void Update()
    {
        if (alarmOn)
        {
            alarmLight.intensity += fadeSpeed * Time.deltaTime;
            if (fadeSpeed > 0 && alarmLight.intensity > highIntensity || fadeSpeed < 0 && alarmLight.intensity < lowIntensity)
            {
                fadeSpeed = -fadeSpeed;
            }
        }
        else {
            alarmLight.intensity = 0f;
        } 
    }

}
