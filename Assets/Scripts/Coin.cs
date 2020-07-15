using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //audio source
    private AudioSource CoinCollect;

    //to hold the coin's point value
    public int points;
    //to hold the coin's monetary value
    public int coinValue;

    // Start is called before the first frame update
    void Start()
    {
        CoinCollect = GameObject.FindWithTag("CoinCollect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            CoinCollect.Play();
            //tell the GameManager to score points
            GameManager.instance.SendMessage("ScorePoints", points);
            //tell the GameManager to collect some coins
            GameManager.instance.SendMessage("CollectCoins", coinValue);
            //destroy this game object
            Destroy(this.gameObject);
        }
    }
}
