using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPawn : Pawn
{
    //to hold Walker's death sound
    public AudioSource deathSound;
    //float for how much bounce the player gets for jumping on the enemy
    public float bounce;
    //int for number of points walkers are worth
    public int points;
    // Start is called before the first frame update
    protected override void Start()
    {
        //get components of game object
        base.Start();
        //get sounds
        deathSound = GameObject.FindWithTag("SnailDeathSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //protected override void Update()
    //{
    //    GroundDetection();
    //}
    //public void GroundDetection()
    //{
    //    RaycastHit2D detectInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, detectDistance, groundLayer);
    //    if (detectInfo.collider == false)
    //    {
    //        if (movingRight == true)
    //        {
    //            transform.eulerAngles = new Vector3(0, -180, 0);
    //            movingRight = false;
    //        }
    //        else
    //        {
    //            transform.eulerAngles = new Vector3(0, 0, 0);
    //            movingRight = true;
    //        }
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if game object is not an asteroid
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SendMessage("ScorePoints", points);
            //make an explosion
            anim.Play("SnailHit");
            //play death sound
            deathSound.Play();
            //push player up
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            //destroy the gamobject
            Destroy(this.gameObject, 1f);
        }
    }
}
