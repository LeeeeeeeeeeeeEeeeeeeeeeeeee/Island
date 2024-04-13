using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class button_SendToCan : MonoBehaviour
{
    private buttonPushing button_value;

    

    private float clickTime; // 클릭 중인 시간
    public float minClickTime = 1; // 최소 클릭시간
    private bool isClick; // 클릭 중인지 판단
    private bool iseventstart;

    void Start()
    {
        button_value = transform.root.GetComponent<buttonPushing>();
        iseventstart = false;
    }

    public void ButtonDown()
    {
        isClick = true;

        iseventstart = false;
        button_value.get_Sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        isClick = false;
        print(clickTime);
    }

    private void Update()
    {
        // 클릭 중이라면
        if (isClick)
        {
            // 클릭시간 측정
            clickTime += Time.deltaTime;

            if (clickTime >= minClickTime && iseventstart == false)
            {
                print("특정 기능 수행");
                eventStart();
                ButtonUp();
                iseventstart= true;
            }
        }
        // 클릭 중이 아니라면
        else
        {
            // 클릭시간 초기화
            clickTime = 0;
        }
    }

    private void eventStart()
    {
        button_value.LetsConstructor();
    }


}
