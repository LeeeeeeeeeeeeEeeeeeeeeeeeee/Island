using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_SendToCan : MonoBehaviour
{
    private buttonPushing button_value;

    

    private float clickTime; // 클릭 중인 시간
    public float minClickTime = 1; // 최소 클릭시간
    private bool isClick; // 클릭 중인지 판단
    private bool iseventstart;
    private int money;

    public TextMeshProUGUI HowMuch;
    Sprite thisButtonsSprite;
    EventTrigger myBtn;
    Button mybtn_2;
    readonly Dictionary<string, int> Value = new Dictionary<string, int>()
    {
        { "PriceTag", 1 },
        { "Purchase", 2 },
        { "ButtonUpdate", 3 }
    };


    void Start()
    {
        //button_value = transform.root.GetComponent<buttonPushing>();   추후 삭제 가능
        myBtn = GetComponent<EventTrigger>(); //real버튼
        mybtn_2= GetComponent<Button>(); // 버튼 색상 변경을 위한 컴포넌트
        myBtn.enabled = false;
        HowMuch = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //가격표
        Texture2D tex = transform.GetChild(0).GetComponent<RawImage>().mainTexture as Texture2D; //현재 건물 스프라이트 받아오기
                
        iseventstart = false;
        thisButtonsSprite = Sprite.Create(tex, new Rect(0,0,tex.width ,tex.height) , new Vector2( 0.5f, 0.5f)); //현재 건물 스프라이트 받아오기2
        SwitchSyntaxBundle("PriceTag");

    }

    public void ButtonDown()
    {
        isClick = true;

        iseventstart = false;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        isClick = false;
        print(clickTime);

    }

    private void Update()
    {
        SwitchSyntaxBundle("ButtonUpdate");
        money = BuildingSystem.build_system.Money;
        // 클릭 중이라면

        if (isClick)
        {
            // 클릭시간 측정
            clickTime += Time.deltaTime;

            if (clickTime >= minClickTime && iseventstart == false)
            {
                SwitchSyntaxBundle("Purchase");
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
        BuildingSystem.build_system.LetsConstructor(this.gameObject.name,thisButtonsSprite);
    }

    //private void MoneyUpdate_Btn()더미데이터. 삭제가능
    //{  

    //    ColorBlock Block = mybtn_2.colors;

    //    switch (this.name)
    //    {
    //        case "House":
    //            break;

    //        case "Cafe":
    //            if(money >= 100)
    //            {
    //                myBtn.enabled= true;
    //                Block.pressedColor = new Color(0.784f,0.784f,0.784f);
    //                mybtn_2.colors = Block;
    //            }
    //            else
    //            {
    //                myBtn.enabled= false;
    //                Block.pressedColor = Color.red;
    //                mybtn_2.colors = Block;
    //            }
    //            break;

    //        case "Grocery":
    //            if (money >= 100)
    //            {
    //                myBtn.enabled = true;
    //                Block.pressedColor = new Color(0.784f, 0.784f, 0.784f);
    //                mybtn_2.colors = Block;
    //            }
    //            else
    //            {
    //                myBtn.enabled = false;
    //                Block.pressedColor = Color.red;
    //                mybtn_2.colors = Block;
    //            }
    //            break;
    //        default:
    //            break; //구매 불가시 색상변경 코드
    //    }
    //}

    private void SwitchSyntaxBundle(string ForWhat)
    {
        int ForWhatValue = Value[ForWhat];
        ColorBlock Block = mybtn_2.colors;

        /*Switch구문 묶음 함수
         * 
        딕셔너리 'Value' 사용
        함수 선언하고 사용할 기능을 매개변수로 전달
        Switch구문 안에서 if를 사용해 기능 구별

        ***건물 추가 시 실질적으로 추가해야 하는 곳
        */

        switch (this.name)
        {

            case "House":
                if(ForWhatValue == 1)
                {
                    myBtn.enabled = true;
                    HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["House"].ToString();
                }
                else if(ForWhatValue == 2)
                {

                }
                else if(ForWhatValue == 3)
                {

                }
                break;

            case "Cafe":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["Cafe"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    BuildingSystem.build_system.Money -= 100;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 100)
                    {
                        myBtn.enabled = true;
                        Block.pressedColor = new Color(0.784f, 0.784f, 0.784f);
                        mybtn_2.colors = Block;
                    }
                    else
                    {
                        myBtn.enabled = false;
                        Block.pressedColor = Color.red;
                        mybtn_2.colors = Block;
                    }
                }
                break;

            case "Grocery":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["Grocery"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    BuildingSystem.build_system.Money -= 100;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 100)
                    {
                        myBtn.enabled = true;
                        Block.pressedColor = new Color(0.784f, 0.784f, 0.784f);
                        mybtn_2.colors = Block;
                    }
                    else
                    {
                        myBtn.enabled = false;
                        Block.pressedColor = Color.red;
                        mybtn_2.colors = Block;
                    }
                }
                break;

            default:
                break; 
        }
    }

}
