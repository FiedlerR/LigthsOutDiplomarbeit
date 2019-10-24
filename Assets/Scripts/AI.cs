using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI : MonoBehaviour
{

    bool m_wasHeard;
    bool m_wasSeen;
    Vector3 m_lastPlayerTransform;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    //enum AIStates {};
    int AIState = 0;

    bool m_isOnLadder = false;
    Transform m_ladderTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("AIState: "+ AIState);
         switch (AIState) {
            case 0: //waypatrol
                navMeshAgent.speed = 1.5f;
                GetComponent<WaypointPatrol>().setIsPatrolActiv(true);
                break;
            case 1:// search
                navMeshAgent.SetDestination(m_lastPlayerTransform);
                if (m_wasSeen && Vector3.Distance(transform.position, player.position) < 10)
                {
                    AIState = 3;
                    navMeshAgent.isStopped = true;
                }
                break;
            case 2: // follow
                lookTo();
                navMeshAgent.SetDestination(m_lastPlayerTransform);
                if (Vector3.Distance(transform.position, player.position) < 10)
                {
                    AIState = 3;
                    navMeshAgent.isStopped = true;
                }
                    break;
            case 3: // chase Mode
               // Debug.Log("Shot");
                lookTo();
                if (Vector3.Distance(transform.position, player.position) >= 10)
                {
                    m_lastPlayerTransform = player.position;
                    AIState = 2;

                }
                break;
            default:
                 //navMeshAgent.SetDestination(m_lastPlayerTransform);
                break;
        }
        if (m_isOnLadder)
        {
            lookTo(m_ladderTransform);
        }
    }

    public void setHeard(bool wasHeard,Transform lastPlayerTransform) {
        m_wasHeard = wasHeard;
        if (m_wasHeard && !m_wasSeen)
        {
            m_lastPlayerTransform = new Vector3 (lastPlayerTransform.position.x, lastPlayerTransform.position.y, lastPlayerTransform.position.z);
            GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            navMeshAgent.speed = 1.5f;
            AIState = 1;
            navMeshAgent.isStopped = false;
        }
        else if(!m_wasSeen) {
            AIState = 0;
        }
    }

    public void setSeen(bool wasSeen, Transform lastPlayerTransform)
    {
        m_wasSeen = wasSeen;
        if (m_wasSeen)
        {
            m_lastPlayerTransform = new Vector3(lastPlayerTransform.position.x, lastPlayerTransform.position.y, lastPlayerTransform.position.z);
            navMeshAgent.speed = 5.5f;
            GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            if (AIState != 3) {
                AIState = 2;
                navMeshAgent.isStopped = false;
            }
        }
        else if (m_wasHeard)
        {
            setHeard(true,lastPlayerTransform);
        }
        else {
            AIState = 0;
        }
    }



    public void setHeard(bool wasHeard)
    {
        m_wasHeard = wasHeard;
        if (m_wasHeard && !m_wasSeen)
        {
            GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            navMeshAgent.speed = 1.5f;
            AIState = 1;
        }
        else if (!m_wasSeen)
        {
            AIState = 0;
        }
    }

    public void setSeen(bool wasSeen)
    {
        m_wasSeen = wasSeen;
        if (m_wasSeen)
        {
            navMeshAgent.speed = 5.5f;
            GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            if (AIState != 3)
            {
                AIState = 2;
            }
        }
        else if (m_wasHeard)
        {
            setHeard(true);
        }
        else
        {
            AIState = 0;
        }
    }

    public void rotateToLadder(Transform ladderTransform) {
        // transform.Rotate(Vector3.left);
        transform.eulerAngles = new Vector3(0, ladderTransform.eulerAngles.y - 90, 0);
        //Debug.Log("rotate");
    }

    public void setIsOnLadder(bool isOnLadder, Transform ladderTransform)
    {
        m_isOnLadder = isOnLadder;
        m_ladderTransform = ladderTransform;
    }

    public void setIsOnLadder(bool isOnLadder)
    {
        m_isOnLadder = isOnLadder;
    }
    /*
    public void lookTo(Transform targetTransform)
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);
    }*/

    public void lookTo() {

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);
    }

    public void lookTo(Transform targetTransform)
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 12);
    }
}
