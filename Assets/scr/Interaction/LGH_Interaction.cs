using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGH_Interaction : MonoBehaviour
{

    void CellClick()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null && hit.transform.gameObject.tag == "Animal"){
                GameObject click_obj = hit.transform.gameObject;
                CellInteraction(click_obj);
            }
        }
    }
    void CellInteraction(GameObject obj){
        //obj.GetComponent<CellCtrl>().InteractionGo();
        Debug.Log(obj.name + "와 상호작용 했습니다.");  //상호작용 시 실행되는 코드
        
            if (InteractionSystem.Interaction_system.IsSeeking == true)
            {
                InteractionSystem.Interaction_system.HideAndSeekInteraction2(obj);
            }
        
    
    }

    void Update()
    {
        CellClick();
    }
}
