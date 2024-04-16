using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem build_system;
    public GameObject Store_Ui;
    public event Action touchUp;

    [HideInInspector] public GameObject Store_Obj;

    private bool isRearrangeMode = false;

    public int Money = 0;
    public TextMeshProUGUI MoneyText;

    public Dictionary<string, int> MoneyValue = new Dictionary<string, int>()
    {
        { "House", 0 },
        { "Cafe", 100 },
        { "Grocery", 100 }
    };


    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = Money.ToString();
        build_system = this;
        Store_Obj = transform.GetChild(0).gameObject;
    }

    private float timer = 0f;
    private float interval = 3f;

    void Update()
    {

        MoneyText.text = Money.ToString();
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            
            Money += 30;
            timer = 0f;
        }
    }

    public void Rearrange()
    {
        Building[] p = GetComponentsInChildren<Building>();


        if (isRearrangeMode == false)
        {
            Store_Obj.GetComponent<BoxCollider2D>().enabled = false;
            
            foreach (var item in p)
            {
                item.RearrangeNow = true;
            }
            isRearrangeMode = true;
        }

        else if (isRearrangeMode == true)
        {
            Store_Obj.GetComponent<BoxCollider2D>().enabled = true;

            bool[] arr = { };
            foreach (var item in p)
            {
                arr.Append(item.isnotCol);
                if (Array.TrueForAll(arr, x => x))
                {
                    item.RearrangeNow = false;
                }
            }
            if (Array.TrueForAll(arr, x => x))
            {
                isRearrangeMode = false;
            }
        }
    }

    public IEnumerator FollowMouse(GameObject gg, int Where)
    {
        //int 1 = 건물설치
        //int 2 = 건물재배치
        gg.TryGetComponent(out Building b);
        bool isCol;
        while (true)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
            if(Input.touchCount== 1 && clickPos.y <= 1880) 
            {
                Touch tt = Input.GetTouch(0);
                if (tt.phase == TouchPhase.Moved)
                {
                    gg.transform.localPosition = clickPos;
                }
                if (tt.phase == TouchPhase.Ended)
                {
                    isCol = b.isnotCol;
                    if (!isCol)
                    {
                        Debug.Log("위치 재지정 필요");

                    }
                    else if (isCol)
                    {
                        if (Where == 1)
                        {
                            touchUp();
                        }
                        break;
                    }
                }
            }
            //if (Input.mousePosition.y <= 1880)
            //{
            //    gg.transform.localPosition = clickPos;
            //}
            yield return null;

        }
    }
}
