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

    //vistTime----------------------------------
    public float v_cool_time;
    public float v_timer;

    //vistTime----------------------------------

    //세포들의 리스트를 받아온다
    public GameObject[] cellList;
    public int Max_Cell_Count;
    public int now_Cell_Count = 0;
    public bool[] now_stay_Cells;

    //세포입장 --------------------------------------------------------------------------------------
    public void CellVisit(GameObject cell){
        
        Debug.Log(cell.name + " 방문");
        cell.gameObject.SetActive(true);
        CellPosOn(cell);  //포지션 초기화

        CellCtrl cellCtrl = cell.GetComponent<CellCtrl>();
        cellCtrl.isOn = true;
        now_Cell_Count++;

    }
    void CellSpriteOn(GameObject cell){
        Debug.Log("스프라이트 초기화");
        cell.GetComponent<SpriteRenderer>().sprite = cell.GetComponent<CellCtrl>().cellSprite;
    }
    void CellPosOn(GameObject cell){
        Debug.Log("위치 초기화");
        GameObject Select;
        List<GameObject> Buildings = ArchitectureSystem.build_system.BuildingList;
        while(true){
            
            Select = Buildings[Random.Range(0,Buildings.Count)];

            if(Select.GetComponent<Cell_In_Building>().isVisit == false){
                Select.GetComponent<Cell_In_Building>().isVisit = true;

                Vector2 pos;

                if (cell.name == "출출이")
                {
                    pos = Select.transform.position + new Vector3(0, 0.5f, 0);
                }
                else
                {
                    pos = Select.transform.position + new Vector3(0, Position_Set(Select), 0);
                }

                cell.transform.position = pos;
                cell.GetComponent<CellCtrl>().staying_Building = Select.GetComponent<Cell_In_Building>();
                break;
            }

        }

    }
    //-----------------------------------------------------------------------------------------------
    //세포퇴장 --------------------------------------------------------------------------------------
    public void CellLeft(GameObject cell){
        Debug.Log(cell.name + " 퇴장");
        cell.gameObject.SetActive(false);
        CellCtrl cellCtrl = cell.GetComponent<CellCtrl>();
        cellCtrl.isOn = false;
        now_Cell_Count--;

        cell.GetComponent<CellCtrl>().staying_Building.isVisit = false;
    }
    void CellSpriteOff(GameObject cell){
        Debug.Log("스프라이트 초기화");
        cell.GetComponent<SpriteRenderer>().sprite = null;
    }
    //-----------------------------------------------------------------------------------------------
    
    float Position_Set(GameObject Select){
        
        switch(Select.name){
            case "빨간 설탕 유리 꽃":
                return -0.2f;
            case "딸기우유 연못":
                return -0.15f;

            default:
                return -1f;
        }
    }
    void Start() {
        v_timer = v_cool_time;
    }
    private void Update() {
        Max_Cell_Count = ArchitectureSystem.build_system.BuildingList.Count;

        if(now_Cell_Count < Max_Cell_Count){
            v_timer += Time.deltaTime;
            if(v_timer >= v_cool_time){
                int n;
                while (true)
                {
                    n = Random.Range(0, 6);
                    if (now_stay_Cells[n] == false)
                    {
                        now_stay_Cells[n] = true;
                        CellVisit(cellList[n]);
                        v_timer = 0;
                        break;
                    }
                }

            }
        }

        if(left_debug == true){
            left_debug = false;
            CellLeft(cellList[debug_id]);
            now_stay_Cells[debug_id] = false;
        }
    }

}
