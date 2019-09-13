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
    int AIState = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (AIState) {
            case -1:
                break;
            case 1:
                navMeshAgent.SetDestination(m_lastPlayerTransform);
                break;
            case 2:
                navMeshAgent.SetDestination(m_lastPlayerTransform);
                break;
            default:
                 navMeshAgent.SetDestination(m_lastPlayerTransform);
                break;
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
            AIState = 2;
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
            AIState = 2;
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
}
