﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //variable that holds this instance of the GameManager
    public static GameManager instance;
    //variable for player
    public GameObject player;
    //variable for checkpoint respawns
    public Vector2 respawnPoint;
    //public player score for testing
    public int score;
    //lives for player
    public int lives;
    //amount of coins player has
    public int coinAmount;
    //for time on current level
    private float sceneTime;
    //reference to score text
    public Text scoreText;
    //reference to lives text
    public Text livesText;
    //reference to coins amount text
    public Text coinText;
    //to hold the time in current scene
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else
        {
            Destroy(this.gameObject); // delete the new game manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }

        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
        }

        //initialize UI text displays and game stats
        score = 0;
        scoreText.text = " x" + score;
        lives = 3;
        livesText.text = " x" + lives;
        sceneTime = 0;
        timeText.text = " " + 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
        }
    }

    void FixedUpdate() 
    {
        //to find the text fields when the UI is destroyed on a continue.
        if (scoreText == null) 
        {
            scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        }
        if (livesText == null)
        {
            livesText = GameObject.FindWithTag("LivesText").GetComponent<Text>();
        }
        if (timeText == null)
        {
            timeText = GameObject.FindWithTag("TimeText").GetComponent<Text>();
        }
        if (coinText == null) 
        {
            coinText = GameObject.FindWithTag("CoinAmount").GetComponent<Text>();
        }

        //update time display
        LevelTime();
    }

    //takes in points from other objects and adds it to the player's score
    public void ScorePoints(int addPoints)
    {
        //add points to player score
        score += addPoints;
        //update score text in UI
        scoreText.text = " x" + score;
    }

    //displays current level time
    public void LevelTime() 
    {
        Convert.ToDouble(sceneTime);
        //update time
        sceneTime += Time.deltaTime;
        //update display
        timeText.text = " " + sceneTime;
    }

    public void LoseLife()
    {
        //minus a life
        lives--;
        //update lives in UI
        livesText.text = " x" + lives;

        //if lives are less than or equal to 0 game over
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void CollectCoins(int addCoins)
    {
        //add points to player score
        coinAmount += addCoins;
        //update score text in UI
        coinText.text = "" + coinAmount;
    }

    public void OnPlayerDeath() 
    {
        LoseLife();
        //send the player to their last checkpoint
        player.transform.position = respawnPoint;
    }
}
