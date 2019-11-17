using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectUseableObjectScript : MonoBehaviour
{
    //Referenz zum Spielerobjekt
    public GameObject player;
    public Transform cameraTransform;
    public Material highlightMaterial;
    GameObject selectedGameObject;
    public float selectDistance;


    // Update is called once per frame
    void Update()
    {
        if (selectedGameObject != null) {
            selectedGameObject = null;
            player.GetComponent<PlayerMovement>().setUseAbleObject(null);
        }


        RaycastHit hit;
        //Debug.DrawRay(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward)*2, Color.green,0, false);
     
        if (Physics.Raycast(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
            // Debug.Log(hit.distance + "++"+ hit.transform.tag);

             Debug.DrawRay(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward), Color.red, 0, false);
            var selectedObject = hit.transform;
            float distance = Vector3.Distance(player.transform.position, hit.point);
            if (distance < selectDistance)
            {

                //Debug.Log(selectedObject.tag);
                // Debug.Log("use hit");

                ///    Debug.Log(selectedObject.gameObject.tag);
                if (selectedObject.CompareTag("ladder") || selectedObject.CompareTag("door") || selectedObject.CompareTag("switch") || selectedObject.CompareTag("window"))
                {
                    /*var selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
                    if (selectedObjectRenderer != null) {
                       // selectedObjectRenderer.material = highlightMaterial;
                    }*/
                    player.GetComponent<PlayerMovement>().setUseAbleObject(hit.transform.gameObject);
                    selectedGameObject = hit.transform.gameObject;
                }
            }

            if (distance < 1.3) {
                if (selectedObject.CompareTag("EnemyFootCollider"))
                {
                    //Debug.Log("use hit");
                    player.GetComponent<PlayerMovement>().setUseAbleObject(hit.transform.gameObject);
                    selectedGameObject = hit.transform.gameObject;
                }
            }
           
           
        }
        /*
        //  if (Physics.Raycast(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, 10, QueryTriggerInteraction.Ignore))
        if (Physics.Raycast(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, QueryTriggerInteraction.Ignore))
        {
            var selectedObject = hit.transform;
            float distance = Vector3.Distance(player.transform.position, hit.point);
                Debug.Log(selectedObject.tag);

                if (selectedObject.CompareTag("EnemyFootCollider"))
                {
                player.GetComponent<PlayerMovement>().setUseAbleObject(hit.transform.gameObject);
                selectedGameObject = hit.transform.gameObject;
            }    
        }*/
    }
}
