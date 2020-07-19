using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPawn : Pawn
{
    //for boss health
    public float health;
    //amount player bounces off of boss hitbox
    public float bounce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("BossHit");
            //push player up
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            //if health is above 0 decrement it
            if (health > 0)
            {
                health--;
            }
        }
    }
}
