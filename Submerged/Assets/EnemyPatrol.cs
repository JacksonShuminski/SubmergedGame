using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Variables
    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft = 0.0f;       // Define wallLeft
    public float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    private SpriteRenderer spriteRenderer; //Used to flip the direction the sprite is rendered
    float originalX; // Original float value
    private bool inverted; //Checks if the sprite starts inverted
    public bool patrol = true; //Should the enemy patrol



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        this.originalX = this.transform.position.x;

        //Is the sprite inveretd?
        inverted = false;

        if (spriteRenderer.flipX) //Checks if true
        {
            inverted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol)
        {
            walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;

            //Move left
            if (walkingDirection > 0.0f && transform.position.x >= originalX + wallRight)
            {
                walkingDirection = -1.0f;

                if (inverted)
                    spriteRenderer.flipX = false;

                else
                    spriteRenderer.flipX = true;
            }

            //Move right
            else if (walkingDirection < 0.0f && transform.position.x <= originalX - wallLeft)
            {
                walkingDirection = 1.0f;

                if (inverted)
                    spriteRenderer.flipX = true;

                else
                    spriteRenderer.flipX = false;
            }

            transform.Translate(walkAmount);
        }
    }
}
