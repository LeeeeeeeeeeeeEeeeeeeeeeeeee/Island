using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float Speed; // ī�޶� �̵� �ӵ�
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    float height;
    float width;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void Update()
    {
        Speed = 0.2f - ((10 - Camera.main.orthographicSize) / 50);

        if(!ArchitectureSystem.build_system.isConstrutMode)
        {
            ArchitectureSystem.build_system.isCameraMode = true;
            if (Input.touchCount == 1) // �հ��� 1���� ������ ��
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                Touch touch = Input.GetTouch(0); // ù��° �հ��� ��ġ�� ����
                if (touch.phase == TouchPhase.Began) // �հ����� ȭ�鿡 ��ġ���� ��
                {
                    prePos = touch.position - touch.deltaPosition; // ���� ��ġ ����
                }
                else if (touch.phase == TouchPhase.Moved) // ��ġ�� ���¿��� �������� ��
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                    Camera.main.transform.Translate(movePos);
                    prePos = touch.position - touch.deltaPosition;
                }
            }

            if (Input.touchCount > 1) // �հ��� 1���� ������ ��
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                Touch touch = Input.GetTouch(0); // ù��° �հ��� ��ġ�� ����
                if (touch.phase == TouchPhase.Began) // �հ����� ȭ�鿡 ��ġ���� ��
                {
                    prePos = touch.position - touch.deltaPosition; // ���� ��ġ ����
                }
                else if (touch.phase == TouchPhase.Moved) // ��ġ�� ���¿��� �������� ��
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                    Camera.main.transform.Translate(movePos);
                    prePos = touch.position - touch.deltaPosition;
                }
            }

            ArchitectureSystem.build_system.isCameraMode = false;
        }   
    }
    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
            if (transform.position.x > 16f || transform.position.x < -16f || transform.position.y > 16f || transform.position.y < -16f)
            {
                transform.position = Vector3.Lerp(transform.position,
                                              movePos,
                                              Time.deltaTime * Speed);
            }
        
        float lx = mapSize.x - width / 2;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height / 2;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}