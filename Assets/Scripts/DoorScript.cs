using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isDoorOpen;
    public Animator m_animator;
    float m_time = 0;

    void Start()
    {
        // m_animator = GetComponentInChildren<Animator>();
        isDoorOpen = !isDoorOpen;
        useDoor();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
 

    public void useDoor()
    {
        //Debug.Log("door");
        //if (m_time <= 0) {
            if (isDoorOpen)
            {
                //m_time = m_animator.GetCurrentAnimatorStateInfo(0).length+1;
                m_animator.SetBool("isClosed", true);
                m_animator.SetBool("isOpen", false);
                isDoorOpen = false;
               // StartCoroutine("timer");
            }
            else
            {
                //m_time = m_animator.GetCurrentAnimatorStateInfo(0).length;
                m_animator.SetBool("isOpen", true);
                m_animator.SetBool("isClosed", false);
                isDoorOpen = true;
                //StartCoroutine("timer");
            }
      //  }
    }


    public void script() {
        useDoor();
    }

    private IEnumerator timer()
    {
        while (m_time >= 0)
        {
            m_time -= 1f;
           // Debug.Log(m_animator.GetCurrentAnimatorStateInfo(0));
            yield return new WaitForSeconds(1f);
        }
    }
}
