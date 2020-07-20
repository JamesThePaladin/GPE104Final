using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    /// <summary>
    /// This script is for the game over screen Y/N button behaviours
    /// Continue is attached to the Y button and just loads the first scene
    /// NotContinue is attached to the N button and just loads the main menu.
    /// </summary>
    public void Continue() 
    {
        Destroy(GameObject.FindWithTag("HUD"));
        SceneManager.LoadScene(1);
    }

    public void NotContinue() 
    {
        Destroy(GameObject.FindWithTag("HUD"));
        SceneManager.LoadScene(0);
    }
}
