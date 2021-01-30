using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    // Variables
    [SerializeField] int walkingSpeed = 3;
    [SerializeField] int runningSpeed  = 6;
    [SerializeField] int jumpSpeed = 600;

    // Bools
    private bool canJump = true;
    private bool faceRight = true;
    //private bool isGrounded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(Mathf.Sign(-10));
    }
    
    private void FixedUpdate()
    {
        Move();
        animator.SetFloat("yVelocity",(rb.velocity.y));
    }

    public void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        Walking(moveInput);
        Running(moveInput);
        Jumping();
        Flip(moveInput);



    }
    private void Walking(float moveInput)
    {
        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(moveInput * walkingSpeed, rb.velocity.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void Running(float moveInput)
    {
        if (moveInput != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveInput * runningSpeed, rb.velocity.y);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canJump)
            {
                rb.AddForce(Vector2.up * jumpSpeed);
                animator.SetBool("isJumping", true);
                canJump = false;
            }

        }
        //Debug.Log(rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground")
        {
            //Debug.Log("Temas etti.");
            animator.SetBool("isJumping", false);
            canJump = true;
        }

    }

    private void Flip(float moveInput)
    {
        if (faceRight == true && moveInput < 0)
        {
            Flipping();
        }
        else if (faceRight == false && moveInput > 0)
        {
            Flipping();
        }
        
        void Flipping()
        {
            faceRight = !faceRight;
            Vector3 scaler = transform.localScale; // localScale şu anki konumunu alır.
            scaler.x *= -1;
            transform.localScale = scaler;
        }

    }

    private void Falling()
    {
        if (rb.velocity.y < 0)
        {
            Debug.Log("düşüyor");
            animator.SetFloat("yVelocity", -1);
        }
    }
  
}
