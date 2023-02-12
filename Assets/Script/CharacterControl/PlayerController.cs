using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, ISavable
{
    public float MoveSpeed;
    private bool IsMoving;
    private Vector2 input;
    private Animator animator;

    const float offsetY = 0.3f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetPositionAndSnapToTile(transform.position);
    }

    public void SetPositionAndSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f + offsetY;

        transform.position = pos;
    }
  
    public void HandleUpdate()
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
                StartCoroutine(Move(targetPos, OnMoveOver));
            }
            
        }
        }
        animator.SetBool("IsMoving", IsMoving);
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Interact());
        }
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    IEnumerator Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("Horizontal"),animator.GetFloat("Vertical"));
        var interactPos = transform.position + facingDir;
        // Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayer.i.InteractableLayer);
        if (collider != null)
        {
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }


    IEnumerator Move(Vector3 targetPos, Action OnMoveOver=null)
    {
        IsMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        IsMoving = false;
        OnMoveOver?.Invoke();
    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0,offsetY), 0.2f, GameLayer.i.PortalLayer);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTrigger>();
            if (triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }        
        }
    }

    public object CaptureState()
    {
        float[] position = new float[] {transform.position.x, transform.position.y};
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);
    }

    private bool IsWalkable(Vector3 targetPos) 
    {
        if(Physics2D.OverlapCircle(targetPos, 0.35f, GameLayer.i.SolidLayer | GameLayer.i.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }

}