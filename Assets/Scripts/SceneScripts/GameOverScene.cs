using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Continue() 
    {
        SceneManager.LoadScene(1);
    }

    public void NotContinue() 
    {
        SceneManager.LoadScene(0);
    }
}
