using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //to hold our player's jump sound
    private AudioSource jumpSound;

    //to hold the moveInput
    private float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        if (pawn == null)
        {
            pawn = GameManager.instance.player.GetComponent<Pawn>();
        }

        jumpSound = GameObject.FindWithTag("JumpSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement inputs
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveInput = Input.GetAxis("Horizontal") * pawn.sprintBoost;
            pawn.Move(new Vector2(moveInput, 0));
        }

        else
        {
            moveInput = Input.GetAxis("Horizontal");
            pawn.Move(new Vector2(moveInput, 0));
        }

        //jump
        if (Input.GetButtonDown("Jump") && pawn.currentJumps > 0)
        {
            jumpSound.Play();
            pawn.Jump();
            pawn.currentJumps--;
        }
        else if (Input.GetButtonDown("Jump") && pawn.currentJumps == 0 && pawn.IsGrounded()) 
        {
            jumpSound.Play();
            pawn.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }
}

