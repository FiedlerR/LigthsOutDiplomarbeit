using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    float m_xAxisClamp;
    Transform lookTaget;
    public Transform playerBody;
    public bool m_isCameraControlActiv;


    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        m_xAxisClamp = 0f;
        m_isCameraControlActiv = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(m_isCameraControlActiv);
      
            cameraRotation();

    }


    void cameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        m_xAxisClamp += mouseY;


        if (m_xAxisClamp > 90)
        {
            m_xAxisClamp = 90;
            mouseY = 0;
            correctEulerAngles(270);
        }
        else if (m_xAxisClamp < -53)
        {
            m_xAxisClamp = -53;
            mouseY = 0;
            correctEulerAngles(53);
        }

        transform.Rotate(Vector3.left * mouseY);
       if (m_isCameraControlActiv)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void correctEulerAngles(float angle) {
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = angle;
        transform.eulerAngles = eulerAngles;
    }


    public void setIsCameraControlActiv(bool isCameraControlActiv,Transform target) {
        m_isCameraControlActiv = isCameraControlActiv;

        lookTaget = target;
        playerBody.eulerAngles = new Vector3(0, lookTaget.eulerAngles.y - 90, 0);
    }

    public void setIsCameraControlActiv(bool isCameraControlActiv)
    {
        m_isCameraControlActiv = isCameraControlActiv;
    }

}
