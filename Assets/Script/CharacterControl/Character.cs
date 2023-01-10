using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    CharacterAnimator animator;
    public float MoveSpeed;
    public bool IsMoving {get; private set;}

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    public IEnumerator Move(Vector2 moveVec)
    {
        animator.Horizontal = Mathf.Clamp(moveVec.x,-1f,1f);
        animator.Vertical = Mathf.Clamp(moveVec.y,-1f,1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!IsPathClear(targetPos))
        {
            yield break;
        }
        
        IsMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        IsMoving = false;
        animator.IsMoving = false;
    }

    private bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;

        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude -1, GameLayer.i.SolidLayer | GameLayer.i.InteractableLayer | GameLayer.i.PlayerLayer) == true)
            return false;
        return true;
    }
    private bool IsWalkable(Vector3 targetPos) 
    {
        if(Physics2D.OverlapCircle(targetPos, 0.35f, GameLayer.i.SolidLayer | GameLayer.i.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    

    public void LookTowards(Vector3 targetPos)
    {
        var xdiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y); 

        if (xdiff == 0 || ydiff == 0)
        {
            animator.Horizontal = Mathf.Clamp(xdiff,-1f,1f);
            animator.Vertical = Mathf.Clamp(ydiff,-1f,1f);
        }
    } 

    public CharacterAnimator Animator
    {
        get => animator;
    }
}
