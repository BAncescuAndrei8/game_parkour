using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float wallJumpCooldown = 1.2f;
    public float nextWalljumpA = 0f;
    public float nextWalljumpB = 0f;
    public float defaultSpeed = 12f;
    public float walkingSpeed = 6f;
    public float sprintSpeed = 22f;
    float speed = 12f;
    public float gravity = -18.87f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public Transform Camera;
    public Transform wallCheckA;
    public Transform wallCheckB;
    public float groundDistance = 0.4f;
    public float wallDistance = 0.8f;
    public LayerMask groundMask;
    public LayerMask wallMask;

    Vector3 velocity;
    bool isGrounded;
    bool isWalledB;
    bool isWalledA;

    // Update is called once per frame
    void Update()
    {
        isWalledB = Physics.CheckSphere(wallCheckB.position, wallDistance, wallMask);
        isWalledA = Physics.CheckSphere(wallCheckA.position, wallDistance, wallMask);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

       

            if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = defaultSpeed;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = walkingSpeed;
            }
            else
            {
                speed = defaultSpeed;
            }
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Time.time > nextWalljumpA)
        {
            if (Input.GetButtonDown("Jump") && isWalledA)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                nextWalljumpA = Time.time + wallJumpCooldown;
                
            }
        }
        if (Time.time > nextWalljumpB)
            if (Input.GetButtonDown("Jump") && isWalledB)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                nextWalljumpB = Time.time + wallJumpCooldown;
                
            }
        
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);


    
    }
}
