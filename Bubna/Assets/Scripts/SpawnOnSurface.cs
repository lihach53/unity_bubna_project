using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnSurface : MonoBehaviour
{
    public GameObject prefab;
    public Camera mainCamera;
    public LayerMask ignoreLayer;  // ����� ����, ������� ����� ������������ ��� Raycast

    public SceneDisplay sceneDisplay; // �������� ������ �� SO scene

    public void Start()
    {
        prefab = gameObject.GetComponent<SceneDisplay>().model;
        ignoreLayer = LayerMask.GetMask("SpawnedLayer");
        sceneDisplay = GetComponent<SceneDisplay>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UserScene.is3D)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                Vector3 spawnPosition = hit.point;
                GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.FromToRotation(Vector3.up, hit.normal));

                spawnedObject.transform.SetParent(hit.transform);

                // ��������� ������������ ������� ���� "SpawnedLayer"
                spawnedObject.layer = LayerMask.NameToLayer("SpawnedLayer");
                sceneDisplay.sceneObjects.Add(spawnedObject); // ���������� � ������� ������
            }
        }

    }
}