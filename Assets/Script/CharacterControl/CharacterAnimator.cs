using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    //Parameter
    public float Horizontal {get; set;}
    public float Vertical {get; set;}
    public bool IsMoving {get; set;}

    //State
    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkLeftAnim;
    SpriteAnimator walkRightAnim;

    SpriteAnimator currentAnim;
    bool wasPrevMoving;

    //Reference
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(walkDownSprites, spriteRenderer);
        walkUpAnim = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkLeftAnim = new SpriteAnimator(walkLeftSprites, spriteRenderer);
        walkRightAnim = new SpriteAnimator(walkRightSprites, spriteRenderer);
        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;
        if (Horizontal == 1)
        {
            currentAnim = walkRightAnim;
        }
        else if (Horizontal == -1)
        {
            currentAnim = walkLeftAnim;
        }
        else if (Vertical == 1)
        {
            currentAnim = walkUpAnim;
        }
        else if (Vertical == -1)
        {
            currentAnim = walkDownAnim;
        }

        if (currentAnim != prevAnim || IsMoving != wasPrevMoving)
        {
            currentAnim.Start();
        }

        if (IsMoving)
        {
            currentAnim.HandleUpdate();
        }
        else{
            spriteRenderer.sprite = currentAnim.Frames[0];
        }
        wasPrevMoving = IsMoving;
    }
}