using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveScene(UserScene scene, SceneDisplay sceneDisplay)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{scene.name}.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        UserSceneData data = new UserSceneData(scene, sceneDisplay);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log($"Файл сохранен в директорию:{path}");
    }

    public static UserSceneData LoadScene(UserScene scene) // параметр для имени
    {
        string path = Application.persistentDataPath + $"/{scene.name}.bin";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserSceneData data = formatter.Deserialize(stream) as UserSceneData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Файл не найден");
            return null;
        }
    }
}
