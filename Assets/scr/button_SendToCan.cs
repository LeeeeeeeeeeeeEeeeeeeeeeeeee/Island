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

    TextMeshProUGUI HowMuch;
    Sprite thisButtonsSprite;
    EventTrigger myBtn;
    Button mybtn_2;
    Image myImage;


    void Start()
    {
        myBtn= GetComponent<EventTrigger>();
        mybtn_2= GetComponent<Button>();
        myImage= GetComponent<Image>();
        myBtn.enabled = false;
        HowMuch = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Texture2D tex = transform.GetChild(0).GetComponent<RawImage>().mainTexture as Texture2D;
        button_value = transform.root.GetComponent<buttonPushing>();
        iseventstart = false;
        thisButtonsSprite = Sprite.Create(tex, new Rect(0,0,tex.width ,tex.height) , new Vector2( 0.5f, 0.5f));

        Debug.Log(tex);
        
        switch (this.name)
        {
            case "House":
                myBtn.enabled = true;
                HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["House"].ToString();
                break;

            case "Cafe":
                HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["Cafe"].ToString();
                
                break;
            case "Grocery":
                HowMuch.text = "  <sprite=7> " + BuildingSystem.build_system.MoneyValue["Grocery"].ToString();
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
        MoneyUpdate_Btn();
        // 클릭 중이라면
        if (isClick)
        {
            // 클릭시간 측정
            clickTime += Time.deltaTime;

            if (clickTime >= minClickTime && iseventstart == false)
            {
                switch (this.name)
                {
                    case "House":
                        break;

                    case "Cafe":
                        BuildingSystem.build_system.Money -= 100;
                        break;
                    case "Grocery":
                        BuildingSystem.build_system.Money -= 100;
                        break;
                    default:
                        break;
                }
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

    private void MoneyUpdate_Btn()
    {
        money = BuildingSystem.build_system.Money;
        ColorBlock Block = mybtn_2.colors;
        
        switch (this.name)
        {
            case "House":
                break;

            case "Cafe":
                if(money >= 100)
                {
                    myBtn.enabled= true;
                    Block.pressedColor = new Color(0.784f,0.784f,0.784f);
                    mybtn_2.colors = Block;
                }
                else
                {
                    myBtn.enabled= false;
                    Block.pressedColor = Color.red;
                    mybtn_2.colors = Block;
                }
                break;
            case "Grocery":
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
                break;
            default:
                break;
        }
    }

}
