using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isDoorOpen;
    public Animator m_animator;
    float m_time = 0;
    public bool isPLayerUsable = true;
    private int count = 0;



    void Awake()
    {

        StartCoroutine(setAnmiationToStartState());
       
    }

    
    void Start()
    {
  
    }

 

    public void useDoor()
    {
        //Debug.Log("door");
        if (m_time <= 0) {
            if (isDoorOpen)
            {
            close();
                //m_time = m_animator.GetCurrentAnimatorStateInfo(0).length+1;
                /* m_animator.SetBool("isClosed", true);
                 m_animator.SetBool("isOpen", false);
                 isDoorOpen = false*/
                m_time = 3;
             StartCoroutine("timer");
        }
        else
            {
            open();
                //m_time = m_animator.GetCurrentAnimatorStateInfo(0).length;
                /* m_animator.SetBool("isOpen", true);
                 m_animator.SetBool("isClosed", false);
                 isDoorOpen = true;*/
                m_time = 3;
                StartCoroutine("timer");
        }
       }

    }


    public void script() {
        useDoor();
    }

    public void close()
    {
            m_animator.SetBool("isClosed", true);
            m_animator.SetBool("isOpen", false);
            isDoorOpen = false;
       // Debug.Log(count);
    }

    public void open()
    {
            m_animator.SetBool("isOpen", true);
            m_animator.SetBool("isClosed", false);
            isDoorOpen = true;
        //Debug.Log("open");
    }

   public int getBotCount() {
        return count;
    }

    public void addBotCount()
    {
        count++;
    }

    public void subBotCount()
    {
        count--;
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


    private IEnumerator setAnmiationToStartState()
    {
        m_animator.speed = 1000;
        useDoor();
        yield return new WaitForSeconds(1f);
        m_animator.speed = 1;
    }
}
