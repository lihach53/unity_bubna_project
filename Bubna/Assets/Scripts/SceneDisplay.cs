using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDisplay : MonoBehaviour
{
    public List<UserScene> scenes;
    public GameObject model;
    public int sceneNumber;

    public List<GameObject> sceneObjects; //(в них скорее всего будет класс таймлайна)
    public void ChooseScene(int sceneNumber)
    {
        this.sceneNumber = sceneNumber;
        scenes[sceneNumber].SetupModel(this);
    }
/*    public void SpawnPerson()
    {
        scenes[sceneNumber].SpawnPerson();
    }*/
    public void GoTo3D()
    {
        scenes[sceneNumber].GoTo3D();
    }

    public void Save()
    {
        scenes[sceneNumber].SaveScene(this);
    }
    public void Load()
    {
        scenes[sceneNumber].LoadScene(this);
    }
}

