using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public static spawnPoint ins;
    private GameObject[] spawnPoints;

    public GameObject[] spawnPrefabs;
    [SerializeField] public int spawnNumber;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        ins = this;

    }

    private void OnLevelWasLoaded(int level)
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnManager");

        if (spawnPoints.Length > 1)
        {
            Destroy(spawnPoints[1]);
        }
    }

    public void setSpawnPoint(GameObject player)
    {
        player.transform.position = spawnPrefabs[spawnNumber].transform.position;
    }
}
