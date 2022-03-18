using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] sourceBGs;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        sourceBGs = GameObject.FindGameObjectsWithTag("sourceBG");

        if (sourceBGs.Length > 1)
        {
            Destroy(sourceBGs[1]);
        }
    }
}
