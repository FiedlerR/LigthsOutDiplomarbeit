using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyst : MonoBehaviour
{
    private GameObject Player;
    private GameObject[] Enemies;

    private bool wasSeen = false;
    private bool usedViolence = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        seenCheck();
        killCheck();
    }

    private void seenCheck() { // checks if any Enemy has ever seen the player
        bool seen;
        foreach (GameObject enemy in Enemies) {
            seen = enemy.GetComponent<Guard>().m_wasSeen;
            if (seen) {
                wasSeen = seen;
            }
        }
    }

    private void killCheck() { // checks if any shootableEnemy has ever seen the player
        bool killed;
        foreach (GameObject enemy in Enemies)
        {
            killed = enemy.GetComponent<ShootableEnemy>().wasKilled;
            if (killed)
            {
                usedViolence = killed;
            }
        }
    }

    public bool evaluateStatus() { // return the result of the player actions
        if (!wasSeen && !usedViolence) {
            return true;
        }
        return false;
    }


}
