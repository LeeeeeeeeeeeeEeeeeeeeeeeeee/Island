using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_TouchOBJ : MonoBehaviour
{
    public GameObject Particle;

    private GameObject store_Ui;

    // Start is called before the first frame update
    private void Start()
    {
        store_Ui = BuildingSystem.build_system.Store_Ui;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toto = Input.GetTouch(0);

            #region Currently Clicked Obj in : Collider2D clickCol
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
            Collider2D clickCol = Physics2D.OverlapPoint(clickPos);
            #endregion

            if (BuildingSystem.build_system.isCameraMode == false)
            {
                if (toto.phase == TouchPhase.Began && clickCol != null && Input.mousePosition.y <= 1880)
                {
                    if (clickCol.tag == "store")
                    {
                        store_Ui.SetActive(true);
                    }
                    else if (clickCol.tag == "Building" && clickCol != null)
                    {
                        if (clickCol.TryGetComponent(out Building bb) && bb.RearrangeNow == true)
                        {
                            Debug.Log("재배치모드");
                            bb.BuildingMove();
                        }
                    }
                    else if(clickCol.tag == "Animal")
                    {
                        Instantiate(Particle, clickCol.transform.position, Particle.transform.rotation);

                        BuildingSystem.build_system.Money += 1;
                    }
                }
            }
        }
    }
}
