using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevelScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameLevelScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameLevelScene");
    }
}
