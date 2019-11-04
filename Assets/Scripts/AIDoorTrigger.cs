using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoorTrigger : MonoBehaviour
{

    public bool isActiv;
    public GameObject scriptObject;



    private void OnTriggerEnter(Collider other)
    {
      
        if ((other.CompareTag("EnemyFootCollider") || other.CompareTag("NPCFootCollider")) &&  isActiv)
        {
            scriptObject.SendMessage("open");
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.CompareTag("EnemyFootCollider") || other.CompareTag("NPCFootCollider")) && isActiv)
        {
            scriptObject.SendMessage("open");
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("EnemyFootCollider") || other.CompareTag("NPCFootCollider")) && isActiv)
        {
            scriptObject.SendMessage("close");
        }
    }

}
