using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building_basicSCR
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ArchitectureSystem.build_system.MoneyOutput = 30;
    }

    public void Interaction_House(GameObject cell)
    {
        //cell.transform.localRotation = Quaternion.Euler(90, 0, 0);

        Debug.Log("Move!");
        StartCoroutine(WalkToHome(cell.transform));
    }

    private IEnumerator WalkToHome(Transform cell)
    {
        Vector2 Target = this.transform.localPosition - cell.position;

        while (true) 
        {
            cell.transform.position = Vector2.MoveTowards(cell.position, Target, 0.001f);
           yield return null;
        }
    }
}
