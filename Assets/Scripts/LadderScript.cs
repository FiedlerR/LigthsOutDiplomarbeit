using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{


    public Vector3 climpOffset;
    public Vector3 topOffset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("LadderTrigger");
        if (other.gameObject.CompareTag("EnemyFootCollider"))
            {
           
                other.GetComponent<AI>().setIsOnLadder(true);
            }
    }

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
            }*/
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().getIsOnLadder())
            {
                /// other.GetComponent<Transform>().position += topOffset;      
               other.GetComponent<Transform>().position += transform.forward*0.3f;
                /*
                float verticalInput = Input.GetAxis("Vertical");
                float horizontalInput = Input.GetAxis("Horizontal");
                Vector3 forwardMovement = transform.forward * verticalInput;
                Vector3 rightMovement = transform.right * horizontalInput;
                other.GetComponent<CharacterController>().simpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f));*/
            }
            other.GetComponent<PlayerMovement>().setIsOnLadder(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<AI>().setIsOnLadder(false);
            Debug.Log("NPC on Ladder");
            other.GetComponent<AI>().rotateToLadder(transform);
        }
    }
}
