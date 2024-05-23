using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LGH_Visit_Cell : MonoBehaviour
{
    //디버깅용
    public bool visit_debug = false;
    public bool left_debug = false;
    public int debug_id = 0;

    //세포들의 리스트를 받아온다
    public GameObject[] cellList;

    //세포입장 --------------------------------------------------------------------------------------
    public void CellVisit(GameObject cell){
        Debug.Log(cell.name + " 방문");
        CellSpriteOn(cell);  //스프라이트 On
        cell.GetComponent<BoxCollider2D>().enabled = true; //콜라이더 on
        CellPosOn(cell);  //포지션 초기화

        CellCtrl cellCtrl = cell.GetComponent<CellCtrl>();
        cellCtrl.isOn = true;

    }
    void CellSpriteOn(GameObject cell){
        Debug.Log("스프라이트 초기화");
        cell.GetComponent<SpriteRenderer>().sprite = cell.GetComponent<CellCtrl>().cellSprite;
    }
    void CellPosOn(GameObject cell){
        Debug.Log("위치 초기화");
        float pos_x = Random.Range(-3.5f, 3.5f);
        float pos_y = Random.Range(-6.5f, 6.5f);
        Vector2 pos = new Vector2(pos_x, pos_y);
        cell.transform.position = pos;
    }
    //-----------------------------------------------------------------------------------------------
    //세포퇴장 --------------------------------------------------------------------------------------
    public void CellLeft(GameObject cell){
        Debug.Log(cell.name + " 퇴장");
        CellSpriteOff(cell);  //스프라이트 Off
        cell.GetComponent<BoxCollider2D>().enabled = false; //콜라이더 off
        CellCtrl cellCtrl = cell.GetComponent<CellCtrl>();
        cellCtrl.isOn = false;

    }
    void CellSpriteOff(GameObject cell){
        Debug.Log("스프라이트 초기화");
        cell.GetComponent<SpriteRenderer>().sprite = null;
    }
    //-----------------------------------------------------------------------------------------------

    private void Update() {
        if(visit_debug){
            CellVisit(cellList[debug_id]);
            visit_debug = false;
        }
        else if(left_debug){
            CellLeft(cellList[debug_id]);
            left_debug = false;
        }
    }
}
