using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool isWindowOpen;
    float m_time = 0;
    public Animator m_animator;


    void Awake()
    {
        StartCoroutine(setAnmiationToStartState());
    }

    public void useWindow()
    {
        if (m_time <= 0)
        {
            if (isWindowOpen)
            {
                close();
                m_time = 3.2f;
                StartCoroutine(timer());
            }
            else
            {
                open();
                m_time = 3.2f;
                StartCoroutine(timer());
            }
        }
    }


    public void script()
    {
        useWindow();
    }

    public void close()
    {
        m_animator.SetBool("isClosed", true);
        m_animator.SetBool("isOpen", false);
        isWindowOpen = false;
    }

    public void open()
    {
        m_animator.SetBool("isOpen", true);
        m_animator.SetBool("isClosed", false);
        isWindowOpen = true;
    }

    private IEnumerator timer()
    {
        while (m_time >= 0)
        {
            m_time -= 1f;
            yield return new WaitForSeconds(m_time < 1? m_time:1);
        }
    }


    private IEnumerator setAnmiationToStartState()
    {
        m_animator.speed = 1000;
        useWindow();
        yield return new WaitForSeconds(1f);
        m_animator.speed = 1;
    }
}

