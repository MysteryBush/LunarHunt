using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int iLevelToLoad;

    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    //to carry the number to next scene
    public int spawnNumber;

    //ref
    [SerializeField] private SpawnPoint spawnPoint;


    void Start()
    {
        spawnPoint = GameObject.Find("SpawnManager").GetComponent<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "Player")
        {
            //GameManager.ins.GetComponent<setSpawn>().spawnNumber = spawnNumber;
            spawnPoint.spawnNumber = spawnNumber;
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
