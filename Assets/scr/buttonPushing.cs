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
    GameObject go;
    public bool isColliding;

    private void Start()
    {
        storeUi = BuildingSystem.build_system.Store_Ui;
        storeObject = BuildingSystem.build_system.Store_Obj;
        BuildingSystem.build_system.touchUp += OK_IConstructThere;
    }

    public void LetsConstructor()
    {
        storeObject.GetComponent<BoxCollider2D>().enabled = false;
        storeUi.SetActive(false);
        go = Instantiate(emptyBuilding);
        go.transform.SetParent(BuildingSystem.build_system.transform, true);
        go.GetComponent<SpriteRenderer> ().sprite = get_Sprite;
        co = StartCoroutine(BuildingSystem.build_system.FollowMouse(go,1));
    }

    public void OK_IConstructThere()
    {
        go.tag = "Building";
        StopCoroutine(co);
        co= null;
        go = null;
        get_Sprite = null;
        storeObject.GetComponent<BoxCollider2D>().enabled = true;

    }

}
