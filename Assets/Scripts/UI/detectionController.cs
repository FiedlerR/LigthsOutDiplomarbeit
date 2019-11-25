using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectionController : MonoBehaviour
{
    public Color32 startColor;
    public Color32 endColor;
    public Color32 alertColor;
    [SerializeField]
    private Image fillArea;
    private Canvas canvas;
    private EnemySight enemySight;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySight = GetComponent<EnemySight>();
        canvas = gameObject.GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        fillArea.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDetection();
        canvas.gameObject.transform.LookAt(player.transform); // UI is always visible for player if he can see unit
    }

    private void UpdateDetection() {                          // Lerps Colors using the reaction time => how close you are to being detected
        if (fillArea.fillAmount >= 0) {
            if (fillArea.fillAmount == 1) {
                fillArea.color = alertColor;
            }
            canvas.enabled = true;
            fillArea.color = Color.Lerp(startColor,endColor, enemySight.MaxReactionTime/enemySight.reactionTime);
        }
        else {
            canvas.enabled = false;
        }
    }
}
