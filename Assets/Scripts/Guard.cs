using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Guard: AI
{

    //Spieler wurde vom Bot gehört
    bool m_wasHeard = false;
    //Spieler wurde vom Bot gesehen
    [HideInInspector]
    public bool m_wasSeen = false;
    // letzte bekannte Spielerposition
    Vector3 m_lastPlayerTransform;
    //NavmeshAgent Referenz
    public NavMeshAgent navMeshAgent;
    //Spielerposition
    public Transform player;

    //
    public float runSpeed = 6;
    //
    public float walkSpeed = 2;
    //
    public float health = 300;
    //
    public float maxHealth = 300;
    //
    public bool isUnconscious = false;
    private bool m_wasSeenBefore;
    public float m_searchCounter = 0;
    public float m_searchMaxTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnconscious)
        {
            //Debug.Log("AIState: "+ AIState);
            switch (AIState)
            {
                case 0: //waypatrol
                    navMeshAgent.speed = walkSpeed;
                 //   if (GetComponent<WaypointPatrol>() != null) {
                        GetComponent<WaypointPatrol>().setIsPatrolActiv(true);
                  //  }
                    break;
                case 1:// search
                    navMeshAgent.SetDestination(m_lastPlayerTransform);
                    if (m_wasSeen && Vector3.Distance(transform.position, player.position) < 10 && !m_isOnLadder)
                    {
                        AIState = 3;
                        navMeshAgent.isStopped = true;
                    }
                    break;
                case 2: // follow
                    lookToPlayer(player);
                    navMeshAgent.SetDestination(m_lastPlayerTransform);
                    //Debug.Log(Vector3.Distance(transform.position, player.position));
                    if (Vector3.Distance(transform.position, player.position) < 10 && !m_isOnLadder)
                    {
                        AIState = 3;
                        navMeshAgent.isStopped = true;
                    }
                    break;
                case 3: // chase Mode
                    lookToPlayer(player);
                    navMeshAgent.isStopped = true;
                    if (Vector3.Distance(transform.position, player.position) >= 10)
                    {
                        m_lastPlayerTransform = player.position;
                        AIState = 2;

                    }
                    GetComponent<shootRaycastTriggerable>().AIShoot(transform.Find("ShotOrigin").transform.position,(player.position - transform.position).normalized);
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
        NavMeshLinkBehaviour(navMeshAgent);

        searchPlayerCounter();
       // Debug.Log(AIState);
        //Debug.Log("searchCounter"+m_searchCounter);
    }

    private void searchPlayerCounter()
    {

        if (m_searchCounter < m_searchMaxTime)
        {
            m_searchCounter += Time.deltaTime;
        }
        else {
            navMeshAgent.speed = walkSpeed;
            if (!m_wasSeen && ! m_wasHeard)
            {
                AIState = 0;
            }
        }
    }

    public void setHeard(bool wasHeard,Transform lastPlayerTransform) {
        m_wasHeard = wasHeard;
        if (m_wasHeard && !m_wasSeen)
        {
            m_lastPlayerTransform = new Vector3 (lastPlayerTransform.position.x, lastPlayerTransform.position.y, lastPlayerTransform.position.z);
            if (GetComponent<WaypointPatrol>() != null)
            {
                GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            }
            if (!m_wasSeenBefore) {
                //navMeshAgent.speed = walkSpeed;
            }
            AIState = 1;
            navMeshAgent.isStopped = false;
        }
        else if(!m_wasSeen) {
            //AIState = 0;
        }
    }

    public void setSeen(bool wasSeen, Transform lastPlayerTransform)
    {

        if (wasSeen) {
            navMeshAgent.speed = runSpeed;
            m_searchCounter = 0;
        }
        m_wasSeen = wasSeen;
        if (m_wasSeen)
        {
            m_lastPlayerTransform = new Vector3(lastPlayerTransform.position.x, lastPlayerTransform.position.y, lastPlayerTransform.position.z);
            navMeshAgent.speed = runSpeed;
            if (GetComponent<WaypointPatrol>() != null)
            {
                GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            }
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
            //AIState = 0;
        }
    }



    public void setHeard(bool wasHeard)
    {
        m_wasHeard = wasHeard;
        if (m_wasHeard && !m_wasSeen)
        {
            if (GetComponent<WaypointPatrol>() != null)
            {
                GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            }
            if (!m_wasSeenBefore)
            {
               // navMeshAgent.speed = walkSpeed;
            }
            AIState = 1;
        }
        else if (!m_wasSeen)
        {
           // AIState = 0;
        }
    }

    public void setSeen(bool wasSeen)
    {

      /*  if (wasSeen)
        {*/
            navMeshAgent.speed = runSpeed;
            m_searchCounter = 0;
       // }
        m_wasSeen = wasSeen;
        if (m_wasSeen)
        {
            navMeshAgent.speed = runSpeed;
            if (GetComponent<WaypointPatrol>() != null)
            {
                GetComponent<WaypointPatrol>().setIsPatrolActiv(false);
            }
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
           // AIState = 0;
        }
    }

    public void setIsUnconscious(bool isUnconscious) {
        Debug.Log("setIsUnconscious: " + isUnconscious);
        this.isUnconscious = isUnconscious;
        navMeshAgent.isStopped = false;
    }

    public bool getWasSeen() {
        return m_wasSeen;
    }


    private IEnumerator wait(float time)
    {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(time);
        navMeshAgent.isStopped = false;

    }

    public Vector3 getLastPlayerV3() { return m_lastPlayerTransform; } // Um in AIShoot auf das Sehen zu reagieren

    public float getSearchCounter() { return m_searchCounter; } // Um in AIShoot auf das Sehen zu reagieren und damit der Gegner auch wieder aufhört zu schießen
    }


