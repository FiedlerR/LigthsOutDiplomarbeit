﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_CharacterController;
    public float slopeForce;
    public float slopeForceRayLength;

    public float jumpMultiplier;
    bool m_IsJumping;
    public AnimationCurve jumpFallOff;

    float m_MovementSpeed;
    Animator m_Animator;
    bool m_isOnLadder;
    public float walkSpeed;
    public float runSpeed;
    public float sneakSpeed;
    public float runBuildUpSpeed;
    public float runSlowDownSpeed;

    bool isSneaking;
    GameObject m_selectedGameObject;
    Transform m_transform;

    public PlayerLook cameraScript;
    InputManager inputManager;
    GameObject mainCamera;


    private void Awake()
    {

        inputManager = GameObject.FindObjectOfType<InputManager>();
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = false;
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponentInChildren<Animator>();
        m_transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();

        jump();
        setMovementSpeed();

        useSelectedObject();
        KOMove();


        if (inputManager.GetKeyDown("Shoot"))
        {
         //   Debug.Log("Ein Schuss!");
            GetComponentsInChildren<shootRaycastTriggerable>()[0].Shoot();
        }

        // playerMoveHandler();

        // Debug.Log(m_selectedGameObject);
    }

    private void useSelectedObject()
    {
        if (m_selectedGameObject != null && inputManager.GetKey("Use"))
        {
            if (m_selectedGameObject.CompareTag("ladder")) {
                transform.transform.position = new Vector3( m_selectedGameObject.transform.position.x, transform.transform.position.y, m_selectedGameObject.transform.position.z) + m_selectedGameObject.GetComponent<LadderScript>().climpOffset;
                setIsOnLadder(true);
            }
            else if (m_selectedGameObject.CompareTag("door"))
            {
                // m_selectedGameObject.GetComponent<DoorScript>().useDoor();
                if (m_selectedGameObject.GetComponent<DoorScript>() != null && m_selectedGameObject.GetComponent<DoorScript>().isPLayerUsable) {
                    m_selectedGameObject.GetComponent<DoorScript>().useDoor();
                }

                m_selectedGameObject = null;
            }
            else if (m_selectedGameObject.CompareTag("switch"))
            {
                m_selectedGameObject.GetComponent<SwitchScript>().useSwitch();
                m_selectedGameObject = null;
            }
            else if (m_selectedGameObject.CompareTag("window"))
            {
                m_selectedGameObject.GetComponentInParent<WindowScript>().useWindow();
                m_selectedGameObject = null;
            }
        }
    }

    void playerMoveHandler() {
        m_CharacterController.SimpleMove(new Vector3(0, 0, 0));


        if (isOnRamp())
        {
            m_CharacterController.Move(Vector3.down * m_CharacterController.height / 2 * slopeForce * Time.deltaTime);
        }
    }

    public void playerMovement() {
        float verticalInput = 0;
        float horizontalInput = 0;
        if (inputManager.GetKey("Forward"))
        {
            verticalInput = 1;

        }else
        if (inputManager.GetKey("Back"))
        {
            verticalInput = -1;

        }
        if (inputManager.GetKey("Right"))
        {
            horizontalInput = 1;

        }else
        if (inputManager.GetKey("Left"))
        {
            horizontalInput = -1;

        }


        // Debug.Log("move");
        //m_CharacterController.SimpleMove(Vector3.ClampMagnitude(transform.up*5, 1f) * m_MovementSpeed);
        if (m_isOnLadder)
        {

            if (verticalInput > 0)
            {
                Vector3 moveDirection = Vector3.up * walkSpeed * verticalInput * Time.deltaTime; // + transform.forward;
                m_CharacterController.Move(moveDirection);
            }
            else {
                Vector3 moveDirection = Vector3.up * walkSpeed * verticalInput * Time.deltaTime;
                m_CharacterController.Move(moveDirection);
            }


            if (verticalInput <= 0 && m_CharacterController.collisionFlags == CollisionFlags.Below) {
                Vector3 forwardMovement = transform.forward * verticalInput;
                Vector3 rightMovement = transform.right * horizontalInput;
                setIsOnLadder(false);
                m_CharacterController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f) * m_MovementSpeed);
            }
        }
        else {
            //float verticalInput = Input.GetAxis("Vertical");
            //float horizontalInput = Input.GetAxis("Horizontal");

            // Debug.Log(verticalInput);
           /* if (verticalInput != 0 || horizontalInput != 0)
            {*/

                // if (verticalInput != 0 || horizontalInput != 0) {

                Vector3 forwardMovement = transform.forward * verticalInput;
                Vector3 rightMovement = transform.right * horizontalInput;
                // m_CharacterController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f) * m_MovementSpeed);


                // }

                Vector3 moveDirection = Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f);
                moveDirection *= m_MovementSpeed;
                m_CharacterController.SimpleMove(moveDirection);
           // }
           
            if ((verticalInput != 0 || horizontalInput != 0) && isOnRamp() )
            {
                m_CharacterController.Move(Vector3.down * m_CharacterController.height / 2 * slopeForce * Time.deltaTime);
            }
        }


        animationControl(verticalInput != 0, horizontalInput != 0);
    }


    public void jump() {
        if (inputManager.GetKeyDown("Jump") && !m_IsJumping)
        {
           
            m_IsJumping = true;
            StartCoroutine(JumpEvent());
        }
    }


    void KOMove()
    {
        if (Input.GetButtonDown("KOMove") && isSneaking)
        {
            if (m_selectedGameObject != null && m_selectedGameObject.CompareTag("EnemyFootCollider") && !m_selectedGameObject.GetComponentInParent<Guard>().getWasSeen())
            {
                Debug.Log("KOMove");
                m_selectedGameObject.GetComponentInParent<Guard>().setIsUnconscious(true);
            }
        }
    }

    bool isOnRamp() {
        if (m_IsJumping) {
            return false;
         }

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position,Vector3.down,out raycastHit, m_CharacterController.height / 2 * slopeForceRayLength)) {
            if (raycastHit.normal != Vector3.up) {
                return true;
            }
        }

        return false;
     }

    void setMovementSpeed()
    {

        if (inputManager.GetKey("Sprint") && !m_isOnLadder)
        {
            m_MovementSpeed = Mathf.Lerp(m_MovementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
            isSneaking = false;
        }
        else if (inputManager.GetKey("Sneak") && !m_isOnLadder)
        {
            isSneaking = true;
            m_MovementSpeed = Mathf.Lerp(m_MovementSpeed, sneakSpeed, Time.deltaTime * runSlowDownSpeed);
        }
        else {
            if (!isSneakingAnimation() && (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("walk") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))) {
                
                mainCamera.transform.localPosition = new Vector3(0.0008975412f, 0.2021998f, 0.050f);
            }
            isSneaking = false;
            m_MovementSpeed = Mathf.Lerp(m_MovementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
        }
    }


    IEnumerator JumpEvent() {

        m_CharacterController.slopeLimit = 90f;
        float jumpTime = 0f;

        do {
            float jumpForce = jumpFallOff.Evaluate(jumpTime);
            m_CharacterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            jumpTime += Time.deltaTime;
            yield return null;
        } while (!m_CharacterController.isGrounded && m_CharacterController.collisionFlags != CollisionFlags.Above);


        m_CharacterController.slopeLimit = 45f;
        m_IsJumping = false;
    }

    public void setIsOnLadder(bool isOnLadder) {
        //Debug.Log(isOnLadder);
        if (isOnLadder)
        {
           cameraScript.setIsCameraControlActiv(!isOnLadder, m_selectedGameObject.transform);
        }
        else {
            cameraScript.setIsCameraControlActiv(!isOnLadder);
        }
        m_isOnLadder = isOnLadder;
    }

    public bool getIsOnLadder()
    {
        //Debug.Log(isOnLadder);
        return m_isOnLadder;
    }

    void animationControl(bool vertical, bool horizontal) {
        if (vertical || horizontal)
        {
            m_Animator.SetBool("isStanding", false);
            if (inputManager.GetKey("Sneak") && !inputManager.GetKey("Sprint") && !m_isOnLadder)
            {

                GameObject mainCamera = GameObject.FindWithTag("MainCamera");
                mainCamera.transform.localPosition = new Vector3(0.0008975412f, 0.2021998f, 0.091f);
                m_Animator.SetBool("isSneaking", true);
                m_Animator.SetBool("isWalking", false);

            }
            else {
                m_Animator.SetBool("isWalking", true);
                m_Animator.SetBool("isSneaking", false);
            }
        }
        else {
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isStanding", true);
            m_Animator.SetBool("isSneaking", false);
        }
    }


    public bool isSneakingAnimation() {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("sneak");
    }

    public bool getIsSneaking()
    {
        return isSneaking;
    }

    public void setUseAbleObject(GameObject selectedGameObject) {
        m_selectedGameObject = selectedGameObject;
    }
}
