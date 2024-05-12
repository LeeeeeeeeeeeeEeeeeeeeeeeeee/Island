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
    //건물 설치를 위한 스크립트
    public static BuildingSystem build_system;
    public GameObject Store_Ui;
    public event Action touchUp;

    [HideInInspector] public GameObject Store_Obj;

    public bool isCameraMode = false;
    public bool isConstrutMode = false;
    private bool isRearrangeMode = false;

    public int Money = 0;
    public int MoneyOutput = 0;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MoneyText2;
    public List<button_SendToCan> ButtonList;

    


    public Dictionary<string, int> MoneyValue = new Dictionary<string, int>()
    {
        { "House", 0 },
        { "Cafe", 100 },
        { "Grocery", 100 }
        //가격표
    };


    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = Money.ToString();
        build_system = this;
        Store_Obj = transform.GetChild(0).gameObject;
        touchUp += OK_IConstructThere;

        ButtonList = new List<button_SendToCan>(Store_Ui.transform.GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<button_SendToCan>());
       

    }

    private float timer = 0f;
    private float interval = 3f;

    void Update()
    {
        MoneyText.text = Money.ToString();
        MoneyText2.text = Money.ToString();

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Money += MoneyOutput;
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
        isConstrutMode = true;
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
        isConstrutMode = false;
    }


    #region purchase and construction
    Coroutine co;
    GameObject go;
    public GameObject emptyBuilding;

    public void LetsConstructor(String BuildName , Sprite Get_Sprite)
    {
        Store_Obj.GetComponent<BoxCollider2D>().enabled = false;
        Store_Ui.SetActive(false);
        go = Instantiate(emptyBuilding);
        go.name = BuildName;
        go.transform.SetParent(transform, true);
        if (Input.touchCount > 0)
        {
            Touch tt = Input.GetTouch(0);
            go.transform.position = tt.position;
        }
        go.GetComponent<SpriteRenderer>().sprite = Get_Sprite;
        co = StartCoroutine(FollowMouse(go, 1));
        //상점오브젝트/상점ui 비활성화 및 빈 건물 생성 후 스프라이트와 이름 설정
        //터치위치로 따라가는 코루틴 시작
    }

    public void OK_IConstructThere()
    {
        go.tag = "Building";
        StopCoroutine(co);
        co = null;
        go = null;
        Store_Obj.GetComponent<BoxCollider2D>().enabled = true;
        //태그 변경 및 터치위치 따라가기 코루틴 정지 및 코루틴변수,빈건물변수 null값, 상점오브젝트 활성화
    }
    #endregion
}
