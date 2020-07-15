using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WalkerPawn : Pawn
{
    /// <summary>
    /// Walker Ground Detection is courtesy of Blackthornprod
    /// I have adapted it to work with my controller
    /// </summary>
    ///
    
    //to hold Walker's death sound
    public AudioSource deathSound;
    //to hold the walker's death explosion
    public GameObject explosion;
    //transform for ground detection
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
    void Start()
    {
        deathSound = GameObject.FindWithTag("WalkerDeathSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundDetection();
    }
    public void GroundDetection()
    {
        RaycastHit2D detectInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, detectDistance);
        if (detectInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
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
