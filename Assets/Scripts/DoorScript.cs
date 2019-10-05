using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isDoorOpen;
    public Animator m_animator;

    void Start()
    {
        // m_animator = GetComponentInChildren<Animator>();
        useDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void useDoor()
    {
        //Debug.Log("door");
        if (isDoorOpen)
        {
            m_animator.SetBool("isClosed", true);
            m_animator.SetBool("isOpen", false);
            isDoorOpen = false;
        }
        else
        {
            m_animator.SetBool("isOpen", true);
            m_animator.SetBool("isClosed", false);
            isDoorOpen = true;
        }
    }
}
