using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    //to hold player's HUD
    private GameObject playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        if (playerHUD == null)
        {
            playerHUD = this.gameObject;
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else
        {
            Destroy(this.gameObject); // delete the new UI attempting to store itself, there can only be one.
            Debug.Log("Warning: A second player HUD was detected and destrtoyed"); // display message in the console to inform of its demise
        }
    }
}
