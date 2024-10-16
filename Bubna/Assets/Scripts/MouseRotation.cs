/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // �������� ��������
    private float rotationX;       // �������� �� ��� X
    private float rotationY;       // �������� �� ��� Y

    void Start()
    {
        // �������������� rotationY ������� ����� �������� ������� �� ��� Y
        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;
    }

    void Update()
    {
        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButton(1))
        {
            // �������� �������� �������� ���� �� ����
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // ������������ ����� ���� ��������
            rotationX += mouseY * rotationSpeed * Time.deltaTime;
            rotationY -= mouseX * rotationSpeed * Time.deltaTime;

            // ��������� �������� � �������
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // �������� ��������
    public float maxTiltAngle = 90.0f;     // ������������ ���� ������� � ��������

    private float rotationX = 0.0f;         // �������� �� ��� X
    private float rotationY = 0.0f;         // �������� �� ��� Y

    void Start()
    {
        
        // �������������� ������� ���� �������� ������� �� ���� X � Y
        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;
    }

    void Update()
    {

        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButton(1) && UserScene.is3D)
        {
            // �������� �������� �������� ���� �� ����
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.y = eulerAngles.y > 180 ? eulerAngles.y -= 360 : eulerAngles.y;

            // ������������ ����� ���� ��������
            if (eulerAngles.y >= -180 && eulerAngles.y <= 180)
                rotationY -= mouseX * rotationSpeed * Time.deltaTime; // �������� �� ��� Y
            else
                rotationY += mouseX * rotationSpeed * Time.deltaTime; // �������� �� ��� 

            // ������������ ����� ���� ������� �� ��� X
            rotationX += mouseY * rotationSpeed * Time.deltaTime;

            // ������������ ���� ������� �� ��� X � ��������� �� -maxTiltAngle �� maxTiltAngle
            rotationX = Mathf.Clamp(rotationX, -maxTiltAngle, maxTiltAngle);

            // ��������� �������� � �������
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        }
    }
}