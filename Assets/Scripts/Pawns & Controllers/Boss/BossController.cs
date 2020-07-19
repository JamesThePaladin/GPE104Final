using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller
{
    //********************
    //TO DO: Finish the boss phases and test boss health
    //Also need to finish boss hitboxes
    //*******************

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
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) < aggroDistance)
                {
                    ChangeStates(BossStates.Phase1);  
                }
                if (pawn.health < phaseChange2)
                {
                    ChangeStates(BossStates.Phase3);
                }
                if (pawn.health < phaseChange3)
                {
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Phase1:
                Phase1();
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) > aggroDistance)
                {
                    ChangeStates(BossStates.Idle);
                }
                if (pawn.health == phaseChange1) 
                {
                    ChangeStates(BossStates.Phase2);
                }
                if (pawn.health == phaseChange2)
                {
                    ChangeStates(BossStates.Phase3);
                }
                break;
            case BossStates.Phase2:
                if (Vector3.Distance(GameManager.instance.player.transform.position, pawn.transform.position) > aggroDistance)
                {
                    ChangeStates(BossStates.Idle);
                }
                if (pawn.health == phaseChange2)
                {
                    ChangeStates(BossStates.Phase3);
                }
                break;
            case BossStates.Phase3:
                Phase3();
                if (pawn.health == phaseChange3)
                {
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Death:
                Death();
                break;
        }
    }

    private void ChangeStates(BossStates newState) 
    {
       currentState = newState;
    }

    private void Idle() 
    {
        pawn.anim.Play("BossIdle");
        //do nothing
    }

    private void Phase1()
    {
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance) 
        {
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            playerDirection.y = 0;
            pawn.anim.Play("BossWalk");
            pawn.Move(playerDirection);
            if (pawn.IsGrounded())
            {
                StartCoroutine(WaitToJump());
            } 
        }

        IEnumerator WaitToJump() 
        {
            yield return new WaitForSeconds(2f);
            pawn.Jump();
        }
    }

    private void Phase2()
    {
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance)
        {
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            playerDirection.y = 0;
            pawn.anim.Play("BossWalk");
            pawn.Move(playerDirection);
            if (pawn.IsGrounded())
            {
                pawn.Jump();
            }
        }
    }

    private void Phase3()
    {
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDistance)
        {
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            playerDirection.y = 0;
            pawn.anim.Play("BossWalk");
            pawn.Move(playerDirection);
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
        //play death sound
        //destroy boss
        Destroy(GameObject.FindWithTag("Boss"));
    }

    
}
