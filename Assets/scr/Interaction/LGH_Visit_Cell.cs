using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LGH_Visit_Cell : MonoBehaviour
{
    //디버깅용
    public bool visit_debug = false;
    public bool left_debug = false;
    public int debug_id;

    //세포들의 리스트를 받아온다
    public GameObject[] cellList;

    private void Start()
    {
        debug_id = Random.Range(0, cellList.Length);
        visit_debug = true;
    }

    //세포입장 --------------------------------------------------------------------------------------
    public void CellVisit(GameObject cell){
        Debug.Log(cell.name + " 방문");
        cell.gameObject.SetActive(true);
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
        List<GameObject> Buildings = ArchitectureSystem.build_system.BuildingList;
        GameObject Select = Buildings[Random.Range(0, Buildings.Count)];
        Vector2 pos = Select.transform.position + (Vector3)Random.insideUnitCircle + new Vector3(0, -1, 0);
            //new Vector2(Select.transform.position.x + (Select.GetComponent<SpriteRenderer>().size.x/2 * Random.R), Select.transform.position.y);
        cell.transform.position = pos;
    }
    //-----------------------------------------------------------------------------------------------
    //세포퇴장 --------------------------------------------------------------------------------------
    public void CellLeft(GameObject cell){
        Debug.Log(cell.name + " 퇴장");
        cell.gameObject.SetActive(false);
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
