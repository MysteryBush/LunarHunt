using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level = 0;

    public void SaveLevel()
    {
        //SaveSystem.SaveLevel(this);
    }

    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadLevel();

        //level = data.level;


    }
}
