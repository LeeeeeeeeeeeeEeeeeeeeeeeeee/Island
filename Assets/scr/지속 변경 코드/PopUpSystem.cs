using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;
using System.Linq;
using TMPro;


[System.Serializable]
struct GetButtonInfo
{
    public enum MyEnum
    {
        BuildShop,
        BuildPlace,
        CellInfo,
        CookInfo
    }
    [SerializeField] public MyEnum ButtonsType;
    [SerializeField] public GameObject _Content;
    [SerializeField] public Button[] BtnS;
    [SerializeField] public GameObject PopupTarget;
}

[System.Serializable]
struct CellSimplifyInfo
{
    int CellID;
    List<Sprite> Favorite_Building;
    List<Sprite> Favorite_Food;
}

public class PopUpSystem : MonoBehaviour, IPointerClickHandler
{
    public static PopUpSystem instance;
    
    [SerializeField] GetButtonInfo[] _Buttons;
    [SerializeField] CellSimplifyInfo[] _SimplifyInfos;

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

                if(i==1)
                {
                    _Buttons[1].BtnS[j].interactable= false;
                }
            }

            
        }
    }

    public int k = 0;
    public Button NowBtnList;
    public void SendThisBtnObj(string type, GameObject j)
    {
        GameObject g;
        GameObject z;
        j.TryGetComponent(out Button b);
        
        switch (type)
        {
            case "BuildShop":
                g = _Buttons[0].PopupTarget.gameObject;
                z = _Buttons[2].PopupTarget.gameObject;
                
                foreach (Button item in _Buttons[0].BtnS)
                {
                    if(item == b)
                    {
                        g.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text;
                        //price

                        g.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
                        //deco

                        g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
                        //buildimage

                        g.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
                        //buildname


                        break;
                    }
                }

                foreach (Button item in _Buttons[2].BtnS)
                {
                    if (item == b)
                    {
                        g.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text;
                        //price

                        g.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
                        //deco

                        g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
                        //buildimage

                        g.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = item.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
                        //buildname


                        break;
                    }
                }
                break;



            case "BuildPlace":
                g = _Buttons[1].PopupTarget.gameObject;

                foreach (Button item in _Buttons[1].BtnS)
                {
                    if (item == b)
                    {
                        g.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = item.transform.GetComponent<Image>().sprite;

                        g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text =
                            item.transform.GetComponent<Image>().sprite.name;

                        NowBtnList = item;
                        break;
                    }
                }
                break;



            case "CellInfo":
                g = _Buttons[3].PopupTarget.gameObject;

                foreach (Button item in _Buttons[3].BtnS)
                {
                    if(item==b)
                    {
                        if(item == _Buttons[3].BtnS[0])
                        {
                            Image[] = g.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
                        }
                    }
                }

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

            _Buttons[1].BtnS[k].interactable = true;
            _Buttons[1].BtnS[k].GetComponent<Image>().sprite = game.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;

            k++;
        }
        else
        {
            Inventory.Instance.AlertText.text = "유미가 부족합니다!";
            Inventory.Instance.AlertText.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
