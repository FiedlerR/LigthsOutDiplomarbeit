using UnityEngine;
using UnityEngine.AI;

public class LadderScript : MonoBehaviour
{


    public Vector3 climpOffset;
    public Vector3 topOffset;
    public Vector3 endDir;
    public bool endBack = false;
    // Start is called before the first frame update
   /* void Start()
    {

    }
    */
    // Update is called once per frame
  /*  void Update()
    {

    }*/

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("LadderTrigger");
        if (other.gameObject.CompareTag("EnemyFootCollider"))
            {
           
                other.GetComponentInParent<Guard>().setIsOnLadder(true, transform);
            }else if (other.gameObject.CompareTag("NPCFootCollider"))
        {

            other.GetComponentInParent<NPC>().setIsOnLadder(true, transform);
        }
    }
    /*
    void OnTriggerStay(Collider other)
    {

        //Debug.Log("LadderTrigger");
        if (other.gameObject.CompareTag("Player"))
        {
            //if (Input.GetKey("f")) {
            //  other.GetComponent<PlayerMovement>().setIsOnLadder(true);
            //other.GetComponent<Transform>().position = new Vector3(other.GetComponent<Transform>().position.x, other.GetComponent<Transform>().position.y, transform.position.z);
            //}
           /*if(Input.GetButtonDown("Action"))
            {
                other.GetComponent<Transform>().position = new Vector3(transform.position.x, other.GetComponent<Transform>().position.y, transform.position.z) + climpOffset;
                other.GetComponent<PlayerMovement>().setIsOnLadder(true);
            }
        }
    }*/

    
        void OnTriggerExit(Collider other)
        {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PlayerMovement>().getIsOnLadder())
        {
            if (endBack)
            {
                other.GetComponent<Transform>().position += endDir;
            }
            else {
                other.GetComponent<PlayerMovement>().m_CharacterController.Move(other.GetComponent<PlayerMovement>().transform.forward);
            }
            other.GetComponent<PlayerMovement>().setIsOnLadder(false);
        } else if (other.gameObject.CompareTag("EnemyFootCollider") || other.gameObject.CompareTag("NPCFootCollider")) {
            other.GetComponentInParent<AI>().setIsOnLadder(false);
            other.GetComponentInParent<NavMeshAgent>().autoTraverseOffMeshLink = false;
        }
            /*
            else if (other.gameObject.CompareTag("EnemyFootCollider"))
            {
                other.GetComponentInParent<Guard>().setIsOnLadder(false);
            }
            else if (other.gameObject.CompareTag("NPCFootCollider"))
            {

                other.GetComponentInParent<NPC>().setIsOnLadder(false);
           }*/
        }
}
