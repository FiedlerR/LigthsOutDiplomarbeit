using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector
{
    // Objecte in in scene
    private GameObject enemy; //Verbundener Gegner (1 Detector pro Gegner)
    private GameObject player;
    private Vector3 instantiatePos;

    //Für Vergleich von Duplicates
    private int enemyID;

    // Referances
    private GameObject publicReference; //UI Element, dass als Initierreferenz benutzt wird 

    // instanzeigene kopie von referance
    private GameObject personalImage; 

    // Für Berechnung
    private float angle; // Winkel zu Gegner (Player <-> Enemy)


    // -- Initalisierung --
    public Detector(GameObject enemy, int enemyID, GameObject player, Vector3 instantiatePos, GameObject publicReference) {
        this.enemy = enemy;
        this.enemyID = enemyID;
        this.player = player;
        this.instantiatePos = instantiatePos;
        this.publicReference = publicReference;

        instantiateImage(); // instantiert eine kopie von referance bei angegebener Position und rotation
        recalculateAngle(); // berechnet Winkle zwischen Player und Enemy
        rotateToAngle();    // rotiert personalImage um berechneten Winkel
    }


    // -- Berechnung --
    public void recalculateAngle() {
        //Winkelberechnung vorbereiten

        //Richtung berechnen
        Vector3 direction = enemy.GetComponent<Transform>().position - player.transform.position;

        //Winkel berechnen
        //Arctan von e.z & e.x -> conversion zu deg
        angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
    }

    public void rotateToAngle() { //Objekt drehen
        //berechnung vorbereiten
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        //Rotation um Achse Vector3.forward bei Winkel angle
        personalImage.transform.rotation = Quaternion.Slerp(personalImage.transform.rotation, angleAxis, 1);
    }

    public int getEnemyID() { // returnt GegnerID dieser Instanz
        return enemyID;
    }

    public GameObject getEnemy() { // returnt verbundener Gegner dieser Instanz
        return enemy;
    }

    public bool hasEnemy (int id){ // returnt true wenn diese instanz einen Gegner hat (für furchtbares Debugging)
        return id == enemyID;
    }

    private void instantiateImage() {
        personalImage = GameObject.Instantiate(publicReference, instantiatePos, new Quaternion()); // instantiert eine kopie von referance bei angegebener Position und rotation
    }

    public void deletePair() {      // deletes indicator Enemy pair 
        GameObject.Destroy(personalImage);
        GameObject.Destroy(enemy);
    }
}