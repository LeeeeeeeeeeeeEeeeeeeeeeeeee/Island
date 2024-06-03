using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    public CellCtrl _CellCtrl;
    int k;
    void Start()
    {
        _CellCtrl= transform.parent.GetComponent<CellCtrl>();
        switch (this.name)
        {
            case "Pat":
                k = 1;
                break;
            case "Clapclap":
                k = 2;
                break;
            case "Snack":
                k = 3;
                break;
            default:
                break;
        }
    }

    public void InteractionStart()
    {
        _CellCtrl.InteractionGo(k); //5
    }

    

}
