using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    float m_xAxisClamp;
    Transform lookTaget;
    public Transform playerBody;
    public bool m_isCameraControlActiv;


    private InputMaster controls = null;

    private void Awake()
    {
        Cursor.visible = false;
       // controls = new InputMaster();
    }

    /*
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    */

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


    public void cameraRotation()
    {

        //Debug.Log("mouse");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Vector2 lookInput = controls.Player.Look.ReadValue<Vector2>();



        //float mouseX = lookInput.x;
        //float mouseY = lookInput.y;

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
