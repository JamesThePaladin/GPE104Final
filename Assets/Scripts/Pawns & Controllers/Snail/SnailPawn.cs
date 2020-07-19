using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPawn : Pawn
{
    //to hold Walker's death sound
    public AudioSource deathSound;
    //transform for wall detection
    public Transform wallDetect;
    //for our snail's shell
    public GameObject snailShellPrefab;
    //float for how much bounce the player gets for jumping on the enemy
    public float bounce;
    //int for number of points walkers are worth
    public int points;
    //bool for moving right or left
    private bool movingRight = false;
    //distance raycast travels
    public float detectDistance;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        //get components of game object
        base.Start();
        //get sounds
        deathSound = GameObject.FindWithTag("SnailDeathSound").GetComponent<AudioSource>();
    }

    //Update is called once per frame
    protected override void Update()
    {
        GroundDetection();
    }
    public void GroundDetection()
    {
        RaycastHit2D detectInfo = Physics2D.Raycast(wallDetect.position, -Vector2.right, detectDistance, groundLayer);
        if (detectInfo.collider == true)
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
            anim.Play("SnailHit");
            //play death sound
            deathSound.Play();
            //push player up
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            //spawn shell
            GameObject snailShell = Instantiate(snailShellPrefab, transform.position, transform.rotation);
            //destroy the gamobject
            Destroy(this.gameObject, 1f);
        }
    }
}
