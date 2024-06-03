using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManage : MonoBehaviour
{
    public List<GameObject> cells = new List<GameObject>();

    public List<GameObject> cellButtons = new List<GameObject>();

    public void CellActiver()
    {
        for(int i  = 0; i < cells.Count; i++)
        {
            if (cells[i].GetComponent<CellCtrl>().isOn)
            {
                cellButtons[i].GetComponent<Image>().sprite = cells[i].GetComponent<CellCtrl>().cellSprite;
            }
        }
    }
}
