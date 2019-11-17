using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    //binding navMeshAgent from AI Unit
    public NavMeshAgent navMeshAgent;

    //array of waypoints GameObject
    public Transform[] waypoints;


    // public int[] waypointType;



    public enum WaypointType
    {
        waitForCertainTime,
        waitForGuard,
        goToWaitingGuard
    }

    public WaypointType [] waypointType;
    //
    public float[] waypointTime;

    //Zeit die an einen waypoint gewartet wird
    //public float timeToWait;

    //legt fest ob die Patrolliere aktiv ist
    public bool isPatrolActiv;

    //Auf welches Objekt schaut der Bot am Waypoint
    public Transform[] lookToWhileWaiting;

    // index for the current waypoint
    int m_CurrentWaypointIndex;

    //Timer für das warten
    float m_WaitingTimer;

    //Referenz zum Animator
    Animator m_Animator;

    //
    public bool isWaiting = false;

    void Start()
    {


        //set first destination waypoint to 0
       /* if (waypoints.Length != 0)
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }*/
        ///
        m_Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isPatrolActiv && waypoints.Length != 0)
        {
            switch (waypointType[m_CurrentWaypointIndex])
            {
                case WaypointType.waitForCertainTime:
                    patrol();
                    break;
                case WaypointType.waitForGuard:
                    waitForAnotherBot();
                    break;
                case WaypointType.goToWaitingGuard:
                    pickUpWaitingBot();
                    break;

            }
        }
     
    }

    private void pickUpWaitingBot()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && waypoints[m_CurrentWaypointIndex].transform.gameObject.GetComponent<WaypointPatrol>().isWaiting)
        {
            waypoints[m_CurrentWaypointIndex].transform.gameObject.GetComponent<WaypointPatrol>().setIsWaiting(false);
            //calculate the current waypoint index
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        }
        else {
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    private void waitForAnotherBot()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            isWaiting = true;
        }
    }

    void patrol() {

            //   
            if (lookToWhileWaiting.Length != 0 && lookToWhileWaiting[m_CurrentWaypointIndex] == null && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + navMeshAgent.speed)
            {
                Vector3 direction = (lookToWhileWaiting[m_CurrentWaypointIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
            }


            //check if the unit arrived the waypoint
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                //

                // m_Animator.SetBool("isWalking", false);
                // m_Animator.SetBool("isStanding", true);

                //
                m_WaitingTimer += Time.deltaTime;

                //
                if (!isStillMoving())
                {
                    //
                    //  m_Animator.SetBool("isWalking", true);
                    //m_Animator.SetBool("isStanding", false);


                    //
                    m_WaitingTimer = 0;
                    //calculate the current waypoint index
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

                    //use the calculated waypoint to set a new destination
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }

            }
        
    }


    private bool isStillMoving()
    {
        return m_WaitingTimer < waypointTime[m_CurrentWaypointIndex];
    }

    public void setIsPatrolActiv(bool isActiv) {
        isPatrolActiv = isActiv;
    }

    public Transform getCurrentWaypoint() {
        return waypoints[m_CurrentWaypointIndex];

    }

    public void setIsWaiting(bool isWaiting)
    {
        this.isWaiting = isWaiting;
        //calculate the current waypoint index
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
    }
}
