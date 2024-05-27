using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ArchitectureSystem : MonoBehaviour
{
    //Scripts for building installation
    public static ArchitectureSystem build_system;
    public GameObject Store_Ui;
    public event Action touchUp;

    [HideInInspector] public GameObject Store_Obj;

    public bool isCameraMode = false;
    public bool isConstrutMode = false;
    public bool isRearrangeMode = false;

    public int Money = 0;
    public int MoneyOutput = 0;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MoneyText2;
    public List<button_SendToCan> ButtonList;
    public List<GameObject> BuildingList;

    public GameObject Building_BtnObj;
    public GameObject CurrentSelectedBuilding;

    public Dictionary<string, int> MoneyValue = new Dictionary<string, int>()
    {
        { "빨간 설탕 유리 꽃", 0 },
        { "노란 설탕 유리 꽃", 0 },
        { "파란 설탕 유리 꽃", 0 },
        { "비스킷 의자", 0 },
        { "아이스크림 테이블", 0 },
        { "마카롱 쿠션", 0 },
        { "2단 마카롱 쿠션", 0 },
        { "딸기우유 연못", 0 },
        { "솜사탕 구름 1", 0 },
        { "솜사탕 구름 2", 0 },
        { "캔디 가로등", 0 },
        { "롤리팝 캔디 나무", 0 },
        { "마카롱 나무", 0 },
        { "초코 분수", 0 },
        { "녹차 푸딩 산", 0 },
        { "초코 푸딩 산", 0 },
        { "거대한 롤리팝 마시멜로우 언덕", 0 }
    };

    void Start()
    {
        MoneyText.text = Money.ToString();
        build_system = this;
        Store_Obj = transform.GetChild(0).gameObject;

        ButtonList = new List<button_SendToCan>(Store_Ui.transform.GetChild(1).GetComponentsInChildren<button_SendToCan>());

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
            Store_Obj.GetComponent<PolygonCollider2D>().enabled = false;
            
            foreach (var item in p)
            {
                item.RearrangeNow = true;
            }
            isRearrangeMode = true;
        }

        else if (isRearrangeMode == true)
        {
            Store_Obj.GetComponent<PolygonCollider2D>().enabled = true;

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
        //int 1 = Build
        //int 2 = Rearrange
        gg.TryGetComponent(out Building b);
        bool isCol;
        isConstrutMode = true;
        CurrentSelectedBuilding = gg;
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
                            OK_IConstructThere();
                        }
                        else if(Where == 2) 
                        {
                            touchUp();
                        }
                    }
                }
            }
            Debug.Log("This");
            yield return null;

        }
        
        
    }


    #region purchase and construction
    Coroutine co;
    Coroutine co2;
    GameObject go;
    public GameObject emptyBuilding;

    public void LetsConstructor(String BuildName , Sprite Get_Sprite)
    {
        Store_Obj.GetComponent<PolygonCollider2D>().enabled = false;
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
        go.AddComponent<PolygonCollider2D>();
        go.GetComponent<PolygonCollider2D>().isTrigger = true;
        co = StartCoroutine(FollowMouse(go, 1));
        //상점오브젝트/상점ui 비활성화 및 빈 건물 생성 후 스프라이트와 이름 설정
        //터치위치로 따라가는 코루틴 시작
    }



    public void OK_IConstructThere()
    {
        go.tag = "Building";

        Building_BtnObj.SetActive(true);
        co2 = StartCoroutine(FollowBuilding_btn());
    }

    private IEnumerator FollowBuilding_btn()
    {
        Transform btnObj = Building_BtnObj.transform;
        Transform CrtBuilding = CurrentSelectedBuilding.transform;
        while (true)
        {
            btnObj.position = Camera.main.WorldToScreenPoint(CrtBuilding.position - Vector3.up);
            yield return null;
        }
    }

    public void btn_OK()
    {
        //StopCoroutine(co);
        //StopCoroutine(co2);


        if (isRearrangeMode == true)
        {
            CurrentSelectedBuilding.GetComponent<Building>().StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
            co = null;
            co2 = null;

            go = null;
        }

        Store_Obj.GetComponent<PolygonCollider2D>().enabled = true;


        isConstrutMode = false;

        Building_BtnObj.SetActive(false);

        CurrentSelectedBuilding = null;
    }

    public void btn_Flip()
    {
        GameObject g = CurrentSelectedBuilding;
        if (g.TryGetComponent(out SpriteRenderer ren))
        {
            if (ren.flipX == true)
            {
                ren.flipX = false;
            }
            else if (ren.flipX == false)
            {
                ren.flipX = true;
            }
        }
    }

    public void btn_Cancle()
    {
        //StopCoroutine(co);
        //StopCoroutine(co2);
        if (isRearrangeMode == true)
        {
            CurrentSelectedBuilding.GetComponent<Building>().StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
            co = null;
            co2 = null;
            go = null;
        }
        Destroy(CurrentSelectedBuilding);

        Store_Obj.GetComponent<PolygonCollider2D>().enabled = true;

        isConstrutMode = false;

        Building_BtnObj.SetActive(false);

        CurrentSelectedBuilding = null;
    }
    #endregion
}
