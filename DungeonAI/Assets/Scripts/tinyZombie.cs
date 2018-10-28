using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinyZombie : MonoBehaviour {
    public Transform target;
    public float speed = 3f;
    public float maxDistance = 10f;
    public float minDistance = 0.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float range = Vector2.Distance(transform.position, target.position);
        float xMove = 0f;
        float yMove = 0f;

        if (minDistance < range && range < maxDistance)
        {
            Debug.Log(range);
            Vector2 updatedPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            xMove = updatedPosition.x - transform.position.x;
            yMove = updatedPosition.y - transform.position.y;

            transform.position = updatedPosition;
        }
        // Set animation
        animator.SetFloat("tinyZombieRun", Mathf.Abs(xMove) + Mathf.Abs(yMove));

        // Flip sprite if moving to left
        if (xMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (xMove > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
