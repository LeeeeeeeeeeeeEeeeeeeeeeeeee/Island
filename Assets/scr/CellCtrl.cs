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

    public float RandomTimer = 100f;

    private LGH_Visit_Cell CellCreate;

    private void Start()
    {
        RandomTimer = Random.Range(180f, 864f);

        CellCreate = transform.root.GetComponent<LGH_Visit_Cell>();
        if (TimeManager.TimeSystem.offlineTime >= 0.15f)
        {
            int r = Random.Range(0, 2);

            if (r == 1)
            {
                Debug.Log("Visit");

                CellCreate.CellVisit(this.gameObject);
            }
        }

        Invoke("MakeInteraction", Random.Range(5f, 10f));
    }

    private void Update()
    {
        if(RandomTimer > 0)
        {
            RandomTimer = RandomTimer - 1 * Time.deltaTime;
        }
        else
        {
            Instantiate(gameObject.transform.GetChild(2), gameObject.transform.position, Quaternion.identity);
            CellCreate.debug_id = Id;
            CellCreate.left_debug = true;
        }
    }

    public void MakeInteraction()
    {
        StartCoroutine(InteractionButtonActive()); //1

        Invoke("MakeInteraction", Random.Range(300f, 600f));
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
        float T = Random.Range(0, 2);
        int n = Random.Range(0, transform.childCount - 1); //now 2
        Debug.Log(n);
        while (true)
        {
            _Time += Time.deltaTime;

            if (T <= _Time && isOn == true)
            {
                transform.GetChild(n).gameObject.SetActive(true); //2
                InteractionStart = true; //3
                break;
            }


            yield return null;
        }
    }
}
