using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public LayerMask solidObjectLayer;
    private bool IsMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
  
    private void Update()
    {
        if (!IsMoving) 
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            if (input.x != 0) input.y = 0; 

            if(input != Vector2.zero) {
            
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
            var targetPos = transform.position;
            targetPos.x += input.x;
            targetPos.y += input.y;
            if (IsWalkable(targetPos))
            {
                StartCoroutine(Move(targetPos));
            }
            
        }
        }
        animator.SetBool("IsMoving", IsMoving);
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("Horizontal"),animator.GetFloat("Vertical"));
        var interactPos = transform.position + facingDir;
        // Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        IsMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        IsMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos) 
    {
        if(Physics2D.OverlapCircle(targetPos, 0.35f, solidObjectLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
}
