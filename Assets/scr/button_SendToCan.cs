using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    TextMeshProUGUI HowMuch;
    Sprite thisButtonsSprite;

    void Start()
    {
        HowMuch = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Texture2D tex = transform.GetChild(0).GetComponent<RawImage>().mainTexture as Texture2D;
        button_value = transform.root.GetComponent<buttonPushing>();
        iseventstart = false;
        thisButtonsSprite = Sprite.Create(tex, new Rect(0,0,tex.width ,tex.height) , new Vector2( 0.5f, 0.5f));

        Debug.Log(tex);
        
        switch (this.name)
        {
            case "House":
                HowMuch.text = "집 : " + BuildingSystem.build_system.MoneyValue["House"];
                break;

            case "Cafe":
                HowMuch.text = "카페 : " + BuildingSystem.build_system.MoneyValue["Cafe"];
                break;
            case "Grocery":
                HowMuch.text = "식료품점 : " + BuildingSystem.build_system.MoneyValue["Grocery"];
                break;
            default:
                break;
        }
    }

    public void ButtonDown()
    {
        isClick = true;

        iseventstart = false;

        button_value.get_Sprite = thisButtonsSprite;

        
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
        button_value.LetsConstructor(this.gameObject.name);
    }


}
