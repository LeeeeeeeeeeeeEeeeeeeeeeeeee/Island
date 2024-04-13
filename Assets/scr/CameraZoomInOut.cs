using UnityEngine;

public class CameraZoomInOut : MonoBehaviour
{
    public float speed;      //줌인,줌아웃할때 속도 

    void Update()
    {
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            if (Camera.main.orthographicSize > 2 && Camera.main.orthographicSize < 11.8f)
            {
                Touch touch01 = Input.GetTouch(0); //첫번째 손가락 터치를 저장
                Touch touch02 = Input.GetTouch(1); //두번째 손가락 터치를 저장

                //터치에 대한 이전 위치값을 각각 저장함
                //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
                Vector2 touchZeroPrevPos = touch01.position - touch01.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
                Vector2 touchOnePrevPos = touch02.position - touch02.deltaPosition;

                // 각 프레임에서 터치 사이의 벡터 거리 구함
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
                float touchDeltaMag = (touch01.position - touch02.position).magnitude;

                // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Camera.main.orthographicSize += deltaMagnitudeDiff * speed * Time.deltaTime;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
        }

        if (Camera.main.orthographicSize <= 2)
        {
            Camera.main.orthographicSize = 2.1f;
        }
        else if (Camera.main.orthographicSize >= 11.8f)
        {
            Camera.main.orthographicSize = 11.7f;
        }
    }
}