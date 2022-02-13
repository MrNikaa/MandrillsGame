using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    float wallJumpVelocity;
    //public Animator m_Animator;
    private Vector3 direction;
    public float speed = 5f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    public bool canDoubleJump = true;


    public bool isGrounded;
    bool isWalled;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        direction.x = horizontalInput * speed;
        //m_Animator.SetFloat("run", Mathf.Abs(horizontalInput)); // Mathf.Abs i igivea rac  modulebi anu |-5| = 5 
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        isWalled = Physics.CheckSphere(wallCheck.position, 0.2f, groundLayer);
        //m_Animator.SetBool("isGrounded", isGrounded);

        if (isGrounded == false || isWalled == false)
        {
            gravity = -20;
        }

        Jump();
        if(horizontalInput != 0){ 
            Quaternion flip = Quaternion.LookRotation(new Vector3(0,0,horizontalInput));
            transform.rotation = flip;
        }
        cc.Move(direction * Time.deltaTime);
    }

    void Jump(){
        // es kodi anichebs chvens motamashes axtomis funqicas
        if (isWalled)
        {
            gravity = -1f;
            canDoubleJump = false;
            if (Input.GetButtonDown("Jump") && isWalled)
            {
                isWalled = false;
                direction.y = jumpForce;
            }
        }

        if(isGrounded)
        {
            gravity = -1f;
            canDoubleJump = true;
            if(Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            if(canDoubleJump && Input.GetButtonDown("Jump"))
            {
                //m_Animator.SetTrigger("doubleJump");
                direction.y = jumpForce;
                canDoubleJump = false;
            }
        }

        direction.y += gravity * Time.deltaTime;
    }
}
