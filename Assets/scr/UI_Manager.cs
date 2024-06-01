using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    
    // 나중에 뎁스별로 팝업은 별도로 관리해야할듯
    
    public enum Enum_Popup_Subject
    {
        Cooking,
        Storage_Cooking, 
        Storage_Building,
        Collection,
        Mission,
        Shop_Building,
        Production,
        Board,
        // 이하 2차 뎁스
        Character_Information,
        Shop_Furniture_List,
        // 이하 의사 확인 팝업 뎁스
        Check_Intention_Purchase_Building,
        Check_Intention_Place_Building
    }

    public Dictionary<Enum_Popup_Subject, int> Dic_Popup_Code_From_Enum = new Dictionary<Enum_Popup_Subject, int>()
    {
        {Enum_Popup_Subject.Cooking,                            0},
        {Enum_Popup_Subject.Storage_Cooking,                    1},
        {Enum_Popup_Subject.Storage_Building,                   2},
        {Enum_Popup_Subject.Collection,                         3},
        {Enum_Popup_Subject.Mission,                            4},
        {Enum_Popup_Subject.Shop_Building,                      5},
        {Enum_Popup_Subject.Production,                         6},
        {Enum_Popup_Subject.Board,                              7},
        {Enum_Popup_Subject.Character_Information,              8},
        {Enum_Popup_Subject.Shop_Furniture_List,                9},
        {Enum_Popup_Subject.Check_Intention_Purchase_Building,  10},
        {Enum_Popup_Subject.Check_Intention_Place_Building,     11}
    };
    
    public Dictionary<string, Enum_Popup_Subject> Dic_Popup_Enum_From_String = new Dictionary<string, Enum_Popup_Subject>()
    {
        {"cooking",                            Enum_Popup_Subject.Cooking},
        {"storage_cooking",                    Enum_Popup_Subject.Storage_Cooking},
        {"storage_building",                   Enum_Popup_Subject.Storage_Building},
        {"collection",                         Enum_Popup_Subject.Collection},
        {"mission",                            Enum_Popup_Subject.Mission},
        {"shop_building",                      Enum_Popup_Subject.Shop_Building},
        {"production",                         Enum_Popup_Subject.Production},
        {"board",                              Enum_Popup_Subject.Board},
        {"character_information",              Enum_Popup_Subject.Character_Information},
        {"shop_furniture_list",                Enum_Popup_Subject.Shop_Furniture_List},
        {"check_intention_purchase_building",  Enum_Popup_Subject.Check_Intention_Purchase_Building},
        {"check_intention_place_building",     Enum_Popup_Subject.Check_Intention_Place_Building} 
    };
    
    [SerializeField] private GameObject[] arr_popups;
    [SerializeField] private GameObject[] arr_content_popups;

    [SerializeField] private GameObject[] arr_tab_shop_building;
    [SerializeField] private GameObject[] arr_tab_character_information;
    [SerializeField] private GameObject[] arr_tab_collection;

    [SerializeField] private GameObject[] arr_popup_extended_background;
    [SerializeField] private GameObject ui_main_player_property_info;

    [SerializeField] private int present_tab_num_shop_building;
    [SerializeField] private int present_tab_num_character_information;
    [SerializeField] private int present_tab_num_collection;
    
    [SerializeField] private int present_popup_code = -1; // 현재 팝업 화면을 판단하는 변수, 나중에 이걸 큐로 바꿔야 함

    [SerializeField] private Stack<int> stack_present_popup_code = new Stack<int>();
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Initialize();
        Initialize_Tap(-2);
    }
    
    // 하이라키 내 각 오브젝트 마다의 설정값을 초기화하는 함수 (모든 팝업 닫기(비활성화)) 
    public void Initialize()
    {
        foreach (var obj in arr_popup_extended_background)
        {
            obj.SetActive(false);
        }
        
        foreach (var obj in arr_popups)
        {
            if(obj==true)
                obj.SetActive(false);
        }
        present_popup_code = -1;
    }
    
    public void Initialize_Tap(int num = -1)
    {
        if (num == -2)
        {
            foreach (var obj in arr_tab_shop_building)
            {
                obj.SetActive(false);
            }
        
            foreach (var obj in arr_tab_character_information)
            {
                obj.SetActive(false);
            }
        
            foreach (var obj in arr_tab_collection)
            {
                obj.SetActive(false);
            }
        
            arr_tab_shop_building[0].SetActive(true);
            arr_tab_character_information[0].SetActive(true);
            arr_tab_collection[0].SetActive(true);
            
            present_tab_num_shop_building = 0;
            present_tab_num_character_information = 0;
            present_tab_num_collection = 0;
        } else
        {
            switch (num)
            {
                case 3:
                    foreach (var obj in arr_tab_collection)
                    {
                        obj.SetActive(false);
                    }
                    arr_tab_collection[0].SetActive(true);
                    break;
                case 5:
                    foreach (var obj in arr_tab_shop_building)
                    {
                        obj.SetActive(false);
                    }
                    arr_tab_shop_building[0].SetActive(true);
                    break;
                case 8:
                    foreach (var obj in arr_tab_character_information)
                    {
                        obj.SetActive(false);
                    }
                    arr_tab_character_information[0].SetActive(true);
                    break;
                default:
                    break;
            }
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
        
        int subject_num = Dic_Popup_Code_From_Enum[subject];
        
        if (subject_num < 8)
            arr_popup_extended_background[0].SetActive(true);
        else if (subject_num < 10)
            arr_popup_extended_background[1].SetActive(true);
        else
            arr_popup_extended_background[2].SetActive(true);
        
        if (arr_content_popups[subject_num] == true)
        {
            arr_content_popups[subject_num].SetActive(true);

            PopUpSystem.instance.NowPopUp = arr_content_popups[subject_num];

            if (present_popup_code == -1)
                present_popup_code = subject_num;
            else
            {
                Debug.Log("PUSH " + present_popup_code);
                stack_present_popup_code.Push(present_popup_code);
                present_popup_code = subject_num;
            }
        }
        /*
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
        */
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
        if (present_popup_code < 8)
            arr_popup_extended_background[0].SetActive(false);
        else if (present_popup_code < 10)
            arr_popup_extended_background[1].SetActive(false);
        else
            arr_popup_extended_background[2].SetActive(false);

        arr_content_popups[present_popup_code].SetActive(false);

        Initialize_Tap(present_popup_code);
        
        if (stack_present_popup_code.Count > 0)
        {
            Debug.Log("Stack Pop : " + present_popup_code);
            present_popup_code = stack_present_popup_code.Pop();
        }
        else
        {
            Debug.Log("Stack : -1 ");
            present_popup_code = -1;
        }
        
        if (present_popup_code == -1)
        {
            Initialize_Tap(-2);
        }
    }

    public void Show_Btn()
    {
        
    }

    public void Exchange_Tab(int tab)
    {
        if (present_popup_code == 3)
        {
            arr_tab_collection[present_tab_num_collection].SetActive(false);
            arr_tab_collection[tab].SetActive(true);
            present_tab_num_collection = tab;
        }
        if (present_popup_code == 5)
        {
            arr_tab_shop_building[present_tab_num_shop_building].SetActive(false);
            arr_tab_shop_building[tab].SetActive(true);
            present_tab_num_shop_building = tab;
        }
        if (present_popup_code == 8)
        {
            arr_tab_character_information[present_tab_num_character_information].SetActive(false);
            arr_tab_character_information[tab].SetActive(true);
            present_tab_num_character_information = tab;
        }
    }
}
