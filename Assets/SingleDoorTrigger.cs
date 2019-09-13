using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorTrigger : MonoBehaviour
{
    public Transform door;
    public float openingSpeed;
    public float maxOpeningWidth;
    public bool invert;
    public bool isOpen = false;

    bool m_isDoorOpenening;
    Vector3 m_StartPositon;
    Vector3 m_EndPositon;
    int m_ObjectsInTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_EndPositon = door.localPosition + Vector3.right * maxOpeningWidth;
        m_StartPositon = door.localPosition;
        m_isDoorOpenening = isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDoorOpenening)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, m_EndPositon, Time.deltaTime * openingSpeed);

        }
        else
        {
            door.localPosition = Vector3.Lerp(door.localPosition, m_StartPositon, Time.deltaTime * openingSpeed);
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

        if (m_ObjectsInTrigger == 0)
        {
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
