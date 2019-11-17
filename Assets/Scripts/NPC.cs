using UnityEngine;
using UnityEngine.AI;

public class NPC : AI
{
    //binding navMeshAgent from AI Unit
    public NavMeshAgent navMeshAgent;
    //array of waypoints GameObject
    public GameObject[] waypoints;
    //0 wait; 1 Talk to the destination
    // public int[] waypointType;

    public enum WaypointType
    {
        waitForCertainTime,
        talkToOtherNPC
    }

    public WaypointType[] waypointType;
    //
    public float[] waypointTime;
    //
    public GameObject[] lookToWhileWaitingOnWaypoint;
    // index for the current waypoint
    int m_CurrentWaypointIndex;
    //
    float m_WaitingTimer;
    //
    int ísOtherNpcTalking = -1;
    //
   // bool m_isOnLadder = false;
    //
   // Transform m_ladderTransform;





    //
    Animator m_Animator;

  


    void Start()
    {


        //set first destination waypoint to 0
        if (waypoints.Length != 0)
        {
            navMeshAgent.SetDestination(waypoints[0].transform.position);
        }
        //
        m_Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!m_isOnLadder)
        {
            walkingAround();
        }
        else {
            lookTo(m_ladderTransform);
        }
        NavMeshLinkBehaviour(navMeshAgent);

    }

    void walkingAround() {
        //   
        if (lookToWhileWaitingOnWaypoint.Length != 0 && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + navMeshAgent.speed)
        {
            Vector3 direction = (lookToWhileWaitingOnWaypoint[m_CurrentWaypointIndex].transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
        }


        //check if the unit arrived the waypoint
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + (waypointType[m_CurrentWaypointIndex] == WaypointType.talkToOtherNPC ? 1.3 : 0))
        {
            //

            if (waypointType[m_CurrentWaypointIndex] == WaypointType.talkToOtherNPC)
            {
                navMeshAgent.isStopped = true;
                waypoints[m_CurrentWaypointIndex].GetComponent<NPC>().lookTo(transform);
                lookTo(lookToWhileWaitingOnWaypoint[m_CurrentWaypointIndex].transform);
            }

            m_WaitingTimer += Time.deltaTime;
            if (m_WaitingTimer > waypointTime[m_CurrentWaypointIndex])
            {

                navMeshAgent.isStopped = false;

                if (ísOtherNpcTalking != -1) {
                    waypoints[ísOtherNpcTalking].GetComponent<NPC>().disableTalkMode();
                    ísOtherNpcTalking = -1;
                }
                m_WaitingTimer = 0;
                //calculate the current waypoint index
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                //use the calculated waypoint to set a new destination
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].transform.position);
                if (waypointType[m_CurrentWaypointIndex] == WaypointType.talkToOtherNPC) {
                    waypoints[m_CurrentWaypointIndex].GetComponent<NPC>().enableTalkMode();
                    ísOtherNpcTalking = m_CurrentWaypointIndex;
                }

            }
        }



    }
    void enableTalkMode()
    {
        navMeshAgent.isStopped = true;
    }

    /*
    void rotateTo(Transform transfrom) {
        Vector3 direction = (transfrom.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    /*
    public void setIsOnLadder(bool isOnLadder,Transform ladderTransform)
    {
        m_isOnLadder = isOnLadder;
        m_ladderTransform = ladderTransform;
        //Debug.Log("ladder: "+ isOnLadder);
    }

    public void setIsOnLadder(bool isOnLadder)
    {
        m_isOnLadder = isOnLadder;
       // Debug.Log("ladder: " + isOnLadder);
    }

    public void lookTo(Transform targetTransform)
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 12);
    }*/

    void disableTalkMode()
    {
        navMeshAgent.isStopped = false;
    }
}
