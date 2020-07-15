using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
    [Header("Animation Thresholds")]
    //thresholds for playing animations
    public float animXDeadZone;
    public float animYDeadZone;

    void FixedUpdate()
    {
        UpdateAnimations();
    }

    //method for animations
    public void UpdateAnimations()
    {
        if (rb.velocity.y > animYDeadZone && rb.velocity.x <= animXDeadZone)
        {
            sr.flipX = true;
            anim.Play("Player1Jump");
        }
        else if (rb.velocity.y > animYDeadZone && rb.velocity.x >= animXDeadZone)
        {
            sr.flipX = false;
            anim.Play("Player1Jump");
        }
        else if (rb.velocity.y < animYDeadZone && rb.velocity.x <= animXDeadZone)
        {
            sr.flipX = true;
            anim.Play("Player1Jump");
        }
        else if (rb.velocity.y < animYDeadZone && rb.velocity.x >= animXDeadZone)
        {
            sr.flipX = false;
            anim.Play("Player1Jump");
        }
        else if (rb.velocity.x < animXDeadZone && rb.velocity.y == animYDeadZone)
        {
            sr.flipX = true;
            anim.Play("Player1Walk2");
        }
        else if (rb.velocity.x > animXDeadZone && rb.velocity.y == animYDeadZone)
        {
            sr.flipX = false;
            anim.Play("Player1Walk2");
        }
        else
        {
            anim.Play("Player1Idle");
        }
    }
}
