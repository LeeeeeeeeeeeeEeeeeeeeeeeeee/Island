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
    public Sprite thisButtonsSprite;
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
        //Texture2D tex = transform.GetChild(0).GetComponent<RawImage>().mainTexture as Texture2D; //���� �ǹ� ��������Ʈ �޾ƿ���
                
        iseventstart = false;
        //thisButtonsSprite = Sprite.Create(tex, new Rect(0,0,tex.width ,tex.height) , new Vector2( 0.5f, 0.5f)); //���� �ǹ� ��������Ʈ �޾ƿ���2
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
        money = ArchitectureSystem.build_system.Money;
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
        ArchitectureSystem.build_system.LetsConstructor(this.gameObject.name,thisButtonsSprite);
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
            case "빨간 설탕 유리 꽃":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["빨간 설탕 유리 꽃"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "노란 설탕 유리 꽃":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["노란 설탕 유리 꽃"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "파란 설탕 유리 꽃":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["파란 설탕 유리 꽃"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "비스킷 의자":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["비스킷 의자"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "아이스크림 테이블":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["아이스크림 테이블"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "마카롱 쿠션":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["마카롱 쿠션"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "2단 마카롱 쿠션":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["2단 마카롱 쿠션"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "딸기우유 연못":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["딸기우유 연못"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "솜사탕 구름 1":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["솜사탕 구름 1"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "솜사탕 구름 2":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["솜사탕 구름 2"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "캔디 가로등":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["캔디 가로등"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "롤리팝 캔디 나무":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["롤리팝 캔디 나무"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "마카롱 나무":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["마카롱 나무"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "초코 분수":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["초코 분수"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "녹차 푸딩 산":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["녹차 푸딩 산"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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
            case "초코 푸딩 산":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["딸초코 푸딩 산"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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

            case "거대한 롤리팝 마시멜로우 언덕":
                if (ForWhatValue == 1)
                {
                    HowMuch.text = "  <sprite=7> " + ArchitectureSystem.build_system.MoneyValue["거대한 롤리팝 마시멜로우 언덕"].ToString();
                }
                else if (ForWhatValue == 2)
                {
                    ArchitectureSystem.build_system.Money -= 0;
                }
                else if (ForWhatValue == 3)
                {
                    if (money >= 0)
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
