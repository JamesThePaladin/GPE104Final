using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WalkerPawn : Pawn
{
    //to hold Walker's death sound
    public AudioSource deathSound;
    //transform for ground detection empty
    public Transform groundDetect;
    //float for the distance of the detection raycast
    public float detectDistance;
    //float for how much bounce the player gets for jumping on the enemy
    public float bounce;
    //bool for moving right
    public bool movingRight = true;
    //int for number of points walkers are worth
    public int points;

    // Start is called before the first frame update
    protected override void Start()
    {
        //get components of game object
        base.Start();
        //get sounds
        deathSound = GameObject.FindWithTag("WalkerDeathSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GroundDetection();
    }

    /// <summary>
    /// Walker Ground Detection is courtesy of Blackthornprod
    /// I have adapted it to work with my controller
    /// </summary>
    public void GroundDetection()
    {
        //send a raycast down by a set amount starting at the ground detect empty on the ground layer
        RaycastHit2D detectInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, detectDistance, groundLayer);
        //if you dont detect anything
        if (detectInfo.collider == false)
        {
            //and if moving right is true
            if (movingRight == true)
            {
                //flip -180 degrees
                transform.eulerAngles = new Vector3(0, -180, 0);
                //set moving right to false
                movingRight = false;
            }
            else
            {
                //flip 180 degrees
                transform.eulerAngles = new Vector3(0, 0, 0);
                //and set moving right to true
                movingRight = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if game object is not an asteroid
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SendMessage("ScorePoints", points);
            //make an explosion
            anim.Play("DeathAnim");
            //play death sound
            deathSound.Play();
            //push player up
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            //destroy the gamobject
            Destroy(this.gameObject, 1f);
        }
    }
}
