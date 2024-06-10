using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static System.Net.WebRequestMethods;

public class Cam_TouchOBJ : MonoBehaviour
{
    public GameObject[] Particles;

    Vector2 Touch_start_pos;

    public Collider2D BeganCol;

    public float ForRearrangeTime;
    float Rearrange_Time = 0;
    Coroutine Rearrange_Co;

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toto = Input.GetTouch(0);

            #region Currently Clicked Obj in : Collider2D clickCol
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(clickPos, transform.forward, 15f, 1 << LayerMask.NameToLayer("TouchLayer"));
            Debug.DrawRay(clickPos, transform.forward * 10, Color.red, 0.3f);
            Collider2D clickCol = hit.collider;
            //Collider2D clickCol = Physics2D.OverlapPoint(clickPos);
            #endregion

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (ArchitectureSystem.build_system.isCameraMode == false)
            {
                if (toto.phase == TouchPhase.Began)
                {
                    if (clickCol != null)
                    {
                        BeganCol = clickCol;
                        

                        if (clickCol.tag == "Building" || clickCol.tag == "Cook")
                        {
                            //if (clickCol.transform.parent.TryGetComponent(out Building bb) && bb.RearrangeNow == true)
                            //{
                            //    Debug.Log("설치");
                            //    bb.BuildingMove();
                            //}
                            //else 
                            if (clickCol.transform.parent.TryGetComponent(out Building b) && b.RearrangeNow == false && !ArchitectureSystem.build_system.isConstrutMode)
                            {
                                if (ArchitectureSystem.build_system.Building_BtnObj.activeSelf == false)
                                {
                                    Rearrange_Co = StartCoroutine(Rearrange_Func(b));
                                }
                            }
                        }
                    }
                    else
                    {
                        BeganCol = null;
                    }
                }

                if (toto.phase == TouchPhase.Ended && BeganCol == clickCol && clickCol != null && InteractionSystem.Interaction_system.is_Interaction_Mode == false)
                {
                    if (clickCol.tag == "Interaction" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        clickCol.GetComponent<InteractionButton>().InteractionStart(); //4
                        InteractionSystem.Interaction_system.is_Interaction_Mode = true;
                        SoundManager.instance.PlaySound("Inter");
                    }

                    if (clickCol.tag == "store" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        //UI_Manager.Instance.OnClick_Open_Popup_Btn("shop_building");
                        SoundManager.instance.PlaySound("Shop");
                    }
                    else if (clickCol.tag == "Building" && clickCol != null)
                    {
                        //if (clickCol.transform.parent.TryGetComponent(out Building bb) && bb.RearrangeNow == true)
                        //{
                        //    Debug.Log("설치");
                        //    bb.BuildingMove();
                        //}

                        if (clickCol.transform.parent.TryGetComponent(out Food_Generator generator))
                        {
                            if (generator.current_food_count > 0)
                            {
                                Instantiate(Particles[1], clickCol.transform.position, Particles[1].transform.rotation);
                            }

                            generator.Get_Food();
                        }
                    }
                    else if(clickCol.tag == "Animal" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        Instantiate(Particles[0], clickCol.transform.position, Particles[0].transform.rotation);

                        ArchitectureSystem.build_system.Money += 1;
                        
                    }
                    else if (clickCol.tag == "Cook" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        //UI_Manager.Instance.OnClick_Open_Popup_Btn("cooking");
                        SoundManager.instance.PlaySound("Cook");
                    }
                    else if (clickCol.tag == "Pat")
                    {
                        Touch_start_pos = toto.position;
                        SoundManager.instance.PlaySound("Inter");
                    }
                }

                if (toto.phase == TouchPhase.Ended && clickCol != null)
                {
                    if(InteractionSystem.Interaction_system.IsSeeking==true && clickCol.tag == "Animal")
                    {
                        InteractionSystem.Interaction_system.HideAndSeekInteraction2(clickCol.gameObject);
                    }
                }

                if (clickCol != null && toto.phase == TouchPhase.Moved)
                {
                    if (clickCol.tag == "Pat")
                    {
                        Debug.Log(Vector2.Distance(Touch_start_pos, toto.position));
                        if (Vector2.Distance(Touch_start_pos, toto.position) >= 300)
                        {
                            Touch_start_pos = toto.position;
                            InteractionSystem.Interaction_system.StartCoroutine(InteractionSystem.Interaction_system.PatInteraction2(clickCol.transform));
                        }
                    }
                }

                if (toto.phase == TouchPhase.Ended)
                {
                    SoundManager.instance.PlaySound("Touch");
                    Instantiate(Particles[2], clickPos, Particles[2].transform.rotation);

                    if (Rearrange_Co != null)
                    {
                        StopCoroutine(Rearrange_Co);
                    }
                }

            }
        }
    }

    private IEnumerator Rearrange_Func(Building b)
    {
        Rearrange_Time = 0;
        while (true)
        {
            Rearrange_Time += Time.deltaTime;
            if (ForRearrangeTime <= Rearrange_Time)
            {
                ArchitectureSystem.build_system.Rearrange();
                b.btn_active();
                b.BuildingMove();
                break;
            }

            yield return null;
        }
    }

}
