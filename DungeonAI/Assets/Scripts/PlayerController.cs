using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Animator animator;
    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // Play run animation and move the character
        animator.SetFloat("playerRun", Mathf.Abs(moveVertical) + Mathf.Abs(moveHorizontal));
        float dy = moveVertical * speed * Time.deltaTime;
        float dx = moveHorizontal * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + dx, transform.position.y + dy);

        // Flip sprite if moving to left
        if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

    }
}
