using UnityEngine;
using UnityEngine.UI;

public class CameraZoomInOut : MonoBehaviour
{
    public float speed; 

    void Update()
    {

        if (Input.touchCount == 2)
        {
            if (Camera.main.orthographicSize > 2 && Camera.main.orthographicSize < 11.8f)
            {
                Touch touch01 = Input.GetTouch(0);
                Touch touch02 = Input.GetTouch(1);

                
                Vector2 touchZeroPrevPos = touch01.position - touch01.deltaPosition; 
                Vector2 touchOnePrevPos = touch02.position - touch02.deltaPosition;

                
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; 
                float touchDeltaMag = (touch01.position - touch02.position).magnitude;

                
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Camera.main.orthographicSize += deltaMagnitudeDiff * speed * Time.deltaTime;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
        }

        if (Camera.main.orthographicSize <= 1.8f)
        {
            Camera.main.orthographicSize = 2f;
        }
        else if (Camera.main.orthographicSize >= 20.2f)
        {
            Camera.main.orthographicSize = 20f;
        }
    }
}