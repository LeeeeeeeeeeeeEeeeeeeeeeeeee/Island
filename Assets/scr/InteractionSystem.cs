using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class InteractionSystem : MonoBehaviour
{

    public static InteractionSystem Interaction_system;



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

    public float smoothSpeed = 0.125f; // 이동 속도
    public Vector3 offset = Vector3.zero; // 오프셋
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
    public void PatInteraction(GameObject _Cell)
    {
        GameObject Pat = _Cell.transform.GetChild(0).gameObject;
        Pat.tag = "Pat";
        Pat.GetComponent<SpriteRenderer>().enabled = false;
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

            ArchitectureSystem.build_system.isConstrutMode = false;
            t.tag = "Interaction"; //9
            t.gameObject.SetActive(false);
            t.GetComponent<SpriteRenderer>().enabled = true;
            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y + 1, t.transform.position.z);
            t.parent.GetComponent<CellCtrl>().InteractionStart = false;
            
        }
        
    }
    #endregion



    #region clapclap
    [SerializeField] private Transform ClapClapButton;
    public void ClapClapInteraction(GameObject _Cell)
    {
        GameObject Clap = _Cell.transform.GetChild(1).gameObject;
        Clap.SetActive(false);
        ClapClapButton.position = new Vector2(Screen.width / 2, Screen.height / 2);
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
        yield return new WaitForSeconds(3);

        for(int i = 0; i <=2; i++) 
        {
            switch (a[i])
            {
                case 1:
                    b[0].SetActive(true);
                    yield return oneSecond;
                    b[0].SetActive(false);
                    break;

                case 2:
                    b[1].SetActive(true);
                    yield return oneSecond;
                    b[1].SetActive(false);
                    break; 

                case 3:
                    b[0].SetActive(true);
                    b[1].SetActive(true);
                    yield return oneSecond;
                    b[0].SetActive(false);
                    b[1].SetActive(false);
                    break;

                default:
                    break;
            }
        }
        if(BI!=4)
        {
            Debug.Log("Fail");
        }

        c.GetComponent<SpriteRenderer>().enabled = false;
        c.GetComponent<BoxCollider2D>().enabled = false;
    }

    int BI = 0;
    
    public void ClapClapInteraction_ButtonInput()
    {
        BI++;
        Debug.Log(BI);
    }
    #endregion

}












