using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MainMenu : MonoBehaviour
{
    //ref gameManager
    private GameObject gameManager;
    private SceneState sceneState;
    private SceneVisit sceneVisit;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        sceneState = gameManager.GetComponent<SceneState>();
        sceneVisit = gameManager.GetComponent<SceneVisit>();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //reset GameState
        sceneState.resetSceneState();
        sceneVisit.allFalse();
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
