using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Folow always the path. Also if the Player was seen by this unit.
public class WaypointPatrol : MonoBehaviour
{
    //binding navMeshAgent from AI Unit
    public NavMeshAgent navMeshAgent;
    //array of waypoints GameObject
    public Transform[] waypoints;
    //
    public float timeToWait;
    //
    public bool isPatrolActiv;
    //
    public Transform[] lookToWhileWaiting;

    // index for the current waypoint
    int m_CurrentWaypointIndex;
    //
    float m_WaitingTimer;

    //
    Animator m_Animator;


    void Start()
    {


        //set first destination waypoint to 0
        if (waypoints.Length != 0)
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }
        ///
        m_Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        if (isPatrolActiv) {
            //   
            if (lookToWhileWaiting.Length != 0 && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + navMeshAgent.speed)
            {
                Vector3 direction = (lookToWhileWaiting[m_CurrentWaypointIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
            }


            //check if the unit arrived the waypoint
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                //

                m_Animator.SetBool("isWalking", false);
                m_Animator.SetBool("isStanding", true);

                m_WaitingTimer += Time.deltaTime;
                if (m_WaitingTimer > timeToWait)
                {
                    //
                        m_Animator.SetBool("isWalking", true);
                        m_Animator.SetBool("isStanding", false);
                   

                    //
                    m_WaitingTimer = 0;
                    //calculate the current waypoint index
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                    //use the calculated waypoint to set a new destination
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
             
            }
        }
    }


    public void setIsPatrolActiv(bool isActiv) {
        isPatrolActiv = isActiv;
    }
}
