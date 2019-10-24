using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoorTrigger : MonoBehaviour
{

    public bool isActiv;
    public GameObject scriptObject;



    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("EnemyFootCollider"))
        {
            /*Debug.Log("collider test");
            Debug.Log(scriptObject);*/
            scriptObject.SendMessage("script");
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyFootCollider"))
        {
            scriptObject.SendMessage("script");
        }
    }

}
