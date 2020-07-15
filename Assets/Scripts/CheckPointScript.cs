using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //for position of current checkpoint
        Vector2 pointUpdate;

        //if a player hits it
        if (other.CompareTag("Player"))
        {
            //get checkpoint position
            pointUpdate = new Vector2(transform.position.x, transform.position.y);
            //update the respawn point
            GameManager.instance.respawnPoint = pointUpdate;

        }
    }
}
