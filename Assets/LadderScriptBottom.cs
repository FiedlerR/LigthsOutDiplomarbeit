using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScriptBottom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().setIsOnLadder(true);
            // other.GetComponent<Transform>().position = new Vector3(transform.localPosition.x, other.GetComponent<Transform>().position.y, other.GetComponent<Transform>().position.z);
        }
        else if(other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<AI>().setIsOnLadder(true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().setIsOnLadder(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<AI>().setIsOnLadder(false);
        }
    }
}
