using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;
using System.Linq;
using TMPro;
// using static UnityEditor.Progress;

[System.Serializable]
struct GetButtonInfo
{
    public enum MyEnum
    {
        BuildShop,
        FurnitureShop,
        BuildPlace,
        CellInfo,
        CookInfo
    }
    [SerializeField] public MyEnum ButtonsType;
    [SerializeField] public GameObject _Content;
    [SerializeField] public Button[] BtnS;
    [SerializeField] public GameObject PopupTarget;
}


public class PopUpSystem : MonoBehaviour, IPointerClickHandler
{
    public static PopUpSystem instance;
    
    [SerializeField] GetButtonInfo[] _Buttons;

    public GameObject NowPopUp;

    void Start()
    {
        instance = this;
        for (int i = 0; i < _Buttons.Length; i++)
        {
            _Buttons[i].BtnS = _Buttons[i]._Content.GetComponentsInChildren<Button>();

            for (int j = 0; j < _Buttons[i].BtnS.Length; j++)
            {
                int temp = j;
                int temp2 = i;
                _Buttons[temp2].BtnS[temp].onClick.AddListener(() => SendThisBtnObj(_Buttons[temp2].ButtonsType.ToString(), _Buttons[temp2].BtnS[temp].gameObject));

                if(i==2)
                {
                    _Buttons[2].BtnS[j].interactable= false;
                }
            }

            
        }
    }

    public int k = 0;
    public Button NowBtnList;
    public void SendThisBtnObj(string type, GameObject j)
    {
        GameObject g;
        j.TryGetComponent(out Button b);
        
        switch (type)
        {
            case "BuildShop":
                g = _Buttons[0].PopupTarget.gameObject;
                
                foreach (Button item in _Buttons[0].BtnS)
                {
                    if(item == b)
                    {
                        g.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                        //price

                        g.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                        //deco

                        g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
                        g.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.rect.size/3.5f;
                        //buildimage

                        g.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
                        //buildname

                        
                        break;
                    }
                }
                break;


            case "FurnitureShop":
                g = _Buttons[1].PopupTarget.gameObject;

                foreach (Button item in _Buttons[1].BtnS)
                {
                    if (item == b)
                    {
                        g.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                        //price

                        g.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                        //deco

                        g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
                        g.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.rect.size / 3.5f;
                        //buildimage

                        g.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
                        //buildname


                        break;
                    }
                }
                break;


            case "BuildPlace":
                g = _Buttons[2].PopupTarget.gameObject;

                foreach (Button item in _Buttons[2].BtnS)
                {
                    if (item == b)
                    {
                        Sprite TempImg = item.transform.GetChild(0).GetComponent<Image>().sprite;

                        g.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = ArchitectureSystem.build_system.DecoValue[TempImg.name].ToString();

                        g.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = TempImg;
                        g.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta = TempImg.rect.size / 3.5f;

                        g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = TempImg.name;

                        NowBtnList = item;
                        break;
                    }
                }
                break;



            case "CellInfo":
                break;


            default:
                break;
        }
    }

    public void Purchase(GameObject game)
    {
        if (ArchitectureSystem.build_system.Money >= int.Parse(game.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text))
        {
            Inventory.Instance.AlertText.text = game.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text + "를 구매하였습니다!";
            Inventory.Instance.AlertText.color = Color.white;

            SoundManager.instance.PlaySound("Buy");

            for (int i = 0; i < _Buttons[2].BtnS.Length; i++)
            {
                if (_Buttons[2].BtnS[i].interactable == false)
                {
                    _Buttons[2].BtnS[i].interactable = true;
                    _Buttons[2].BtnS[i].transform.GetChild(0).GetComponent<Image>().sprite = game.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
                    _Buttons[2].BtnS[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = game.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.rect.size / 10;

                    break;
                }
            }

            ArchitectureSystem.build_system.Money -= int.Parse(game.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text);
        }
        else
        {
            Inventory.Instance.AlertText.text = "유미가 부족합니다!";
            Inventory.Instance.AlertText.color = Color.white;
        }
    }

    public void Install(Sprite Defalut)
    {
        ArchitectureSystem.build_system.LetsConstructor(NowBtnList.transform.GetChild(0).GetComponent<Image>().sprite.name, NowBtnList.transform.GetChild(0).GetComponent<Image>().sprite);
        NowBtnList.transform.GetChild(0).GetComponent<Image>().sprite = Defalut;
        NowBtnList.interactable = false;
    }

    public void ReStroage(GameObject Obj)
    {
        for (int i = 0; i < _Buttons[2].BtnS.Length; i++)
        {
            if (_Buttons[2].BtnS[i].interactable == false)
            {
                _Buttons[2].BtnS[i].interactable = true;
                _Buttons[2].BtnS[i].transform.GetChild(0).GetComponent<Image>().sprite = Obj.GetComponent<SpriteRenderer>().sprite;
                _Buttons[2].BtnS[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Obj.GetComponent<SpriteRenderer>().sprite.rect.size / 10;

                break;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
