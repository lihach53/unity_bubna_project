/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Android.Types;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public GameObject objectPrefab;
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class SceneData
{
    public GameObject scenePrefab;
    public Vector3 platformPositions;
    public Vector3 platformScale;
    public List<ObjectData> objects = new List<ObjectData>();
}


public class UserDataSaver : MonoBehaviour
{
    public GameObject scene;
    public List<GameObject> sceneObjects;

    private string filePath;

    public SceneDisplay sceneDisplay;
    void Start()
    {
        sceneDisplay = GetComponent<SceneDisplay>();
        filePath = Application.persistentDataPath + "/sceneData.json";
    }
    [System.Serializable]
    public class SceneData
    {
        public string scenePrefabName;  // Хранит имя префаба сцены
        public Vector3 platformPositions;
        public Vector3 platformScale;
        public List<ObjectData> objects = new List<ObjectData>();
    }

    public void SaveScene()
    {
        SceneData sceneData = new SceneData();
        scene = sceneDisplay.scene;

        sceneData.platformPositions = scene.transform.position;
        sceneData.platformScale = scene.transform.localScale;

        // Сохраняем имя префаба сцены
        if (sceneDisplay.model != null)
        {
            sceneData.scenePrefabName = sceneDisplay.model.name;
        }
        else
        {
            Debug.LogWarning("Префаб сцены не назначен.");
        }

        foreach (var obj in sceneObjects)
        {
            ObjectData sceneObjectData = new ObjectData();
            sceneObjectData.position = obj.transform.position;
            sceneObjectData.rotation = obj.transform.rotation;

            // Здесь можно также сохранить имя префаба объекта
            if (PrefabUtility.IsPartOfPrefabInstance(obj))
            {
                sceneObjectData.objectPrefab = PrefabUtility.GetCorrespondingObjectFromSource(obj);
            }
            else
            {
                sceneObjectData.objectPrefab = obj;
            }

            sceneData.objects.Add(sceneObjectData);
        }

        string jsonData = JsonUtility.ToJson(sceneData, true);
        File.WriteAllText(filePath, jsonData);

        Debug.Log($"Данные сохранены ({filePath})");
    }
    public void LoadScene()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(jsonData);

            // Загружаем префаб сцены из Resources
            if (!string.IsNullOrEmpty(sceneData.scenePrefabName))
            {
                scene = Resources.Load<GameObject>(sceneData.scenePrefabName);
                if (scene != null)
                {
                    scene = Instantiate(scene);
                    scene.transform.position = sceneData.platformPositions;
                    scene.transform.localScale = sceneData.platformScale;
                }
                else
                {
                    Debug.LogError("Не удалось загрузить префаб сцены.");
                    return;
                }
            }
            else
            {
                Debug.LogError("Префаб сцены отсутствует в данных.");
                return;
            }

            // Удаление старых объектов
            foreach (var obj in sceneObjects)
            {
                Destroy(obj);
            }
            sceneObjects.Clear();

            // Воссоздаем объекты на сцене
            foreach (var objData in sceneData.objects)
            {
                GameObject newObject = Instantiate(objData.objectPrefab);
                newObject.transform.position = objData.position;
                newObject.transform.rotation = objData.rotation;
                sceneObjects.Add(newObject);
            }

            Debug.Log("Данные успешно загружены.");
        }
        else
        {
            Debug.LogWarning("Файл данных не найден!");
        }
    }


    */
/*    public void SaveScene()
        {
            SceneData sceneData = new SceneData();
            scene = sceneDisplay.scene;
            sceneData.platformPositions = scene.transform.position;
            sceneData.platformScale = scene.transform.localScale;
            sceneData.scenePrefab = scene;

            foreach (var obj in sceneObjects)
            {
                ObjectData sceneObjectData = new ObjectData();
                sceneObjectData.position = obj.transform.position;
                sceneObjectData.rotation = obj.transform.rotation;
                sceneObjectData.objectPrefab = obj;
                sceneData.objects.Add(sceneObjectData);
            }

            string jsonData = JsonUtility.ToJson(sceneData, true);
            File.WriteAllText(filePath, jsonData);

            Debug.Log($"Данные сохранены ({filePath})");
        }

        public void LoadScene()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                SceneData sceneData = JsonUtility.FromJson<SceneData>(jsonData);
                scene = sceneData.scenePrefab;
                scene.transform.position = sceneData.platformPositions;
                scene.transform.localScale = sceneData.platformScale;
                Instantiate(scene);
                for (int i = 0; i < sceneData.objects.Count; i++)
                {
                    sceneObjects[i].transform.position = sceneData.objects[i].position;
                    sceneObjects[i].transform.rotation = sceneData.objects[i].rotation;
                    sceneObjects[i] = sceneData.objects[i].objectPrefab;
                    Instantiate(sceneObjects[i]);
                }


                Debug.Log("Данные успешно загружены.");
            }
            else
            {
                Debug.LogWarning("Файл данных не найден!");
            }
        }*/
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public SerializableVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public struct SerializableQuaternion
{
    public float x, y, z, w;

    public SerializableQuaternion(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }

    public SerializableQuaternion(Quaternion q)
    {
        x = q.x;
        y = q.y;
        z = q.z;
        w = q.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}

[System.Serializable]
public class ObjectData
{
    public string objectPrefabName;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
}
[System.Serializable]
public class UserSceneData
{
    public new string name;
    public int lenght; //x
    public int width; //z
    /*    public int height; //y*/
    public string scenePrefabName; //поменять на ресурс 
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public List<ObjectData> sceneObjects;
    public UserSceneData(UserScene scene, SceneDisplay sceneDisplay)
    {
        name = scene.name;
        lenght = scene.lenght;
        width = scene.width;
        scenePrefabName = scene.sceneModel.name;
        position = new SerializableVector3(scene.clone.transform.position);
        rotation = new SerializableQuaternion(scene.clone.transform.rotation);
        sceneObjects = new List<ObjectData>();
        for (int i = 0; i < sceneDisplay.sceneObjects.Count; i++)
        {
            ObjectData objectData = new ObjectData
            {
                position = new SerializableVector3(sceneDisplay.sceneObjects[i].transform.position),
                rotation = new SerializableQuaternion(sceneDisplay.sceneObjects[i].transform.rotation),
                objectPrefabName = sceneDisplay.sceneObjects[i].name,
            };
            sceneObjects.Add(objectData);
            Debug.Log($"Обьем:{sceneObjects.Capacity}\nОбьект:{objectData.objectPrefabName}, {objectData.position}");
        }
    }
}