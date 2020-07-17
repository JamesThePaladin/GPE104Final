using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller
{
    //********************
    //TO DO: Finish the boss phases and test boss health
    //Also need to finish boss hitboxes
    //*******************

    protected new BossPawn pawn;
    //aggro distance for boss
    public float aggroDistance;
    //stopping distance from the players
    public float stoppingDIstance;

    //for phase 1 -> 2 health threshold
    public float phaseChange1;
    //for phase 2 -> 3 health threshold
    public float phaseChange2;
    //for phase 3 -> death health threshold
    public float phaseChange3;

    public enum BossStates 
    {
        Idle,
        Phase1,
        Phase2,
        Phase3,
        Death
    }

    public BossStates currentState;

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
                break;
            case BossStates.Phase1:
                Phase1();
                if (pawn.health < phaseChange1) 
                {
                    ChangeStates(BossStates.Phase2);
                } 
                break;
            case BossStates.Phase2:
                if (pawn.health < phaseChange2)
                {
                    ChangeStates(BossStates.Phase3);
                }
                break;
            case BossStates.Phase3:
                if (pawn.health < phaseChange3)
                {
                    ChangeStates(BossStates.Death);
                }
                break;
            case BossStates.Death:
                break;
        }
    }

    private void ChangeStates(BossStates newState) 
    {
       currentState = newState;
    }

    private void Idle() 
    {
        //do nothing
    }

    private void Phase1()
    {
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDIstance) 
        {
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            playerDirection.y = 0;
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
        if (Vector3.Distance(pawn.transform.position, GameManager.instance.player.transform.position) > stoppingDIstance)
        {
            Vector2 playerDirection = GameManager.instance.player.transform.position - pawn.transform.position;
            playerDirection.y = 0;
            pawn.Move(playerDirection);
            if (pawn.IsGrounded())
            {
                pawn.Jump();
            }
        }
    }

    private void Phase3()
    {
        //spawn 2 walkers in front of it
        //spawn one yet to-be-made flying enemy
        //revert to phase 2 behaviour until dead
    }

    private void Death()
    {
        //play death animation
        //play death sound
        //give player points
        //destroy object
    }
}
