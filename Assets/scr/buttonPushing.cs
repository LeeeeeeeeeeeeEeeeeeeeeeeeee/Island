using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class buttonPushing : MonoBehaviour
{
    public Sprite get_Sprite;
    private GameObject storeUi;
    private GameObject storeObject;
    [SerializeField] private GameObject emptyBuilding;
    [SerializeField] private GameObject RearrangeExitbtn;
    
    
    Coroutine co;
    Coroutine co2;
    GameObject go;
    public bool isColliding;

    private void Start()
    {
        storeUi = BuildingSystem.build_system.Store_Ui;
        storeObject = BuildingSystem.build_system.Store_Obj;
        BuildingSystem.build_system.touchUp += OK_IConstructThere;
    }

    public void LetsConstructor(String BuildName)
    {
        storeObject.GetComponent<BoxCollider2D>().enabled = false;
        storeUi.SetActive(false);
        go = Instantiate(emptyBuilding);
        go.name = BuildName;
        go.transform.SetParent(BuildingSystem.build_system.transform, true);
        if(Input.touchCount > 0) 
        {
            Touch tt = Input.GetTouch(0);
            go.transform.position = tt.position;
        }
        go.GetComponent<SpriteRenderer> ().sprite = get_Sprite;
        co = StartCoroutine(BuildingSystem.build_system.FollowMouse(go,1));
    }

    public void OK_IConstructThere()
    {
        go.tag = "Building";

        BuildingSystem.build_system.Building_BtnObj.SetActive(true);
        co2 = StartCoroutine(FollowBuilding_btn());
    }

    private IEnumerator FollowBuilding_btn()
    {
        Transform btnObj = BuildingSystem.build_system.Building_BtnObj.transform;
        Transform CrtBuilding = BuildingSystem.build_system.CurrentSelectedBuilding.transform;
        while (true)
        {
            Debug.Log("SDa");
            btnObj.position = Camera.main.WorldToScreenPoint(CrtBuilding.position - Vector3.up);
            yield return null;
        }
    }

    public void btn_OK()
    {
        //StopCoroutine(co);
        //StopCoroutine(co2);
       

        if(BuildingSystem.build_system.isRearrangeMode == true)
        {
            BuildingSystem.build_system.CurrentSelectedBuilding.GetComponent<Building>().StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
            co = null;
            co2 = null;

            go = null;
            get_Sprite = null;
        }
        
        storeObject.GetComponent<BoxCollider2D>().enabled = true;
        
        
        BuildingSystem.build_system.isConstrutMode= false;

        BuildingSystem.build_system.Building_BtnObj.SetActive(false);
    }

    public void btn_Flip()
    {
        GameObject g = BuildingSystem.build_system.CurrentSelectedBuilding;
        if(g.TryGetComponent(out SpriteRenderer ren))
        {
            if(ren.flipX == true)
            {
                ren.flipX = false;
            }else if(ren.flipX==false)
            {
                ren.flipX = true; 
            }
        }
    }

    public void btn_Cancle()
    {
        Destroy(BuildingSystem.build_system.CurrentSelectedBuilding);
        //StopCoroutine(co);
        //StopCoroutine(co2);
        if (BuildingSystem.build_system.isRearrangeMode == true)
        {
            BuildingSystem.build_system.CurrentSelectedBuilding.GetComponent<Building>().StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
            co = null;
            co2 = null;

            go = null;
            get_Sprite = null;
        }

        storeObject.GetComponent<BoxCollider2D>().enabled = true;
        
        BuildingSystem.build_system.isConstrutMode = false;


        BuildingSystem.build_system.Building_BtnObj.SetActive(false);

    }




}
