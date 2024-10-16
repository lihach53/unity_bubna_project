using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Scene", menuName ="SO/Scene", order =51)]
public class UserScene : ScriptableObject
{
    public new string name;
    public int lenght; //x
    public int width; //z
    /*    public int height; //y*/

    public GameObject sceneModel;
    public GameObject clone;

    public static bool is3D = true;


    public void ToSize()
    {
        Vector3 scale = sceneModel.transform.localScale;
        scale.x *= lenght;
/*        scale.y *= height;*/
        scale.z *= width;
        sceneModel.transform.localScale = scale;
    }
    public void SetupModel(SceneDisplay sceneDisplay)
    {
        ToSize();
        clone = Instantiate(this.sceneModel);
        clone.AddComponent<MouseRotation>();
        if(sceneDisplay.sceneObjects.Count > 0)
        {
            SpawnPerson(sceneDisplay);
        }
    }

    public void SpawnPerson(SceneDisplay sceneDisplay)
    {
        foreach(var o in sceneDisplay.sceneObjects)
        {
            Instantiate(o,clone.transform);
        }
    }
    public void GoTo3D()
    {
        if (UserScene.is3D)
        {
            clone.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        }
        else
        {
            clone.transform.rotation = Quaternion.Euler(-30.0f, 0.0f, 0.0f);
        }
        UserScene.is3D = !UserScene.is3D;
    }

    public void SaveScene(SceneDisplay sceneDisplay)
    {
        SaveSystem.SaveScene(this, sceneDisplay);
    }
    
    public void LoadScene(SceneDisplay sceneDisplay)
    {
        UserSceneData data = SaveSystem.LoadScene(this);
        name = data.name;
        lenght = data.lenght;
        width = data.width;
        sceneModel = Resources.Load<GameObject>(data.scenePrefabName);
        sceneModel.transform.position = data.position.ToVector3();
        sceneModel.transform.rotation = data.rotation.ToQuaternion();
        for(int i = 0; i < data.sceneObjects.Count; i++)
        {
            sceneDisplay.sceneObjects.Add(Resources.Load<GameObject>(data.sceneObjects[i].objectPrefabName));
            Debug.Log($"название префаба:{data.sceneObjects[i].objectPrefabName}");
            sceneDisplay.sceneObjects[i].transform.position = data.sceneObjects[i].position.ToVector3();
            sceneDisplay.sceneObjects[i].transform.rotation = data.sceneObjects[i].rotation.ToQuaternion();
        }
        if (sceneDisplay.sceneObjects.Count == 0)
            Debug.Log("scneObjects ПУСТОЙ!!!");
        SetupModel(sceneDisplay);
    }
}
