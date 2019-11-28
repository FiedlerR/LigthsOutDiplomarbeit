using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    // Variablen für das Delay des "erkennen" des Spielers
    public float MaxReactionTime = 1.5f;
    [HideInInspector]
    public float reactionTime = 0f;

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
   // public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;

    public float hearRadius;
    public Transform player;

    void Awake() {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (reactionTime > MaxReactionTime) {                                                                           // damit der Spieler nicht länger gesehen wird als er wiklich wird (Chase funktioniert normal)
            reactionTime = MaxReactionTime;
        } else if (reactionTime < 0f) {
            reactionTime = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {

        //Debug.Log("reactionTime"+reactionTime+"/"+MaxReactionTime);
        //Spieler in Hör/Sichtbereich
        if (other.transform == player)
        {
            //Debug.Log(player.position);

            playerInSight = false;
            Vector3 direction = player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f) {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + Vector3.up *0.8f, direction.normalized, out hit, col.radius)) {
                    Debug.DrawRay(transform.position + Vector3.up*0.8f, direction.normalized, Color.green, 2, false);
                    if (hit.collider.transform == player)
                    {
                        playerInSight = true;
                        reactionTime += Time.deltaTime;                                                                     // Delay für die Erkennung des Spielers
                        if (reactionTime >= MaxReactionTime){
                
                            GetComponent<Guard>().setSeen(true, other.GetComponent<Transform>());
                        }   
                        return;
                    }
                    else
                    {
                        GetComponent<Guard>().setSeen(false, other.GetComponent<Transform>());
                    }

                }
            }
            if (!playerInSight) {
                reactionTime -= Time.deltaTime;                                                                             // Gegner "vergisst" Spieler
            if (calculatePathLength(player.position) <= hearRadius) //col.radius)
            {
                if (!other.GetComponent<PlayerMovement>().getIsSneaking())
                {
                    GetComponent<Guard>().setHeard(true, other.GetComponent<Transform>());
                    if (reactionTime <= MaxReactionTime/2) {
                        reactionTime = MaxReactionTime / 2;
                    }
                    //  Debug.Log("Player was heard");
                }
                else {
                    GetComponent<Guard>().setHeard(false, other.GetComponent<Transform>());
                }

            }
            else
            {
                GetComponent<Guard>().setHeard(false, other.GetComponent<Transform>());
            }
        }
        }
    } 

    void OnTriggerExit(Collider other)
    {
        //Spieler verlässt Hör/Sichtbereich
        if (other.transform == player) { 
            playerInSight = false;
           GetComponent<Guard>().setSeen(false);
           GetComponent<Guard>().setHeard(false);
        }
    }

    //Die Pfadlänge zum Ziel berechnen
    float calculatePathLength(Vector3 targetPosition) {

        NavMeshPath path = new NavMeshPath();
        if (nav.enabled) {
            nav.CalculatePath(targetPosition,path);
        }
        Vector3[] allWaypoints = new Vector3[path.corners.Length + 2];
        allWaypoints[0] = transform.position;
        allWaypoints[allWaypoints.Length - 1] = targetPosition;

        for (int i = 0; i <path.corners.Length;i++) {
            allWaypoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i <allWaypoints.Length-1;i++) {
            pathLength += Vector3.Distance(allWaypoints[i],allWaypoints[i+1]);
        }

        return pathLength;
    }



   
}
