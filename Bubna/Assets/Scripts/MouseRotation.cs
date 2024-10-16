/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // Скорость вращения
    private float rotationX;       // Вращение по оси X
    private float rotationY;       // Вращение по оси Y

    void Start()
    {
        // Инициализируем rotationY текущим углом поворота объекта по оси Y
        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;
    }

    void Update()
    {
        // Проверяем, зажата ли левая кнопка мыши
        if (Input.GetMouseButton(1))
        {
            // Получаем значения движения мыши по осям
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Рассчитываем новые углы вращения
            rotationX += mouseY * rotationSpeed * Time.deltaTime;
            rotationY -= mouseX * rotationSpeed * Time.deltaTime;

            // Применяем вращение к объекту
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
    public float rotationSpeed = 100.0f;  // Скорость вращения
    public float maxTiltAngle = 90.0f;     // Максимальный угол наклона в градусах

    private float rotationX = 0.0f;         // Вращение по оси X
    private float rotationY = 0.0f;         // Вращение по оси Y

    void Start()
    {
        
        // Инициализируем текущие углы поворота объекта по осям X и Y
        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;
    }

    void Update()
    {

        // Проверяем, зажата ли левая кнопка мыши
        if (Input.GetMouseButton(1) && UserScene.is3D)
        {
            // Получаем значения движения мыши по осям
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.y = eulerAngles.y > 180 ? eulerAngles.y -= 360 : eulerAngles.y;

            // Рассчитываем новые углы вращения
            if (eulerAngles.y >= -180 && eulerAngles.y <= 180)
                rotationY -= mouseX * rotationSpeed * Time.deltaTime; // Вращение по оси Y
            else
                rotationY += mouseX * rotationSpeed * Time.deltaTime; // Вращение по оси 

            // Рассчитываем новый угол наклона по оси X
            rotationX += mouseY * rotationSpeed * Time.deltaTime;

            // Ограничиваем угол наклона по оси X в диапазоне от -maxTiltAngle до maxTiltAngle
            rotationX = Mathf.Clamp(rotationX, -maxTiltAngle, maxTiltAngle);

            // Применяем вращение к объекту
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        }
    }
}