using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    
    public enum Enum_Popup_Subject
    {
        Cooking,
        Storage_Cooking, 
        Storage_Building,
        Collection,
        Mission,
        Shop_Building,
        Production,
        Board
    }

    public Dictionary<Enum_Popup_Subject, int> Dic_Popup_Code_From_Enum = new Dictionary<Enum_Popup_Subject, int>()
    {
        {Enum_Popup_Subject.Cooking,           0},
        {Enum_Popup_Subject.Storage_Cooking,   1},
        {Enum_Popup_Subject.Storage_Building,  2},
        {Enum_Popup_Subject.Collection,        3},
        {Enum_Popup_Subject.Mission,           4},
        {Enum_Popup_Subject.Shop_Building,     5},
        {Enum_Popup_Subject.Production,        6},
        {Enum_Popup_Subject.Board,             7}
    };
    
    public Dictionary<string, Enum_Popup_Subject> Dic_Popup_Enum_From_String = new Dictionary<string, Enum_Popup_Subject>()
    {
        {"cooking",          Enum_Popup_Subject.Cooking},
        {"storage_cooking",  Enum_Popup_Subject.Storage_Cooking},
        {"storage_building", Enum_Popup_Subject.Storage_Building},
        {"collection",       Enum_Popup_Subject.Collection},
        {"mission",          Enum_Popup_Subject.Mission},
        {"shop_building",    Enum_Popup_Subject.Shop_Building},
        {"production",       Enum_Popup_Subject.Production},
        {"board",            Enum_Popup_Subject.Board}
    };
    
    [SerializeField] private GameObject[] arr_popups;
    [SerializeField] private GameObject[] arr_content_popups;

    [SerializeField] private GameObject[] arr_tab_shop_building;


    [SerializeField] private GameObject ui_popup_extended_background;
    [SerializeField] private GameObject ui_main_player_property_info;

    private int popup_type = 0;
    private int present_popup_code = 0; // 현재 팝업 화면을 판단하는 변수, 나중에 이걸 큐로 바꿔야 함
    private bool state_tab_shop_building = true;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Initialize();
    }
    
    // 하이라키 내 각 오브젝트 마다의 설정값을 초기화하는 함수 (모든 팝업 닫기(비활성화)) 
    public void Initialize()
    {
        ui_popup_extended_background.SetActive(false);

        foreach (var obj in arr_popups)
        {
            if(obj==true)
                obj.SetActive(false);
        }
    }
    
    // 콘텐츠를 여는 버튼의 OnClick()과 연결된 함수 
    // enum을 매개변수로 받을 수 없기 때문에, string으로 받아옴
    // string 매개변수의 값 가이드는 'Dic_Popup_Enum_From_String' 딕셔너리 참조
    public void OnClick_Open_Popup_Btn(string popup)
    {
        Open_Popup(Dic_Popup_Enum_From_String[popup]);
    }
    
    // 콘텐츠 팝업을 표시하는 함수
    // 매개 변수로 팝업 종류를 정의하는 enum값을 받음
    // enum 값으로 팝업 배열의 주소를 받고, 주소 내 팝업을 활성화함
    public void Open_Popup(Enum_Popup_Subject subject)
    {
        ui_popup_extended_background.SetActive(true);
        
        popup_type = 0;

        if (arr_content_popups[Dic_Popup_Code_From_Enum[subject]] == true)
        {
            arr_content_popups[Dic_Popup_Code_From_Enum[subject]].SetActive(true);
            present_popup_code = Dic_Popup_Code_From_Enum[subject];
        }
        
        switch (subject)
        {
            case Enum_Popup_Subject.Collection:
                Debug.Log("screen : 1");
                break;
            case Enum_Popup_Subject.Cooking:
                Debug.Log("screen : 2");
                break;
            case Enum_Popup_Subject.Mission:
                Debug.Log("screen : 3");
                break;
            case Enum_Popup_Subject.Shop_Building:
                Debug.Log("screen : 4");
                break;
            case Enum_Popup_Subject.Storage_Building:
                Debug.Log("screen : 5");
                break;
            case Enum_Popup_Subject.Storage_Cooking:
                Debug.Log("screen : 6");
                break;
            default:
                Debug.Log("Exception");
                break;
        }
        Debug.Log(subject);
        //Set_Transform_UI_Player_Property_Info(true);
    }

    public void Set_Transform_UI_Player_Property_Info(bool state)
    {
        if(state == true)
        {
            ui_main_player_property_info.SetActive(true);
        } else
        {}
    }

    public void OnClick_Extended_Popup_Background_Btn()
    {
        Close_Popup();
    }
    
    public void Close_Popup()
    {
        arr_content_popups[present_popup_code].SetActive(false);
        ui_popup_extended_background.SetActive(false);
    }

    public void Show_Btn()
    {
        
    }

    public void Exchange_Tab(int tab)
    {
        if (present_popup_code == 5)
        {
            if (tab == 1)
            {
                arr_tab_shop_building[1].SetActive(true);
                arr_tab_shop_building[0].SetActive(false);
            }
            else
            {
                arr_tab_shop_building[1].SetActive(false);
                arr_tab_shop_building[0].SetActive(true);
            }
        }
    }
}
