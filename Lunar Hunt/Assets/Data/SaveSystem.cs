using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//SAVE & LOAD SYSTEM in Unity - Brackey
//https://www.youtube.com/watch?v=XOjd_qU2Ido&t=1s&ab_channel=Brackeys

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevel(LevelData level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(level);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevel()
    {
        string path = Application.persistentDataPath + "/level.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
