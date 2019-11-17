using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{


    //enum AIStates {};
    public int AIState = 0;
    //Ob der Bot auf einer Leiter ist
    public bool m_isOnLadder = false;
    // Die Position der Leiter auf der sich der Bot befindet
    public Transform m_ladderTransform;
    //
    public bool MoveAcrossNavMesh = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void lookToPlayer(Transform player)
    {

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
    }

    public IEnumerator MoveAcrossNavMeshLink(NavMeshAgent navMeshAgent)
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        Object owner = navMeshAgent.navMeshOwner;
        GameObject gameObject = (owner as Component).gameObject;
        //(owner as Component).gameObject.transform.Find("DoorScript");
        if (gameObject.GetComponentsInChildren<DoorScript>()[0].getBotCount() <= 1)
        {
            yield return new WaitForSeconds(2);
        }

        Vector3 endPos = data.endPos + Vector3.up * navMeshAgent.baseOffset;
        while (navMeshAgent.transform.position != endPos)
        {
            if (AIState != 2 || AIState != 3)
            {
                lookTo(endPos);
            }
            navMeshAgent.transform.position = Vector3.MoveTowards(navMeshAgent.transform.position, endPos, navMeshAgent.speed * Time.deltaTime);
            yield return null;
        }
        navMeshAgent.CompleteOffMeshLink();

        MoveAcrossNavMesh = false;
        yield return new WaitForSeconds(2);
        gameObject.BroadcastMessage("subBotCount");
        if (gameObject.GetComponentsInChildren<DoorScript>()[0].getBotCount() < 1)
        {
            gameObject.BroadcastMessage("close");
        }
    }

    public void NavMeshLinkBehaviour(NavMeshAgent navMeshAgent)
    {


        if (navMeshAgent.isOnOffMeshLink && !MoveAcrossNavMesh)
        {
            OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
            Object owner = navMeshAgent.navMeshOwner;
            GameObject gameObject = (owner as Component).gameObject;
            if (gameObject.CompareTag("door"))
            {
                gameObject.BroadcastMessage("open");
                gameObject.BroadcastMessage("addBotCount");
                navMeshAgent.autoTraverseOffMeshLink = false;
                StartCoroutine(MoveAcrossNavMeshLink(navMeshAgent));
                MoveAcrossNavMesh = true;
            }
            else
            {
                navMeshAgent.autoTraverseOffMeshLink = true;
            }
        }
    }
}
