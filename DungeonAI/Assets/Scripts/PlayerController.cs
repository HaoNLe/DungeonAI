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

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Move the character
        Vector3 moveDirection = new Vector3(moveHorizontal, moveVertical, 0.0f);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        characterController.Move(moveDirection * Time.deltaTime);

        // Play run animation
        animator.SetFloat("playerRun", Mathf.Abs(moveVertical) + Mathf.Abs(moveHorizontal));

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
