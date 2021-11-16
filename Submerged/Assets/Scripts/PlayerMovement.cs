using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MovementState
{
    Idle,
    Left,
    Right,
    Jump
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Vector2 playerPos;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float moveInput;
    
    public float speed;
    public float jumpForce;
    public Vector2 moveAmount = Vector2.zero;
    public MovementState currentMovState;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentMovState = MovementState.Idle;
        animator.SetInteger("State", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentMovState = MovementState.Left;
            moveAmount.x -= 1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            currentMovState = MovementState.Right;
            moveAmount.x += 1;
        }
        else if (Input.GetKey(KeyCode.Space)) //Should not need to be held down in the future
        {
            currentMovState = MovementState.Jump;
            moveAmount.y += 1;
        }
        else
        {
            currentMovState = MovementState.Idle;
        }

        moveAmount = moveAmount.normalized * speed;

        //Animation state machine
        switch (currentMovState)
        {
            case MovementState.Idle:
                animator.SetInteger("State", 0);
                break;

            case MovementState.Right: //Will add left later
                animator.SetInteger("State", 1);
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                break;

            case MovementState.Left: //Will add left later
                animator.SetInteger("State", 1);
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
                break;

            case MovementState.Jump:
                animator.SetInteger("State", 2);
                break;
        }

    }

    private void FixedUpdate()
    {
        //Handles movement
        moveInput = Input.GetAxisRaw("Horizontal");
        playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);

        if (Mathf.Approximately(playerBody.velocity.x, 0.0f))
            animator.SetFloat("Speed", 0.0f);

        else
            animator.SetFloat("Speed", playerBody.velocity.x);
    }
}
