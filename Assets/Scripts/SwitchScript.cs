using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{

    public bool isActiv;
    Animator m_animator;
    public GameObject scriptObject;
    public float scriptDelay;
    float m_time = 0;
    bool firstStart = true;



    void Awake()
    {
        m_animator = GetComponent<Animator>();
        StartCoroutine(setAnmiationToStartState());
    }



    public void useSwitch()
    {
   
        if (isActiv && m_time <= 0)
        {
            m_time = scriptDelay;
            StartCoroutine("timer");
            m_animator.SetBool("isActivated", false);
            isActiv = false;
            //Debug.Log("activ");
            // scriptObject.SendMessage("script");
        }
        else if (m_time <= 0)
        {
            m_time = scriptDelay;
            StartCoroutine("timer");
            m_animator.SetBool("isActivated", true);
            isActiv = true;
            //Debug.Log("!activ");
            //scriptObject.gameObject.SendMessage("script");
            //scriptObject.SendMessage("script");
        }
    }

    private IEnumerator timer()
    {
        while (m_time > 0)
        {
            m_time -= 1f;
           // Debug.Log(m_time);
            yield return new WaitForSeconds(1f);
        }
        if (!firstStart)
        {
            scriptObject.SendMessage("script");
        }
        else
        {
            firstStart = false;
        }
       
    }

    private IEnumerator setAnmiationToStartState()
    {
        m_animator.speed = 1000;
        useSwitch();
        yield return new WaitForSeconds(1f);
        m_animator.speed = 1;
    }


}
