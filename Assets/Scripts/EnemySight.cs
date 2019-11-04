using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
   // public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    public Transform player;

    void Awake() {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider other)
    {

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
                        GetComponent<AI>().setSeen(true, other.GetComponent<Transform>());
                        //Debug.Log("Player was seen");
                        return;
                    }
                    else
                    {
                        GetComponent<AI>().setSeen(false, other.GetComponent<Transform>());
                    }

                }
            }
            if (!playerInSight) {
            if (calculatePathLength(player.position) <= col.radius)
            {
                if (!other.GetComponent<PlayerMovement>().getIsSneaking())
                {
                    GetComponent<AI>().setHeard(true, other.GetComponent<Transform>());
                    //  Debug.Log("Player was heard");
                }
                else {
                    GetComponent<AI>().setHeard(false, other.GetComponent<Transform>());
                }

            }
            else
            {
                GetComponent<AI>().setHeard(false, other.GetComponent<Transform>());
            }
        }
        }
    } 

    void OnTriggerExit(Collider other)
    {
        //Spieler verlässt Hör/Sichtbereich
        if (other.transform == player) { 
            playerInSight = false;
            GetComponent<AI>().setSeen(false);
            GetComponent<AI>().setHeard(false);
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
