using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void toMainMenu()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            gameManager.GetComponent<SceneVisit>().allFalse();
        }
        SceneManager.LoadScene(0);
    }
}
