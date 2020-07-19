using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    //for pawn's animator
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //for position of current checkpoint
        Vector2 pointUpdate;

        //if a player hits it
        if (other.CompareTag("Player"))
        {
            anim.Play("ActivatedAnim");
            //get checkpoint position
            pointUpdate = new Vector2(transform.position.x, transform.position.y);
            //update the respawn point
            GameManager.instance.respawnPoint = pointUpdate;

        }
    }
}
