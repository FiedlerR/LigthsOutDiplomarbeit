using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform doorRight;
    public Transform doorLeft;
    public float openingSpeed;
    public float maxOpeningWidth;

    public bool invert;
    public bool isOpen = false;

    bool m_isDoorOpenening;
    Vector3 m_StartPositonRight;
    Vector3 m_StartPositonLeft;
    Vector3 m_EndPositonRight;
    Vector3 m_EndPositonLeft;
    int m_ObjectsInTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_EndPositonRight = doorRight.localPosition + Vector3.right*maxOpeningWidth;
        m_EndPositonLeft = doorLeft.localPosition - Vector3.right * maxOpeningWidth;
        m_StartPositonRight = doorRight.localPosition;
        m_StartPositonLeft = doorLeft.localPosition;
        m_isDoorOpenening = isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDoorOpenening) {  
            doorRight.localPosition = Vector3.Lerp(doorRight.localPosition, m_EndPositonRight, Time.deltaTime*openingSpeed);
            doorLeft.localPosition = Vector3.Lerp(doorLeft.localPosition, m_EndPositonLeft, Time.deltaTime * openingSpeed);

        } else {
            doorRight.localPosition = Vector3.Lerp(doorRight.localPosition, m_StartPositonRight, Time.deltaTime * openingSpeed);
            doorLeft.localPosition = Vector3.Lerp(doorLeft.localPosition, m_StartPositonLeft, Time.deltaTime * openingSpeed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_ObjectsInTrigger--;
        }
        else if (other.gameObject.CompareTag("EnemyFootCollider"))
        {
            m_ObjectsInTrigger--;
        }

        if (m_ObjectsInTrigger == 0) {
            m_isDoorOpenening = invert ? true : false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
          {
              m_isDoorOpenening = invert ? false : true;
              m_ObjectsInTrigger++;
          }
         else if (other.gameObject.CompareTag("EnemyFootCollider"))
        {
            m_isDoorOpenening = invert ? false : true;
            m_ObjectsInTrigger++;
        }
      }
}
