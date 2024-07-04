using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Цель, за которой будет следовать камера
    public float rotationSpeed = 3.0f; // Скорость вращения камеры при помощи мыши
    public float heightOffset = 1.5f; // Высота камеры относительно цели
    public float minYAngle = -20f; // Минимальный угол поворота по оси X
    public float maxYAngle = 20f; // Максимальный угол поворота по оси X
    public float destroyHeight = -10f; // Высота, при падении ниже которой объект будет уничтожен

    private Vector3 offset; // Смещение между камерой и целью
    private float currentYRotation = 0f; // Текущий угол вращения по оси X

    void Start()
    {
        // Вычисляем начальное смещение камеры относительно цели
        offset = new Vector3(transform.position.x - target.position.x, heightOffset, transform.position.z - target.position.z);
    }

    void LateUpdate()
    {
        // Получаем ввод от мыши по осям X и Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вращаем смещение вокруг цели по оси Y
        Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;

        // Обновляем угол вращения по оси X и ограничиваем его
        currentYRotation = Mathf.Clamp(currentYRotation - mouseY * rotationSpeed, minYAngle, maxYAngle);
        Quaternion camUpDownAngle = Quaternion.AngleAxis(currentYRotation, Vector3.right);

        // Обновляем позицию камеры, чтобы она всегда следовала за целью с текущим смещением, сохраняя фиксированную высоту
        Vector3 newPosition = target.position + offset;
        newPosition.y = target.position.y + heightOffset;
        transform.position = newPosition;

        // Применяем вращение по оси X
        transform.rotation = camUpDownAngle * transform.rotation;

        // Направляем камеру на цель
        transform.LookAt(target);

        // Проверка высоты цели и уничтожение объекта при падении ниже destroyHeight
        if (target.position.y < destroyHeight)
        {
            Destroy(target.gameObject);
        }
    }
}
