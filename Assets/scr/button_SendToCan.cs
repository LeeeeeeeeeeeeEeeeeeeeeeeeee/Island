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

    

    private float clickTime; // Ŭ�� ���� �ð�
    public float minClickTime = 1; // �ּ� Ŭ���ð�
    private bool isClick; // Ŭ�� ������ �Ǵ�
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
                HowMuch.text = "�� : " + BuildingSystem.build_system.MoneyValue["House"];
                break;

            case "Cafe":
                HowMuch.text = "ī�� : " + BuildingSystem.build_system.MoneyValue["Cafe"];
                break;
            case "Grocery":
                HowMuch.text = "�ķ�ǰ�� : " + BuildingSystem.build_system.MoneyValue["Grocery"];
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

    // ��ư Ŭ���� ������ ��
    public void ButtonUp()
    {
        isClick = false;
        print(clickTime);
    }

    private void Update()
    {
        // Ŭ�� ���̶��
        if (isClick)
        {
            // Ŭ���ð� ����
            clickTime += Time.deltaTime;

            if (clickTime >= minClickTime && iseventstart == false)
            {
                print("Ư�� ��� ����");
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
        button_value.LetsConstructor(this.gameObject.name);
    }


}
