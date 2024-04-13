using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float Speed; // 카메라 이동 속도
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

        
            if (Input.touchCount == 1) // 손가락 1개가 눌렸을 때
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                Touch touch = Input.GetTouch(0); // 첫번째 손가락 터치를 저장
                if (touch.phase == TouchPhase.Began) // 손가락이 화면에 터치됐을 때
                {
                    prePos = touch.position - touch.deltaPosition; // 이전 위치 저장
                }
                else if (touch.phase == TouchPhase.Moved) // 터치된 상태에서 움직였을 때
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                    Camera.main.transform.Translate(movePos);
                    prePos = touch.position - touch.deltaPosition;
                }
            }
        
            if (Input.touchCount > 1) // 손가락 1개가 눌렸을 때
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                Touch touch = Input.GetTouch(0); // 첫번째 손가락 터치를 저장
                if (touch.phase == TouchPhase.Began) // 손가락이 화면에 터치됐을 때
                {
                    prePos = touch.position - touch.deltaPosition; // 이전 위치 저장
                }
                else if (touch.phase == TouchPhase.Moved) // 터치된 상태에서 움직였을 때
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                    Camera.main.transform.Translate(movePos);
                    prePos = touch.position - touch.deltaPosition;
                }
            
        }
    }
    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
            if (transform.position.x > 4.5f || transform.position.x < -4.5f || transform.position.y > 4.5f || transform.position.y < -4.5f)
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