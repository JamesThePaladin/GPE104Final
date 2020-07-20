using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// script for main menu button behaviours
    /// </summary>
    public void PlayGame() 
    {
        //load the scene after the Main Menu, which is level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    //for quit button on start menu, closes the app
    public void QuitGame() 
    {
        Application.Quit();
    }
}
