using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionSystem : MonoBehaviour
{
    public GameObject _GameManeger;
    public List<GameObject> _BuildingList
    {
        get { return ArchitectureSystem.build_system.BuildingList; }
    }
    public List<GameObject> CellList = new List<GameObject>();
    public List<GameObject> CurrentCellList = new List<GameObject>();

    private void Start()
    {
        CellList = _GameManeger.GetComponent<CellManage>().cells;
        StartCoroutine(AddBuilding_InterActionCell());
    }

    private IEnumerator AddBuilding_InterActionCell()
    {
        int k = _BuildingList.Count;
        InputCurrentCell();
        yield return new WaitUntil(() => k < _BuildingList.Count);


        
        if (CurrentCellList.Count > 0)
        {
            //CurrentCellList[k].transform.position = _BuildingList[k].transform.position;
            _BuildingList[k].GetComponent<Building>().Interaction_Building(CurrentCellList[k]);
        }
        

        StartCoroutine(AddBuilding_InterActionCell());
    } 

    private void InputCurrentCell()
    {
        for (int num = 0; num < CellList.Count; num++)
        {
            if (CellList[num].TryGetComponent(out CellCtrl c) && !CurrentCellList.Contains(CellList[num]))
            {
                if (c.isOn == true)
                {
                    CurrentCellList.Add(CellList[num]);
                }
            }
        }

    }





}
