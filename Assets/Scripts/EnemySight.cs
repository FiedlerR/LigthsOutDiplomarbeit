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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform == player)
        {

            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle* 0.5f) {
                RaycastHit hit;

                if (Physics.Raycast(transform.position,direction.normalized,out hit, col.radius)) {

                    if (hit.collider.transform == player)
                    {
                        playerInSight = true;
                        GetComponent<AI>().setSeen(true, other.GetComponent<Transform>());
                        Debug.DrawRay(transform.position, direction.normalized, Color.green, 2, false);
                       // Debug.Log("Player was seen");
                    }
                    else
                    {
                        GetComponent<AI>().setSeen(false, other.GetComponent<Transform>());
                    }

                }
            }
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

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player) { 
            playerInSight = false;
            GetComponent<AI>().setSeen(false);
            GetComponent<AI>().setHeard(false);
        }
    }

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
