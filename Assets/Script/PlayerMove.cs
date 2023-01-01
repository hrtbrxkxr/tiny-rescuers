using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   public float MoveSpeed = 5f;
   public Rigidbody2D rb;
   public Animator animator;

   Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void UpdateAnimationAndMove()
    {
        if(movement != Vector2.zero) {
            
            FixedUpdate();
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * MoveSpeed*Time.fixedDeltaTime);
    }
}
