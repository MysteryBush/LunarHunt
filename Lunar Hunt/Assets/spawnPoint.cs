using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public GameObject[] spawnPoints;
    private int spawnNumber;
    private PlayerMovement player;

    private void Start()
    {
        spawnNumber = GameManager.ins.GetComponent<setSpawn>().spawnNumber;
        player = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
    }
    public void setSpawnPoint()
    {
        player.transform.position = spawnPoints[spawnNumber].transform.position;
    }
}
