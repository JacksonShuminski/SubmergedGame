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
    public float speed;
    public Vector2 moveAmount = Vector2.zero;
    public MovementState currentMovState;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentMovState = MovementState.Idle;
        animator.SetInteger("State", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentMovState = MovementState.Left;
            moveAmount.x -= 1;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            currentMovState = MovementState.Right;
            moveAmount.x += 1;
        }
        else if (Input.GetKey(KeyCode.Space))
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
                break;

            case MovementState.Jump:
                animator.SetInteger("State", 2);
                break;
        }

    }

    private void FixedUpdate()
    {
        
    }
}
