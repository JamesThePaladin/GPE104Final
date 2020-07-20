using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller
{
    //for boss pawn
    protected new BossPawn pawn;
    //for walker instantiation
    public GameObject walkerPrefab;
    //for walker controller
    public GameObject walkerControllerPrefab;
    //for walker spawner transform
    public Transform walkerSpawner;
    //aggro distance for boss
    public float aggroDistance;
    //stopping distance from the players
    public float stoppingDistance;

    //for phase 1 -> 2 health threshold
    public float phaseChange1;
    //for phase 2 -> 3 health threshold
    public float phaseChange2;
    //for phase 3 -> death health threshold
    public float phaseChange3;
    //int for point value
    public int points;

    public enum BossStates 
    {
        Idle,
        Phase1,
        Phase2,
        Phase3,
        Death
    }

    public BossStates currentState;

    private void Start()
    {
        pawn = GameObject.FindWithTag("Boss").GetComponent<BossPawn>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState) 
        {
            case BossStates.Idle:
                Idle();
                //if the distance between the boss and the player is less than aggro distance
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) < aggroDistance)
                {
                    //change to Phase 1
                    ChangeStates(BossStates.Phase1);  
                }
                //if the bosse' health is low enough for Phase 2
                if (pawn.health == phaseChange1)
                {
                    //change to Phase 2
                    ChangeStates(BossStates.Phase2);
                }
                //if the boss's health is low enough for Phase 3
                if (pawn.health == phaseChange2)
                {
                    //change to Phase 3
                    ChangeStates(BossStates.Phase3);
                }
                //if the boss's health is low enough to die
                if (pawn.health == phaseChange3)
                {
                    //die
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Phase1:
                Phase1();
                //if the distance between the boss and the player is greater than aggro distance
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) > aggroDistance)
                {
                    //change back to idle
                    ChangeStates(BossStates.Idle);
                }
                //if the boss's health is low enough for Phase 2
                if (pawn.health == phaseChange1) 
                {
                    //change to Phase 2
                    ChangeStates(BossStates.Phase2);
                }
                //if the boss's health is low enough for Phase 3
                if (pawn.health == phaseChange2)
                {
                    //change to Phase 3   
                    ChangeStates(BossStates.Phase3);
                }
                //if the boss's health is low enough to die
                if (pawn.health == phaseChange3)
                {
                    //die
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Phase2:
                Phase2();
                //if the distance between the boss and the player is greater than aggro distance
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) > aggroDistance)
                {
                    //change back to idle
                    ChangeStates(BossStates.Idle);
                }
                //if the boss's health is low enough for Phase 3
                if (pawn.health == phaseChange2)
                {
                    //change to Phase 3
                    ChangeStates(BossStates.Phase3);
                }
                //if the boss's health is low enough to die
                if (pawn.health == phaseChange3)
                {
                    //die
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Phase3:
                Phase3();
                //if the boss's health is low enough to die
                if (pawn.health == phaseChange3)
                {
                    //die
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Death:
                Death();
                break;
        }
    }

    //helper function for changing boss states
    private void ChangeStates(BossStates newState) 
    {
        currentState = newState;
    }

    private void Idle() 
    {
        pawn.anim.Play("BossIdle");
        //do nothing
    }

    /// <summary>
    /// Phase 1, boss moves towards the player until a certain point while jumping periodically
    /// </summary>
    private void Phase1()
    {
        //if the distance between you and the player is greater than your stopping distance
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance) 
        {
            //get the direction the player is in
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            //set y to zero to prevent movement on that axis
            playerDirection.y = 0;
            //play your walk animation
            pawn.anim.Play("BossWalk");
            //tell pawn to move in that direction
            pawn.Move(playerDirection);
            //jump periodically only if grounded
            if (pawn.IsGrounded())
            {
                StartCoroutine(WaitToJump());
            } 
        }
        //jump function inside an IEnumerator to reduce jump frequency
        IEnumerator WaitToJump() 
        {
            yield return new WaitForSeconds(2f);
            pawn.Jump();
        }
    }

    /// <summary>
    /// Phase 2 is the same as Phase 1 except the boss jumps more frequently
    /// </summary>
    private void Phase2()
    {
        //if the distance between you and the player is greater than your stopping distance
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance)
        {
            //get the direction the player is in
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            //set y to zero to prevent movement on that axis
            playerDirection.y = 0;
            //play walk aniamtion
            pawn.anim.Play("BossWalk");
            //tell pawn to move in that direction
            pawn.Move(playerDirection);
            //jump only if grounded
            if (pawn.IsGrounded())
            {
                pawn.Jump();
            }
        }
    }

    private void Phase3()
    {
        //if the distance between you and the player is greater than your stopping distance
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance)
        {
            //get the direction the player is in
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            //set y to zero to prevent movement on that axis
            playerDirection.y = 0;
            //play walk aniamtion
            pawn.anim.Play("BossWalk");
            //tell pawn to move in that direction
            pawn.Move(playerDirection);
            //jump only if grounded
            if (pawn.IsGrounded())
            {
                pawn.Jump();
            }
        }
    }

    private void Death()
    {
        //give player points
        GameManager.instance.SendMessage("ScorePoints", points);
        //play death animation
        pawn.anim.Play("BossDie");
        //destroy boss
        Destroy(GameObject.FindWithTag("Boss"));
    }

    
}
