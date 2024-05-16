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

    

    private float clickTime; // Ŭ�� ���� �ð�
    public float minClickTime = 1; // �ּ� Ŭ���ð�
    private bool isClick; // Ŭ�� ������ �Ǵ�
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
        //button_value = transform.root.GetComponent<buttonPushing>();   ���� ���� ����
        myBtn = GetComponent<EventTrigger>(); //real��ư
        mybtn_2= GetComponent<Button>(); // ��ư ���� ������ ���� ������Ʈ
        myBtn.enabled = false;
        HowMuch = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //����ǥ
        Texture2D tex = transform.GetChild(0).GetComponent<RawImage>().mainTexture as Texture2D; //���� �ǹ� ��������Ʈ �޾ƿ���
                
        iseventstart = false;
        thisButtonsSprite = Sprite.Create(tex, new Rect(0,0,tex.width ,tex.height) , new Vector2( 0.5f, 0.5f)); //���� �ǹ� ��������Ʈ �޾ƿ���2
        SwitchSyntaxBundle("PriceTag");

    }

    public void ButtonDown()
    {
        isClick = true;

        iseventstart = false;
    }

    // ��ư Ŭ���� ������ ��
    public void ButtonUp()
    {
        isClick = false;
        print(clickTime);

    }

    private void Update()
    {
        SwitchSyntaxBundle("ButtonUpdate");
        money = BuildingSystem.build_system.Money;
        // Ŭ�� ���̶��

        if (isClick)
        {
            // Ŭ���ð� ����
            clickTime += Time.deltaTime;

            if (clickTime >= minClickTime && iseventstart == false)
            {
                SwitchSyntaxBundle("Purchase");
                eventStart();
                ButtonUp();
                iseventstart= true;
            }
        }
        // Ŭ�� ���� �ƴ϶��
        else
        {
            // Ŭ���ð� �ʱ�ȭ
            clickTime = 0;
        }
    }

    private void eventStart()
    {
        BuildingSystem.build_system.LetsConstructor(this.gameObject.name,thisButtonsSprite);
    }

    //private void MoneyUpdate_Btn()���̵�����. ��������
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
    //            break; //���� �Ұ��� ���󺯰� �ڵ�
    //    }
    //}

    private void SwitchSyntaxBundle(string ForWhat)
    {
        int ForWhatValue = Value[ForWhat];
        ColorBlock Block = mybtn_2.colors;

        /*Switch���� ���� �Լ�
         * 
        ��ųʸ� 'Value' ���
        �Լ� �����ϰ� ����� ����� �Ű������� ����
        Switch���� �ȿ��� if�� ����� ��� ����

        ***�ǹ� �߰� �� ���������� �߰��ؾ� �ϴ� ��
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
