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

        StartCoroutine(InteractionWithPlayer());
    }


    public IEnumerator InteractionWithPlayer()
    {
        float _Time = 0;

        while(true)
        {
            _Time += Time.deltaTime;
            float T = Random.Range(10, 16);
            if (T == _Time)
            {

            }


            yield return null;
        }
    }

}
