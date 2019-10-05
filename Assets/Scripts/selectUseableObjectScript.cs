using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectUseableObjectScript : MonoBehaviour
{
    public GameObject player;
    public Transform cameraTransform;
    public Material highlightMaterial;
    GameObject selectedGameObject;
    public float selectDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selectedGameObject != null) {
            selectedGameObject = null;
            player.GetComponent<PlayerMovement>().setUseAbleObject(null);
        }


        RaycastHit hit;
        Debug.DrawRay(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward)*2, Color.green,0, false);
     
        if (Physics.Raycast(cameraTransform.transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
           // Debug.Log(hit.distance + "++"+ hit.transform.tag);

           // Debug.DrawRay(player.transform.position, (hit.point - player.transform.position).normalized* hit.distance, Color.red, 0, false);
            if (Vector3.Distance(player.transform.position, hit.point) < selectDistance) {
               // Debug.Log("use hit");
                var selectedObject = hit.transform;
           ///    Debug.Log(selectedObject.gameObject.tag);
                if (selectedObject.CompareTag("ladder") || selectedObject.CompareTag("door")) {
                    var selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
                    if (selectedObjectRenderer != null) {
                       // selectedObjectRenderer.material = highlightMaterial;
                    }
                    player.GetComponent<PlayerMovement>().setUseAbleObject(hit.transform.gameObject);
                    selectedGameObject = hit.transform.gameObject;
                }
            }

            }
    }
}
