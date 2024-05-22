using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCtrl : MonoBehaviour
{
    public bool isOn = false;
    public int Id;
    public string cellName;
    public Texture cellImage;
    public Sprite cellSprite;
    public bool InteractionStart = false;

    private LGH_Visit_Cell CellCreate;

    private void Start()
    {
        CellCreate = transform.root.GetComponent<LGH_Visit_Cell>(); 
        if(TimeManager.TimeSystem.offlineTime >= 0.15f)
        {
            int r = Random.Range(0, 2);

            if (r == 1)
            {
                Debug.Log("Visit");

                CellCreate.CellVisit(this.gameObject);
            }
        }

        StartCoroutine(InteractionButtonActive()); //1
    }

    public void InteractionGo(int type)
    {
        if (InteractionStart == true)
        {
            InteractionSystem.Interaction_system.StartCoroutine(InteractionSystem.Interaction_system.SmoothCameraMove(this.transform, type)); //6
        }
    }

    public IEnumerator InteractionButtonActive()
    {
        float _Time = 0;
        float T = Random.Range(0,2);
        while (true)
        {
            _Time += Time.deltaTime;

            if (T <= _Time && isOn==true)
            {
                transform.GetChild(0).gameObject.SetActive(true); //2
                InteractionStart = true; //3
                break;
            }


            yield return null;
        }
    }

}
