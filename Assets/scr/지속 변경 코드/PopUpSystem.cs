using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

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


public class PopUpSystem : MonoBehaviour, IPointerClickHandler
{
    public static PopUpSystem instance;
    
    [SerializeField] GetButtonInfo[] _Buttons;

    public GameObject NowPopUp;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < _Buttons.Length; i++)
        {
            _Buttons[i].BtnS = _Buttons[i]._Content.GetComponentsInChildren<Button>();

            for (int j = 0; j < _Buttons[i].BtnS.Length; j++)
            {
                _Buttons[i].BtnS[j].onClick.AddListener(() => SendThisBtnObj(_Buttons[i].ButtonsType.ToString(), _Buttons[i].BtnS[j].gameObject));
            }
        }


    }

    public void SendThisBtnObj(string type, GameObject j)
    {
        switch (type)
        {

            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ds");
    }


}
