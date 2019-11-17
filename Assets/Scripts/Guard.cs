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
    bool m_wasSeen = false;
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
    //enum AIStates {};
    // int AIState = 0;
    //Ob der Bot auf einer Leiter ist
    // bool m_isOnLadder = false;
    // Die Position der Leiter auf der sich der Bot befindet
    //Transform m_ladderTransform;
    //
    public float health = 300;
    //
    public float maxHealth = 300;
    //
    public bool isUnconscious = false;
    private bool m_wasSeenBefore;
    private float m_searchCounter = 0;
    public float m_searchMaxTime = 0;

    //7
    //private bool MoveAcrossNavMesh = false;

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
                    if (GetComponent<WaypointPatrol>() != null) {
                        GetComponent<WaypointPatrol>().setIsPatrolActiv(true);
                    }
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
                        // Debug.Log("Shot");
                    lookToPlayer(player);
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
        NavMeshLinkBehaviour(navMeshAgent);

        searchPlayerCounter();
       
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

        if (wasSeen)
        {
            navMeshAgent.speed = runSpeed;
            m_searchCounter = 0;
        }
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


    /*
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

    public void lookTo(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 12);
    }*/

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

    /*
    private IEnumerator searchInFullSpeed(float time)
    {

        navMeshAgent.speed = runSpeed;
        yield return new WaitForSeconds(time);
        if (!getWasSeen())
        {
            navMeshAgent.speed = walkSpeed;
            m_wasSeenBefore = false;
        }
     */
    }

    /*
    IEnumerator MoveAcrossNavMeshLink()
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        Object owner = navMeshAgent.navMeshOwner;
        GameObject gameObject = (owner as Component).gameObject;
        //(owner as Component).gameObject.transform.Find("DoorScript");
        if (gameObject.GetComponentsInChildren<DoorScript>()[0].getBotCount() <= 1) { 
            yield return new WaitForSeconds(2);
    }

        Vector3 endPos = data.endPos + Vector3.up * navMeshAgent.baseOffset;
        while (navMeshAgent.transform.position != endPos)
        {
            if (AIState != 2 || AIState != 3) {
                lookTo(endPos);
            }
            navMeshAgent.transform.position = Vector3.MoveTowards(navMeshAgent.transform.position, endPos, navMeshAgent.speed * Time.deltaTime);
            yield return null;
        }
        navMeshAgent.CompleteOffMeshLink();

        MoveAcrossNavMesh = false;
        yield return new WaitForSeconds(2);
        (owner as Component).gameObject.BroadcastMessage("subBotCount");
        if (gameObject.GetComponentsInChildren<DoorScript>()[0].getBotCount() < 1) {
            (owner as Component).gameObject.BroadcastMessage("close");
        }
    }*/


