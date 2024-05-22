using System.Collections;
using System.Collections.Generic;
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
        Interaction_system= this;
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
            t.parent.GetComponent<CellCtrl>().InteractionStart = false;
        }
    }
    #endregion

    #region calpcalp
    public void ClapClapInteraction(GameObject _Cell)
    {

    }

    #endregion

}












