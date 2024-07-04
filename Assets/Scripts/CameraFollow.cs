using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ����, �� ������� ����� ��������� ������
    public float rotationSpeed = 3.0f; // �������� �������� ������ ��� ������ ����
    public float heightOffset = 1.5f; // ������ ������ ������������ ����
    public float minYAngle = -20f; // ����������� ���� �������� �� ��� X
    public float maxYAngle = 20f; // ������������ ���� �������� �� ��� X
    public float destroyHeight = -10f; // ������, ��� ������� ���� ������� ������ ����� ���������

    private Vector3 offset; // �������� ����� ������� � �����
    private float currentYRotation = 0f; // ������� ���� �������� �� ��� X

    void Start()
    {
        // ��������� ��������� �������� ������ ������������ ����
        offset = new Vector3(transform.position.x - target.position.x, heightOffset, transform.position.z - target.position.z);
    }

    void LateUpdate()
    {
        // �������� ���� �� ���� �� ���� X � Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ������� �������� ������ ���� �� ��� Y
        Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;

        // ��������� ���� �������� �� ��� X � ������������ ���
        currentYRotation = Mathf.Clamp(currentYRotation - mouseY * rotationSpeed, minYAngle, maxYAngle);
        Quaternion camUpDownAngle = Quaternion.AngleAxis(currentYRotation, Vector3.right);

        // ��������� ������� ������, ����� ��� ������ ��������� �� ����� � ������� ���������, �������� ������������� ������
        Vector3 newPosition = target.position + offset;
        newPosition.y = target.position.y + heightOffset;
        transform.position = newPosition;

        // ��������� �������� �� ��� X
        transform.rotation = camUpDownAngle * transform.rotation;

        // ���������� ������ �� ����
        transform.LookAt(target);

        // �������� ������ ���� � ����������� ������� ��� ������� ���� destroyHeight
        if (target.position.y < destroyHeight)
        {
            Destroy(target.gameObject);
        }
    }
}
