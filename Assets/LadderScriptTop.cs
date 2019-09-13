using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScriptTop : MonoBehaviour
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
           //other.GetComponent<Transform>().position = new Vector3(other.GetComponent<Transform>().position.x, other.GetComponent<Transform>().position.y, transform.position.z);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().setIsOnLadder(false);
        }
    }
}
