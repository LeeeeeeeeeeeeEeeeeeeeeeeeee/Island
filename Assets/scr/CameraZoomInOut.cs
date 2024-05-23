using UnityEngine;

public class CameraZoomInOut : MonoBehaviour
{
    public float speed;      //����,�ܾƿ��Ҷ� �ӵ� 

    void Update()
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            if (Camera.main.orthographicSize > 2 && Camera.main.orthographicSize < 11.8f)
            {
                Touch touch01 = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
                Touch touch02 = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

                //��ġ�� ���� ���� ��ġ���� ���� ������
                //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
                Vector2 touchZeroPrevPos = touch01.position - touch01.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
                Vector2 touchOnePrevPos = touch02.position - touch02.deltaPosition;

                // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
                float touchDeltaMag = (touch01.position - touch02.position).magnitude;

                // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Camera.main.orthographicSize += deltaMagnitudeDiff * speed * Time.deltaTime;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
        }

        if (Camera.main.orthographicSize <= 2f)
        {
            Camera.main.orthographicSize = 2f;
        }
        else if (Camera.main.orthographicSize >= 20f)
        {
            Camera.main.orthographicSize = 20f;
        }
    }
}