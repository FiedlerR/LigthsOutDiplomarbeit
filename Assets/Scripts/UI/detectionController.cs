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
        canvas = FindObjectOfType<Canvas>();
        Debug.Log("Canvas:" + canvas.name);
        canvas.gameObject.SetActive(false);
        fillArea.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInSight();
        //UpdateTimetoForgetAggro();
        UpdateDetection();
        canvas.gameObject.transform.LookAt(player.transform); // UI is always visible for player if he can see unit
    }

    private void UpdateDetection() {                          // Lerps Colors using the reaction time => how close you are to being detected
        fillArea.fillAmount = enemySight.timeToReact / enemySight.MaxReactionTime;
        if (fillArea.fillAmount >= 0f) {
            if (fillArea.fillAmount == 1) {
                fillArea.color = alertColor;
            }
            else {
                canvas.gameObject.SetActive(true);
                fillArea.color = Color.Lerp(startColor, endColor, enemySight.timeToReact / enemySight.MaxReactionTime);
                //Debug.Log("Reaction Time: "+ enemySight.reactionTime);
            }
        }
        else {
            canvas.gameObject.SetActive(false);
        }
    }

    private void UpdateInSight() {                                                                          // Hält den Gegner davon ab fertig zu suchen, bevor er den Spieler sieht
        if (enemySight.playerInSight)
        {
            enemySight.timeToReact += Time.deltaTime;
        }
        else/* if (enemySight.timeToForgetAggro <= 0)*/                                                        // Hat der Gegner seine Aggro auf den Spieler schon vergessen?
        {
            enemySight.timeToReact -= Time.deltaTime;                                                      // Gegner vergisst Spieler komplett
        }
    }
}
