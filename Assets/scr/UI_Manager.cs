using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject ui_popup_extended_background;
    [SerializeField] private GameObject ui_main_player_property_info;
    
    // Start is called before the first frame update
    void Start()
    {
        ui_popup_extended_background.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void OnClick_Open_Popup_Btn(string subject)
    {
        Open_Popup(subject);
    }

    public void Open_Popup(string popup_subject)
    {
        ui_popup_extended_background.SetActive(true);
        if (popup_subject == "content")
        {
            Debug.Log("check");
            Set_Transform_UI_Player_Property_Info(true);
        }
    }

    public void Set_Transform_UI_Player_Property_Info(bool state)
    {
        if(state == true)
        {
            ui_main_player_property_info.SetActive(true);
            ui_main_player_property_info.transform.SetAsFirstSibling();
            Debug.Log("check_First_Sibling");
        } else
        {}

    }
}
