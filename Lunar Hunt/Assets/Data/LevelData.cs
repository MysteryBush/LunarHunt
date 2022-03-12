using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    //public static SceneData ins;
    public string knotLocationName;
    public string initialKnot;

    public LevelData(LevelData levelData)
    {
        knotLocationName = levelData.knotLocationName;
        initialKnot = levelData.initialKnot;


    }

    //public PlayerData (PlayerCombat player)
    //{
        
    //}
}
