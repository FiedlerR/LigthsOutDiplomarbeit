using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //Array der Gegner
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject> ();

    //UI referenzen
    [SerializeField]
    private GameObject image;
    [SerializeField]
    private GameObject instantiateHere = new GameObject();

    //Liste der existierenden indicators
    private List<Detector> detectorList = new List<Detector>();

    // Variablen für Winkelberechnung
    private GameObject player;
    private float angle;

    void Start() {
        //Alle Gegner finden
        //Alle Gegner finden
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        //Spieler finden
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < enemies.Count -1; i++) {
            detectorList.Add(new Detector(enemies[i], i, player, instantiateHere.transform.position, image)); // TODO: referance error here in runtime
        }
    }

    void Update() {
        // Durch Gegner iterieren
        for (int i = 0; i < enemies.Count; i++) {
            //Wenn Gegner Spieler sieht
            if (enemies[i].GetComponent<EnemySight>().playerInSight) {
                //Winkel berechnen
                detectorList[i].recalculateAngle();
                //Winkel drehen
                detectorList[i].rotateToAngle();
            }
        }
    }
    
    //Löschen von Indicator-gegner paar für outside-use (Gegner stirbt, ...) 
    public void deleteEnemyDetectorPair(int index) {
        enemies.RemoveAt(index);
        detectorList[index].deletePair();
    }
}
