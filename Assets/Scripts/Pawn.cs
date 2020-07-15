using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Pawn : MonoBehaviour
{
    [Header("Components")]
    //for pawn's animator
    public Animator anim;
    //for pawn's Rigidbody2D
    public Rigidbody2D rb;
    //game object's sprite renderer
    public SpriteRenderer sr;

    [Header("Pawn Stats")]
    //for pawn speed
    public float speed;
    //for player sprint speed multiplier
    public float sprintBoost;
    //for pawn jump height
    public float jumpForce;
    //number of jumps left before touching the ground
    public int maxJumps;
    //number for current jumps
    public int currentJumps;

    [Header("Grounding")]
    //for grounding distance
    public float groundingDistance;

    void Start()
    {
        //get components of game object
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        IsGrounded();
    }

    public void Move(Vector2 direction)
    {
        //move the rigidbody by x input multiplied by speed. pass y axis 0
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void Jump() 
    {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    //bool for checking if player is grounded
    public bool IsGrounded()
    {
        //check if we are grounded
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, groundingDistance);
        if (hitInfo.collider.CompareTag("Ground"))
        {
            currentJumps = maxJumps;
            return true;
        }

        else
        {
            return false;
        }
    }
}
