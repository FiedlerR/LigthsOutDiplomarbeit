using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingChanger : MonoBehaviour
{
    // For applying the vignette
    private PostProcessVolume ppVolume;
    private Vignette ppSneakVignette;
    public PlayerMovement movement;
    public float VignetteIntensityonSneak = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        ppVolume.profile.TryGetSettings(out ppSneakVignette);
        //ppSneakVignette.intensity.value = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // Debug.Log("isSneaking:" + movement.getIsSneaking());
        if (movement.getIsSneaking())
        {
            ppSneakVignette.intensity.value = VignetteIntensityonSneak; // Vignette is active while sneaking
        }
        else {
            ppSneakVignette.intensity.value = 0f; // Vignette is inactive while not sneaking
        }
    }
}
