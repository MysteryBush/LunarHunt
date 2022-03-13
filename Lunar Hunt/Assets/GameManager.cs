using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    private GameObject[] gameManagers;
    //Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ins = this;
        SceneVisit.ins.runVisit();
        SceneState.ins.runSceneState();
    }
    private void OnLevelWasLoaded(int level)
    {
        gameManagers = GameObject.FindGameObjectsWithTag("GameManager");

        if (gameManagers.Length > 1)
        {
            Destroy(gameManagers[1]);
        }

        SceneVisit.ins.runVisit();
        SceneState.ins.runSceneState();
    }
    void Awake()
    {
        //getRoot();
    }

    //void getRoot()
    //{
    //    // get root objects in scene
    //    List<GameObject> rootObjects = new List<GameObject>();
    //    Scene scene = SceneManager.GetActiveScene();
    //    scene.GetRootGameObjects(rootObjects);

    //    // iterate root objects and do something
    //    for (int i = 0; i < rootObjects.Count; ++i)
    //    {
    //        GameObject gameObject = rootObjects[i];
    //        ins.transformZ(gameObject);
    //    }
    //}

    //void transformZ(GameObject gameObject)
    //{
    //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    //}
}
