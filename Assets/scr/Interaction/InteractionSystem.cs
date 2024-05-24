using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class InteractionSystem : MonoBehaviour
{

    public static InteractionSystem Interaction_system;

    public bool is_Interaction_Mode = false;

    public GameObject _GameManeger;
    public List<GameObject> _BuildingList
    {
        get { return ArchitectureSystem.build_system.BuildingList; }
    }
    public List<GameObject> CellList = new List<GameObject>();
    public List<GameObject> CurrentCellList = new List<GameObject>();

    private void Start()
    {
        
        Interaction_system = this;
        CellList = _GameManeger.GetComponent<CellManage>().cells;
        StartCoroutine(AddBuilding_InterActionCell());
    }

    private IEnumerator AddBuilding_InterActionCell()
    {
        int k = _BuildingList.Count;
        InputCurrentCell();
        yield return new WaitUntil(() => k < _BuildingList.Count);


        
        if (CurrentCellList.Count > 0)
        {
            //CurrentCellList[k].transform.position = _BuildingList[k].transform.position;
            _BuildingList[k].GetComponent<Building>().Interaction_Building(CurrentCellList[k]);
        }
        

        StartCoroutine(AddBuilding_InterActionCell());
    } 

    private void InputCurrentCell()
    {
        for (int num = 0; num < CellList.Count; num++)
        {
            if (CellList[num].TryGetComponent(out CellCtrl c) && !CurrentCellList.Contains(CellList[num]))
            {
                if (c.isOn == true)
                {
                    CurrentCellList.Add(CellList[num]);
                }
            }
        }
    }

    public float smoothSpeed = 0.125f;
    public Vector3 offset = Vector3.zero;
    [SerializeField] private Transform _Cam;

    public IEnumerator SmoothCameraMove(Transform target, int type)
    {
        while (true)
        {
            Debug.Log((_Cam.transform.position - target.position).magnitude);

            Vector3 newPosition = target.position + offset + new Vector3(0,0,-10);
            _Cam.transform.position = Vector3.Lerp(_Cam.transform.position, newPosition, smoothSpeed);


            if((_Cam.transform.position - target.position).magnitude <= 10f)
            {
                Debug.Log("End");
                Debug.Log((_Cam.transform.position - target.position).magnitude);
                break;
            }else if (_Cam.transform.position.x > 4f || _Cam.transform.position.x < -4f || _Cam.transform.position.y > 4f || _Cam.transform.position.y < -4f)
            {
                break;
            }

            
            yield return null;
        }
        Camera.main.orthographicSize = 3f;

        if (type == 1)
        {
            PatInteraction(target.gameObject); //7
        }
        if(type == 2) 
        {
            ClapClapInteraction(target.gameObject);
        }

    }

    #region Pat

    [SerializeField] private GameObject InterUI;

    public void PatInteraction(GameObject _Cell)
    {
        GameObject Pat = _Cell.transform.GetChild(0).gameObject;
        Pat.tag = "Pat";
        Pat.GetComponent<SpriteRenderer>().enabled = false;
        InterUI.transform.GetChild(0).gameObject.SetActive(true);
        Pat.transform.position = new Vector3(Pat.transform.position.x,Pat.transform.position.y-1,Pat.transform.position.z);
        Pat.GetComponent<BoxCollider2D>().size = new Vector2(5, 7);

        ArchitectureSystem.build_system.isConstrutMode = true; //8
    }

    int PatSlide =0;
    public GameObject _Particle;

    public IEnumerator PatInteraction2(Transform t)
    {
        PatSlide++;
        if(PatSlide>=5)
        {
            Debug.Log("Ok");
            for (int i = 0; i < 6; i++)
            {
                Instantiate(_Particle, t.parent.transform.position, _Particle.transform.rotation);
            }
            ArchitectureSystem.build_system.MoneyOutput = 50;

            yield return new WaitForSeconds(2);

            Inventory.Instance.AlertText.text = "성공!!!";
            Inventory.Instance.AlertText.color = Color.white;

            ArchitectureSystem.build_system.isConstrutMode = false;
            t.tag = "Interaction"; //9
            t.gameObject.SetActive(false);
            t.GetComponent<SpriteRenderer>().enabled = true;
            InterUI.transform.GetChild(0).gameObject.SetActive(false);
            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y + 1, t.transform.position.z);
            t.parent.GetComponent<CellCtrl>().InteractionStart = false;
            InteractionSystem.Interaction_system.is_Interaction_Mode = false;
        }
        
    }
    #endregion



    #region clapclap

    [SerializeField] private Transform ClapClapButton;

    public void ClapClapInteraction(GameObject _Cell)
    {
        GameObject Clap = _Cell.transform.GetChild(1).gameObject;
        Clap.SetActive(false);
        ClapClapButton.position = new Vector2(Screen.width / 2, Screen.height / 2 - 750);
        ClapClapButton.gameObject.SetActive(true);

        int[] a = { 1, 2, 3 };
        a = a.OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();

        GameObject[] b = {ClapClapButton.GetChild(0).gameObject, ClapClapButton.GetChild(1).gameObject};
        StartCoroutine(ClapClapInteraction2(a, b,Clap));
    }

    public static WaitForSeconds oneSecond = new WaitForSeconds(3f);

    private IEnumerator ClapClapInteraction2(int[] a, GameObject[] b , GameObject c)
    {
        BI = 0;
        b[0].SetActive(true);
        b[1].SetActive(true);
        yield return new WaitForSeconds(3);

        for(int i = 0; i <= 2; i++) 
        {
            switch (a[i])
            {
                case 1:
                    b[0].GetComponent<Image>().color = Color.yellow;
                    yield return new WaitForSecondsRealtime(1);
                    b[0].GetComponent<Image>().color = Color.white;
                    break;

                case 2:
                    b[1].GetComponent<Image>().color = Color.yellow;
                    yield return new WaitForSecondsRealtime(1);
                    b[1].GetComponent<Image>().color = Color.white;
                    break; 

                case 3:
                    b[0].GetComponent<Image>().color = Color.yellow;
                    b[1].GetComponent<Image>().color = Color.yellow;
                    yield return new WaitForSecondsRealtime(1);
                    b[0].GetComponent<Image>().color = Color.white;
                    b[1].GetComponent<Image>().color = Color.white;
                    break;

                default:
                    break;
            }
        }

        if(BI!=4)
        {
            Inventory.Instance.AlertText.text = "실패...";
            Inventory.Instance.AlertText.color = Color.white;
            Debug.Log("Fail");
        }
        else
        {
            Inventory.Instance.AlertText.text = "성공!!!";
            Inventory.Instance.AlertText.color = Color.white;
        }

        c.GetComponent<SpriteRenderer>().enabled = false;
        c.GetComponent<BoxCollider2D>().enabled = false;
        b[0].SetActive(false);
        b[1].SetActive(false);
        InterUI.transform.GetChild(1).gameObject.SetActive(false);
        InteractionSystem.Interaction_system.is_Interaction_Mode = false;
    }

    int BI = 0;
    
    public void ClapClapInteraction_ButtonInput(Image Source)
    {
        if (Source.color == Color.yellow)
        {
            Source.color = Color.white;
            BI++;
        }
        else
        {
            Source.color = Color.red;
            BI--;
        }

        Debug.Log(BI);
    }
    #endregion

}












